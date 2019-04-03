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
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await next(context);
			}
			else
			{
				string path = context.Request.Path.Value;
				string socketAddress = $"{context.Connection.RemoteIpAddress.ToString()}/{context.Connection.RemotePort}";
				string[] args = String.IsNullOrEmpty(path) ? null : path.TrimStart('/').Split('/');
				CancellationToken ct = context.RequestAborted;
				WebSocket ws = null;
				if (null != args && args.Length == 1)
				{
					// accept socket request
					ws = await context.WebSockets.AcceptWebSocketAsync();
					string topic = args[0];
					this.logger.LogDebug($"Websocket connection requested from {socketAddress}; topic:{topic}.");
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
						this.logger.LogInformation($"Accepted websocket request from {socketAddress}: Topic {topic}");
						// store this websocket connection in our dictionary
						this.connections.AddConnection(socketAddress, topic, ws);
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
							//TODO: have a semaphore or flag to stop loop
							string socketData = null;
							try
							{
								socketData = await ReceiveStringAsync(ws, new CancellationToken());
							}
							catch (Exception ex)
							{
								this.logger.LogError($"Exception occurred reading from websocket at {socketAddress}:\r\n{ex.ToString()}");
							}
							if (string.IsNullOrEmpty(socketData))
							{
								this.logger.LogDebug($"The websocket connection at {socketAddress} closed.");
								break;
							}
							else
							{
								// it's either an acknowledgement or an event notification...
								WebSocketMessage nMessage = JsonConvert.DeserializeObject<WebSocketMessage>(socketData);
								if (null != nMessage.Body)
								{
									// process message
									// right now, it can only be a event notification 
									WebSocketResponse wsResponse = new WebSocketResponse
									{
										Timestamp = DateTime.Now,
										Status = "OK",
										StatusCode = 200
									};
									// todo: process header
									MessageBody body = nMessage.Body;
									NotificationEvent ev = body.Event;
									if (null != ev)
									{
										this.logger.LogDebug($"Event notification received:\r\n{ev}");
										// Forward notifications to Websocket connected subscribers
										try
										{
											Notification n = new Notification
											{
												Timestamp = body.Timestamp,
												Id = body.Id,
												Event = body.Event
											};
											var subs = this.subscriptions.GetSubscriptions(ev.Topic, ev.HubEvent);
											foreach (var sub in subs)
											{
												await this.notifications.SendNotification(n, sub);
											}
										}
										catch (Exception ex)
										{
											this.logger.LogError($"Unexpected exception processing inbound notification event:\r\n{ex.ToString()}");
											wsResponse.Status = "INVALID";
											wsResponse.StatusCode = 400;
										}
									}
									else
									{
										this.logger.LogError($"Received invalid event notification message:\r\n{socketData}");
									}
									await SendStringAsync(ws, wsResponse.ToString()); // todo: process response
								}
								else
								{
									// should be an ack response
									WebSocketResponse ack = JsonConvert.DeserializeObject<WebSocketResponse>(socketData);
									if (null == ack.Status)
									{
										this.logger.LogError($"Received invalid websocket message:\r\n{socketData}");
									}
									else
									{
										//todo: handle ack
										this.logger.LogDebug($"Received acknowledgement response:\r\n{socketData}");
									}
								}
							}
						}
						this.logger.LogDebug($"The websocket connection thread for {socketAddress} is terminating. Removing WebsocketConnection...");
						if (null != ws && ws.State == WebSocketState.Open)
							await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", new CancellationToken());
						this.connections.RemoveConnection(socketAddress);
						ws.Dispose();
						this.logger.LogDebug("InvokeAsync returning.");
					}
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
