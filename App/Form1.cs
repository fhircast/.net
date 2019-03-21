using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net.WebSockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using dotnet.FHIR.common;

namespace dotnet.FHIR.app
{
	public partial class Form1 : Form
	{
		private ClientWebSocket _ws = new ClientWebSocket();
		private BackgroundWorker webSocketReader;
		private string _topic = null; 
		private string _ipaddress = null;

		public Form1()
		{
			InitializeComponent();
		}

		#region UI Events

		private async void Form1_Load(object sender, EventArgs e)
		{
			txtHubUrl.Text = Properties.Settings.Default.txtHubUrl;
			txtSecret.Text = Properties.Settings.Default.txtSecret;
			txtUserName.Text = Properties.Settings.Default.txtUserName;
			txtSubEvents.Text = Properties.Settings.Default.txtSubEvents;
			txtNotTopic.Text = Properties.Settings.Default.txtNotTopic;
			txtNotEvent.Text = Properties.Settings.Default.txtNotEvent;
			txtNotAccession.Text = Properties.Settings.Default.txtNotAccession;
			txtNotMRN.Text = Properties.Settings.Default.txtNotMRN;
			// get Ip address of this client
			UriBuilder urlBuilder = new UriBuilder("http://checkip.dyndns.org");
			HttpClient client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri);
			HttpResponseMessage response = await client.SendAsync(request);
			string responseText = await response.Content.ReadAsStringAsync();
			string[] a = responseText.Split(':');
			string a2 = a[1].Substring(1);
			string[] a3 = a2.Split('<');
			_ipaddress = a3[0];
		}

		private async void btnSubscribe_Click(object sender, EventArgs e)
		{
			// save subscription parameters
			Properties.Settings.Default.txtHubUrl = txtHubUrl.Text;
			Properties.Settings.Default.txtSecret = txtSecret.Text;
			Properties.Settings.Default.txtUserName = txtUserName.Text;
			Properties.Settings.Default.txtSubEvents = txtSubEvents.Text;
			Properties.Settings.Default.Save();
			// get topic
			HttpClient client = new HttpClient();
			UriBuilder urlBuilder = new UriBuilder($"{txtHubUrl.Text}/api/hub/gettopic?username={txtUserName.Text}&secret={txtSecret.Text}");
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri);
			HttpResponseMessage response;
			try
			{
				response = await client.SendAsync(request);
				_topic = await response.Content.ReadAsStringAsync();
				if (String.IsNullOrEmpty(_topic))
				{
					Log($"Validation failed for username: {txtUserName.Text}, secret: {txtSecret.Text}");
					return;
				}
			}
			catch (Exception ex)
			{
				Log($"Exception occurred getting topic:\r\n{ex.ToString()}");
				return;
			}
			Log($"GetTopic response: {_topic}");
			//Subscribe
			urlBuilder = new UriBuilder($"{txtHubUrl.Text}/api/hub");
			request = new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri);
			request.Content = new FormUrlEncodedContent(
				new Dictionary<string, string>
				{
					{ "hub.callback", "" },
					{ "hub.channel.type", "websocket" },
					{ "hub.mode", btnSubscribe.Text.ToLower() },
					{ "hub.topic", _topic },
					{ "hub.events", txtSubEvents.Text },
					{ "hub.lease", "999" },
					{ "hub.secret", "" }	// not to be confused with "app secret". This value isn't currently implementented on the Hub
				}
			);
			response = await client.SendAsync(request);
			string responseText = await response.Content.ReadAsStringAsync();
			Log($"Subscription response: {responseText}");
			if (btnSubscribe.Text == "Subscribe")
			{
				// Connect to websocket
				if (_ws.State != WebSocketState.Open)
				{
					try
					{
						Cursor.Current = Cursors.WaitCursor;
						string wsUrl = $"{txtHubUrl.Text}/{_topic}".Replace("http", "ws").Replace("https", "wss");
						Log($"Connecting to Hub Websocket: {wsUrl}");
						await _ws.ConnectAsync(new Uri(wsUrl), CancellationToken.None);
						// read response
						string conResponse = await ReceiveStringAsync(_ws, CancellationToken.None);
						Log($"Websocket connection received response:\r\n{conResponse}");
						//TODO: validate response. 
						// set up background socket reader and exit
						Cursor.Current = Cursors.Default;
						btnNotify.Enabled = true;
						webSocketReader = new BackgroundWorker();
						webSocketReader.DoWork += webSocketReader_DoWork;
						webSocketReader.RunWorkerAsync();
					}
					catch (Exception ex)
					{
						_ws.Dispose();
						_ws = new ClientWebSocket();
						MessageBox.Show(ex.ToString());
						return;
					}
				}
				else
				{
					Log("WARNING: The websocket was already connected. ");
				}
				btnSubscribe.Text = "Unsubscribe";
			}
			else
			{
				// Disconnect WebSocket
				if (_ws.State == WebSocketState.Open)
				{
					try
					{
						ResetWebSocket();
						webSocketReader.Dispose();
						webSocketReader = new BackgroundWorker();
					}
					catch (Exception ex)
					{
						_ws.Dispose();
						_ws = new ClientWebSocket();
						MessageBox.Show(ex.ToString());
						return;
					}
				}
				else
				{
					Log("WARNING: The websocket was already disconnected. ");
				}
				btnSubscribe.Text = "Subscribe";
				btnNotify.Enabled = false;
			}
		}

		private async void btnNotify_Click(object sender, EventArgs e)
		{
			// save notify parameters
			Properties.Settings.Default.txtNotTopic = txtNotTopic.Text;
			Properties.Settings.Default.txtNotEvent = txtNotEvent.Text;
			Properties.Settings.Default.txtNotAccession = txtNotAccession.Text;
			Properties.Settings.Default.txtNotMRN = txtNotMRN.Text;
			Properties.Settings.Default.Save();

			// Send notification. NOTE: This is a subset of the data allowed by the Hub.
			// See FHIRCast specifications. Additional authentication will also be needed
			string patientGuid = Guid.NewGuid().ToString();
			Notification notification = new Notification
			{
				Timestamp = DateTime.Now,
				Id = Guid.NewGuid().ToString("N"),
				Event = new NotificationEvent()
				{
					HubEvent = txtNotEvent.Text,
					Topic = txtNotTopic.Text,
					Contexts = new Context[]
					{
						new Context()
						{
							Key = "patient",
							Resource = new Resource()
							{
								ResourceType = "Patient",
								Id = patientGuid,
								Identifiers = new Identifier[]
								{
									new Identifier()
									{
										System = "urn:mrn",
										Value= txtNotMRN.Text
									}
								}
							}
						},
						new Context()
						{
							Key = "study",
							Resource = new Resource()
							{
								ResourceType = "ImagingStudy",
								Id = Guid.NewGuid().ToString(),
								Accession = new Identifier
								{
									System = "urn:accession",
									Value = txtNotAccession.Text
								},
								Patient = new ResourceReference
								{
									Reference = $"patient/{patientGuid}"
								}
							}
						}
					}
				}
			};
			string json = notification.ToString();
			byte[] bytes = Encoding.ASCII.GetBytes(json);
			await _ws.SendAsync(new System.ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
			//TODO: process acknowledgement response
		}

		#endregion

		private async void ResetWebSocket()
		{
			await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
			Log("The websocket connection closed sucessfully.");
			_ws = new ClientWebSocket();
			btnSubscribe.Text = "Subscribe";
		}

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
			Log("Websocket reader starting...");
			while (true)
			{
				string socketData = null;
				try
				{
					socketData = await ReceiveStringAsync(_ws, CancellationToken.None);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.ToString());
					Log("Exception occurred reading websocket:\r\n" + ex.Message);
				}
				if (string.IsNullOrEmpty(socketData))
				{
					if (_ws.State != WebSocketState.Open)
					{
						Log("Websocket is closed - the reader is terminating...");
						// dispose of the socket and remove the connection
						ResetWebSocket();
						// TODO: reset UI. Other cleanup?
						break;
					}
					continue;
				}
				// it's either an acknowledgement or an event notification...
				// it's either an acknowledgement or an event notification...
				Notification notification = JsonConvert.DeserializeObject<Notification>(socketData);
				if (null != notification.Event)
				{
					Log($"Event notification received:\r\n{notification.Event}");
					// send success response to client
					WebSocketResponse wsResponse = new WebSocketResponse
					{
						Timestamp = DateTime.Now,
						Status = "OK",
						StatusCode = 200
					};
					await SendStringAsync(_ws, wsResponse.ToString());
				}
				else if (null != notification.Status)
				{
					Log($"Acknowledgement response received:\r\n{notification.Status} ({notification.StatusCode})");
				}
				else
				{
					Log($"Unexpected websocket message received:\r\n{socketData}");
					WebSocketResponse wsResponse = new WebSocketResponse
					{
						Timestamp = DateTime.Now,
						Status = "FAIL",
						StatusCode = 400
					};
					await SendStringAsync(_ws, wsResponse.ToString());
				}
			}
			Log("Websocket reader terminated.");
		}

		private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
		{
			var buffer = Encoding.UTF8.GetBytes(data);
			var segment = new ArraySegment<byte>(buffer);
			return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
		}

		private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
		{
			var buffer = new ArraySegment<byte>(new byte[8192]);
			using (var ms = new MemoryStream())
			{
				WebSocketReceiveResult result;
				do
				{
					ct.ThrowIfCancellationRequested();

					result = await socket.ReceiveAsync(buffer, ct);
					ms.Write(buffer.Array, buffer.Offset, result.Count);
				}
				while (!result.EndOfMessage);

				ms.Seek(0, SeekOrigin.Begin);
				if (result.MessageType != WebSocketMessageType.Text)
				{
					return null;
				}

				// Encoding UTF8: https://tools.ietf.org/html/rfc6455#section-5.6
				using (var reader = new StreamReader(ms, Encoding.UTF8))
				{
					string data = await reader.ReadToEndAsync();
					System.Diagnostics.Debug.WriteLine($"received data: {data}");
					return data;
				}
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
	}
}
