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
		private ClientWebSocket _ws = null;
		private string _endpoint = null;
		private BackgroundWorker _webSocketReader;
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
			txtToken.Text = Properties.Settings.Default.txtToken;
			txtSecret.Text = Properties.Settings.Default.txtSecret;
			txtTopic.Text = Properties.Settings.Default.txtTopic;
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
			Properties.Settings.Default.txtToken = txtToken.Text;
			Properties.Settings.Default.txtTopic = txtTopic.Text;
			Properties.Settings.Default.txtSubEvents = txtSubEvents.Text;
			Properties.Settings.Default.Save();
			HttpClient client = new HttpClient();
			HttpResponseMessage response;
			//Subscribe/Unsubscribe
			_topic = txtTopic.Text; 
			UriBuilder urlBuilder = new UriBuilder($"{txtHubUrl.Text}/api/hub");
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, urlBuilder.Uri);
			//request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", txtToken.Text);
			Dictionary<string, string> hub = new Dictionary<string, string>
			{
				{ "hub.channel.type", "websocket" },
				{ "hub.mode",  btnSubscribe.Text.ToLower()},
				{ "hub.topic", _topic },
				{ "hub.events", txtSubEvents.Text },
				{ "hub.secret", txtSecret.Text },
				{ "hub.lease", "999" },
			};
			if (btnSubscribe.Text == "Unsubscribe")
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
			else if (btnSubscribe.Text == "Unsubscribe")
			{
				Log($"{btnSubscribe.Text} request was accepted. Closing web socket and terminating background thread.");
				btnSubscribe.Text = "Subscribe";
				try
				{
					await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "client unsubscribing from topic", CancellationToken.None);
					_ws.Dispose();
				}
				catch(Exception ex)
				{
					Log($"Exception occurred closing the websocket: {ex.Message}");
				}
				_endpoint = null;
			}
			else
			{
				_endpoint = await response.Content.ReadAsStringAsync();
				Log($"Hub returned websocket URL: {_endpoint}");
				// Connect to websocket
				try
				{
					Cursor.Current = Cursors.WaitCursor;
					Log($"Connecting to Hub Websocket: {_endpoint}");
					_ws = new ClientWebSocket();
					await _ws.ConnectAsync(new Uri(_endpoint), CancellationToken.None);
					// read intent verification
					string verification = await WebSocketLib.ReceiveStringAsync(_ws, CancellationToken.None);
					Log($"Websocket connection received intent verification:\r\n{verification}");
					//TODO: further discussionwill be held by FHIRCast team to decide if this is necessary
					//WebSocketMessage intentAck = new WebSocketMessage
					//{
					//	Headers = new Dictionary<string, string>
					//	{
					//		{ "status", "ACCEPTED" },
					//		{ "statusCode", "202" }
					//	}
					//};
					//await WebSocketLib.SendStringAsync(_ws, intentAck.ToString());
					// set up background socket reader and exit
					Cursor.Current = Cursors.Default;
					btnNotify.Enabled = true;
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
			WebSocketMessage notificationMessage = new WebSocketMessage
			{
				Timestamp = DateTime.Now,
				Id = $"TestApp-{Guid.NewGuid().ToString("N")}",
				Event = new NotificationEvent()
				{
					HubEvent = txtNotEvent.Text,
					Topic = _topic,
					Contexts = new Context[]
					{
						new Context()
						{
							Key = "patient",
							Resource = new Resource()
							{
								ResourceType = "Patient",
								Id = patientGuid,
								Identifier = new Identifier[]
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
								Id = $"Acc-{txtNotAccession.Text}",
								Identifier = new Identifier[]
								{
									new Identifier()
									{
										System = "urn:accession",
										Value = txtNotAccession.Text
									}
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
			string json = notificationMessage.ToString();
			byte[] bytes = Encoding.ASCII.GetBytes(json);
			await _ws.SendAsync(new System.ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
			//TODO: process acknowledgement response
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
					break;
				}
				int len = null == socketData ? 0 : socketData.Length;
				if (string.IsNullOrEmpty(socketData))
				{
					Log($"Websocket read empty packet. State: {_ws.State}, Closing status: {_ws.CloseStatus}/{_ws.CloseStatusDescription}.");
					//try
					//{
					//	_ws.Dispose();
					//}
					//catch (Exception ex)
					//{
					//	Log($"An error occurred disposing the websocket: {ex.Message}");
					//}
					//finally
					//{
						_ws = null;
					//}
				}
				else
				{
					Log($"{len} bytes read - processing message...");
					WebSocketMessage nMessage = JsonConvert.DeserializeObject<WebSocketMessage>(socketData);
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
						// no acks
						//wsResponse = new WebSocketMessage
						//{
						//	Headers = new Dictionary<string, string>
						//{
						//	{ "status" , "INVALID" },
						//	{ "statusCode", "400" }
						//}
						//};
					}
					//await WebSocketLib.SendStringAsync(_ws, wsResponse.ToString());
					//else
					//{
					//	// should be a response
					//	WebSocketMessage ack = JsonConvert.DeserializeObject<WebSocketMessage>(socketData);
					//	Log($"Received acknowledgement response:\r\n{socketData}");
					//}
				}
			}
			Log("Websocket reader terminated.");
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
	}
}
