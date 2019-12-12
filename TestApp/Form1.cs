using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using dotnet.FHIR.common;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;


namespace dotnet.FHIR.TestApp
{
	public partial class Form1 : Form
	{
		private ClientWebSocket _ws = null;
		private readonly static HttpClient client = new HttpClient();
		private string _endpoint = null;
		private BackgroundWorker _webSocketReader;
		private BackgroundWorker _callbackReader;
		private string _topic = null;
		private string _ipaddress = null;
		private HttpListener _listener;

		const string APPNAME = "TestApp";
		const string CLIENT_KEY = "skJxyHANXWjQpkbvg5SYqz9iyEyj9A0I";
		const string SECRET = "SeCreT";

		public Form1()
		{
			InitializeComponent();
		}

		#region UI Events

		private void Form1_Load(object sender, EventArgs e)
		{
			txtHubUrl.Text = Settings.Default.txtHubUrl;
			txtTopic.Text = Settings.Default.txtTopic;
			txtSubEvents.Text = Settings.Default.txtSubEvents;
			//txtNotAccession.Text = Settings.Default.txtNotAccession;
			_ipaddress = GetLocalIPAddress();
		}

		public static string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("No network adapters with an IPv4 address in the system!");
		}

		private async void btnSubscribe_Click(object sender, EventArgs e)
		{
			// save subscription parameters
			Settings.Default.txtHubUrl = txtHubUrl.Text;
			Settings.Default.txtTopic = txtTopic.Text;
			Settings.Default.txtSubEvents = txtSubEvents.Text;
			Settings.Default.Save();
			HttpClient client = new HttpClient();
			HttpResponseMessage response;
			//Subscribe/Unsubscribe
			string mode = btnSubscribe.Text.ToLower();
			_topic = txtTopic.Text;
			UriBuilder urlBuilder = new UriBuilder($"{txtHubUrl.Text}/api/hub");
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri);
			request.Headers.Authorization = new AuthenticationHeaderValue("key", CLIENT_KEY);
			Dictionary<string, string> hub = new Dictionary<string, string>
			{
				{ "hub.channel.type", chkWS.Checked ? "websocket" : "rest-hook" },
				{ "hub.callback", chkWS.Checked ? "" : $"http://{_ipaddress}:6000" },
				{ "hub.mode",  mode},
				{ "hub.topic", _topic },
				{ "hub.events", txtSubEvents.Text },
				{ "hub.secret", SECRET },
				{ "hub.lease", "999" },
			};
			if (mode == "subscribe")
			{
				if (!chkWS.Checked)
				{
					// set up callback reader before sending request
					_callbackReader = new BackgroundWorker();
					_callbackReader.DoWork += _callbackReader_DoWork;
					_callbackReader.RunWorkerAsync();
				}
			}
			else
			{
				hub.Add("hub.channel.endpoint", _endpoint);
			}
			FormUrlEncodedContent enc = new FormUrlEncodedContent(hub);
			request.Content = enc;
			response = await client.SendAsync(request);
			if (!response.IsSuccessStatusCode)
			{
				Log($"{btnSubscribe.Text} request was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}");
			}
			else
			{
				if (btnSubscribe.Text == "Unsubscribe")
				{
					if (chkWS.Checked)
					{
						Log($"{btnSubscribe.Text} request was accepted. Closing web socket and terminating background thread.");
						try
						{
							await _ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "client unsubscribing from topic", CancellationToken.None);
							_ws.Dispose();
						}
						catch (Exception ex)
						{
							Log($"Exception occurred closing the websocket: {ex.Message}");
						}
						_endpoint = null;
					}
					else
					{
						_listener.Stop();
						_listener.Close();
					}
					btnSubscribe.Text = "Subscribe";
				}
				else // SUBSCRIBE
				{
					if (chkWS.Checked)
					{
						_endpoint = await response.Content.ReadAsStringAsync();
						Log($"Hub returned websocket URL: {_endpoint}");
						// Connect to websocket
						try
						{
							Log($"Connecting to Hub Websocket: {_endpoint}");
							_ws = new ClientWebSocket();
							await _ws.ConnectAsync(new Uri(_endpoint), CancellationToken.None);
							_webSocketReader = new BackgroundWorker();
							_webSocketReader.DoWork += webSocketReader_DoWork;
							_webSocketReader.RunWorkerAsync();
						}
						catch (Exception ex)
						{
							_ws.Dispose();
							MessageBox.Show(ex.ToString());
							return;
						}
						btnSubscribe.Text = "Unsubscribe";
					}
				}
			}
		}

		#endregion

		/// <summary>
		/// Thread that reads messages from each websocket connect
		/// BIG TODO: process acknowledgements by using some sort of notification queue 
		/// of outbound notifications
		/// </summary>
		/// <param name="sender">TODO: this needs to contain something that will identify the websocket
		/// in order to handle acknowledgements and take the notification off the queue.</param>
		/// <param name="e"></param>
		private async void webSocketReader_DoWork(object sender, DoWorkEventArgs e)
		{
			while (_ws.State == WebSocketState.Open)
			{
				Log("Websocket reader waiting for data...");
				string socketData = null;
				try
				{
					socketData = await WebSocketLib.ReceiveStringAsync(_ws, CancellationToken.None);
				}
				catch (Exception ex)
				{
					Log($"Error reading websocket: {ex.Message}.");
				}
				if (_ws.State != WebSocketState.Open)
				{
					Log($"Websocket no longer open. State is {_ws.State.ToString()}");
				}
				else
				{
					int len = null == socketData ? 0 : socketData.Length;
					Log($"{len} bytes read - processing message...");
					Notification nMessage = JsonConvert.DeserializeObject<Notification>(socketData);
					NotificationEvent ev = null;
					if (null != nMessage.Event)
					{
						ev = nMessage.Event;
					}
					if (null != ev)
					{
						Log($"Event notification received:\r\n{nMessage}");
					}
					else
					{
						Log($"Invalid event notification received");
					}
				}
			}
			Log("Websocket reader terminated.");
		}


		private void _callbackReader_DoWork(object sender, DoWorkEventArgs e)
		{
			_listener = new HttpListener();
			string callbackURL = $"http://{_ipaddress}:6000/";
			_listener.Prefixes.Add(callbackURL);
			//NOTE: this requires elevated permissions. Run as administrator, or grant permissions to this user
			try
			{
				_listener.Start();
			}
			catch (Exception)
			{
				MessageBox.Show("Running this client with a RESTful callback requires elevated permissions. Run as administrator, or grant permissions to this user");
				return;
			}
			Log($"Listening on {callbackURL} for callback...");
			// The first thing we should get is an intent verification when we attempt to subscribe 
			HttpListenerContext context = _listener.GetContext();
			HttpListenerRequest request = context.Request;
			string challenge = request.QueryString["hub.challenge"];
			if (null == challenge)
			{
				Log("Intent verification failed - no challenge sent. Not subscribed.");
			}
			else
			{
				Log("Intent verification succeeded - client subscribed.");
				this.btnSubscribe.Invoke((MethodInvoker)delegate
				{
					btnSubscribe.Text = "Unsubscribe";
				});

				// Respond with the challenge
				HttpListenerResponse response = context.Response;
				response.StatusCode = (int)HttpStatusCode.Accepted;
				var buffer = Encoding.UTF8.GetBytes(challenge);
				response.ContentLength64 = buffer.Length;
				response.OutputStream.Write(buffer, 0, buffer.Length);

				// loop here reading and processing notification messages
				System.IO.Stream body;
				System.Text.Encoding encoding;
				System.IO.StreamReader reader;
				while (true)
				{
					try
					{
						context = _listener.GetContext();
					}
					catch (Exception ex)
					{
						Log($"Exception reading callback:\r\n{ex.Message}");
						break;
					}
					request = context.Request;
					response = context.Response;
					body = request.InputStream;
					encoding = request.ContentEncoding;
					reader = new System.IO.StreamReader(body, encoding);
					string s = reader.ReadToEnd();
					if (String.IsNullOrEmpty(s))
					{
						Log("Callback received empty message.");
					}
					else
					{
						JObject o = JObject.Parse(s);
						string formattedJson = JsonConvert.SerializeObject(o, Formatting.Indented);
						Log($"Callback received message:\r\n{formattedJson}");
						Notification nMessage = null;
						try
						{
							nMessage = JsonConvert.DeserializeObject<Notification>(s);
						}
						catch (Exception) { }
						NotificationEvent ev = null;
						if (null != nMessage && null != nMessage.Event)
						{
							ev = nMessage.Event;
						}
						if (null != ev)
						{
							Log($"Valid event notification received.");
							response.StatusCode = (int)HttpStatusCode.Accepted;
							string responseContent = "Accepted";
							buffer = Encoding.UTF8.GetBytes(responseContent);
							response.ContentLength64 = buffer.Length;
							response.OutputStream.Write(buffer, 0, buffer.Length);
						}
						else
						{
							Log($"Invalid event notification received.");
							response.StatusCode = (int)HttpStatusCode.BadRequest;
							string responseContent = "BadRequest";
							buffer = Encoding.UTF8.GetBytes(responseContent);
							response.ContentLength64 = buffer.Length;
							response.OutputStream.Write(buffer, 0, buffer.Length);
						}
					}
				}
				Log("Callback worker thread terminated.");
			}
		}

		private void Log(string message)
		{
			if (this.InvokeRequired)
			{
				this.txtLog.Invoke((MethodInvoker)delegate
				{
					// Running on the UI thread
					txtLog.Text += DateTime.Now.ToString("G") + "\t" + message + "\r\n";
				});
			}
			else
				txtLog.Text += DateTime.Now.ToString("G") + "\t" + message + "\r\n";
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.txtLog.Invoke((MethodInvoker)delegate
				{
					// Running on the UI thread
					txtLog.Text = "";
				});
			}
			else
				txtLog.Text = "";
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(txtLog.Text);
		}

		private void btnNotify_Click(object sender, EventArgs e)
		{
			// send DiagnosticReport-open event
			string accessionNumber = txtAccession.Text;
			Notification notification = new Notification
			{
				Timestamp = DateTime.Now,
				Id = $"{APPNAME}-{Guid.NewGuid().ToString("N")}",
				Event = new NotificationEvent()
				{
					HubEvent = "DiagnosticReport-open",
					Topic = _topic,
					Contexts = new List<Context>
					{
						new Context()
						{
							Key = "Report",
							Resource = new DiagnosticReport
							{
								Id = Guid.NewGuid().ToString("N"),
							}
						},
						new Context()
						{
							Key = "Study",
							Resource = new ImagingStudy()
							{
								Id = accessionNumber,
								Identifier =
								{ new Identifier
									{
										Type = new CodeableConcept
										{
											Coding = new List<Coding>
											{
												new Coding
												{
													System = "http://terminology.hl7.org/CodeSystem/v2-0203",
													Code = "ACSN"
												}
											}
										},
										Value = accessionNumber
									}
								}
							}
						}
					}
				}
			};

			// send the notification event to the hub
			_ = SendNotification(notification);
		}

		private async Task<bool> SendNotification(Notification notification)
		{
			// Serialize the notification and send it to the hub
			JsonSerializerSettings js = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			string json = JsonConvert.SerializeObject(notification, Formatting.Indented, js);
			HttpResponseMessage response;
			UriBuilder urlBuilder = new UriBuilder($"{txtHubUrl.Text}/api/hub/{_topic}");
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri);
			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", CLIENT_KEY);
			request.Content = new StringContent(json, Encoding.UTF8, "application/json");
			Log($"Sending notification event:\r\n{json}");
			response = await client.SendAsync(request);
			if (!response.IsSuccessStatusCode)
			{
				Log($"***** Event notification was not accepted: {(int)response.StatusCode} - {response.ReasonPhrase}");
				return false;
			}
			else
				return true;
		}

	}

}
