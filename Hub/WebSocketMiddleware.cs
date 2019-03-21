using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace dotnet.FHIR.hub
{
	public class WebSocketMiddleware : IMiddleware
	{
		private readonly ISubscriptions subscriptions;
		private readonly INotifications notifications;
		private readonly IWebsocketConnections connections;
		private readonly ILogger<WebSocketMiddleware> logger;

		public WebSocketMiddleware(ILogger<WebSocketMiddleware> logger, ISubscriptions subscriptions, INotifications notifications, IWebsocketConnections connections)
		{
			this.subscriptions = subscriptions;
			this.notifications = notifications;
			this.logger = logger;
			this.connections = connections;
		}

		/// <summary>
		/// Webservice thread that reads messages from each websocket connect
		/// BIG TODO: process acknowledgements by using some sort of notification queue 
		/// of outbound notifications.
		/// </summary>
		/// <param name="sender">TODO: this needs to contain something that will identify the websocket
		/// in order to handle acknowledgements and take the notification off the queue.</param>
		/// <param name="e"></param>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await next(context);
			}
			else
			{
				string path = context.Request.Path.Value;
				string[] args = String.IsNullOrEmpty(path) ? null : path.TrimStart('/').Split('/');
				CancellationToken ct = context.RequestAborted;
				WebSocket ws = null;
				if (null != args && args.Length == 1)
				{
					// accept socket request
					ws = await context.WebSockets.AcceptWebSocketAsync();
					string topic = args[0];
					this.logger.LogDebug($"Websocket connection requested; topic:{topic}.");
					// validate topic, and get user name from the database configuration
					//TODO
					bool validated = true;
					if (!validated)
					{
						this.logger.LogInformation("Topic not validated. Message rejected.");
						WebSocketResponse response = new WebSocketResponse
						{
							Timestamp = DateTime.Now,
							Status = "FAIL",
							StatusCode = 400
						};
						await SendStringAsync(ws, response.ToString());
					}
					else
					{
						this.logger.LogInformation($"Accepted websocket request: Topic {topic}, IP: {context.Connection.RemoteIpAddress.ToString()}:{context.Connection.RemotePort}");
						// store this websocket connection in our dictionary
						this.connections.AddConnection(topic, ws);
						// send success response to client
						WebSocketResponse response = new WebSocketResponse
						{
							Timestamp = DateTime.Now,
							Status = "OK",
							StatusCode = 200
						};
						await SendStringAsync(ws, response.ToString());
						// Loop here until the socket is disconnected - reading and handling
						// each message sent by the client
						while (true)
						{
							string socketData = null;
							try
							{
								socketData = await ReceiveStringAsync(ws, ct);
							}
							catch(Exception ex)
							{
								this.logger.LogError($"Exception occurred reading from websocket:\r\n{ex.ToString()}");
							}
							if (string.IsNullOrEmpty(socketData))
							{
								if (ws.State != WebSocketState.Open)
								{
									this.logger.LogError($"The websocket connection on port {context.Connection.RemotePort} is closed. Terminating this subscription...");
									break;
								}
								continue;
							}
							// it's either an acknowledgement or an event notification...
							Notification notification = JsonConvert.DeserializeObject<Notification>(socketData);
							if (null != notification.Event)
							{
								this.logger.LogInformation($"Event notification received:\r\n{notification.Event.ToString()}");
								// send success response to client
								WebSocketResponse wsResponse = new WebSocketResponse
								{
									Timestamp = DateTime.Now,
									Status = "OK",
									StatusCode = 200
								};
								await SendStringAsync(ws, wsResponse.ToString());
								// Forward notifications to Websocket connected subscribers
								var subs = this.subscriptions.GetSubscriptions(notification.Event.Topic, notification.Event.HubEvent);
								foreach (var sub in subs)
								{
									await this.notifications.SendNotification(notification, sub);
								}
							}
							else if (null != notification.Status)
							{
								this.logger.LogInformation($"Acknowledgement response received:\r\n{notification.Status} ({notification.StatusCode})");
							}
							else
							{
								this.logger.LogError($"Unexpected websocket message received:\r\n{response}");
							}
						}
						await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
						//TODO: remove subscription
						ws.Dispose();
					}
				}
				else
				{
					await next(context);
				}
			}
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
				using (var reader = new StreamReader(ms, Encoding.UTF8))
				{
					string data = await reader.ReadToEndAsync();
					System.Diagnostics.Debug.WriteLine($"received data: {data}");
					return data;
				}
			}
		}
	}
}
