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
using System.Net.Http.Headers;
using System.Net.Http;

namespace dotnet.FHIR.hub
{
	public class WebSocketMiddleware : IMiddleware
	{
		private readonly ISubscriptions subscriptions;
		private readonly INotifications notifications;
		private readonly IWebsocketConnections connections;
		private readonly ILogger<WebSocketMiddleware> logger;
		private readonly string webSocketProtocol;

		public WebSocketMiddleware(ILogger<WebSocketMiddleware> logger, ISubscriptions subscriptions, INotifications notifications, IWebsocketConnections connections)
		{
			this.subscriptions = subscriptions;
			this.notifications = notifications;
			this.connections = connections;
			this.logger = logger;
#if DEBUG
			this.webSocketProtocol = "ws://";
#else
			this.webSocketProtocol = "wss://";
#endif
		}

		/// <summary>
		/// Websocket connection handler
		/// </summary>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await next(context);
			}
			else
			{
				string socketAddress = $"{context.Connection.RemoteIpAddress.ToString()}/{context.Connection.RemotePort}";
				string endPoint = $"{webSocketProtocol}{context.Request.Host}{context.Request.PathBase}{context.Request.Path.Value}";
				this.logger.LogDebug($"Recieved websocket connection requested from {socketAddress}; end point:{endPoint}.");
				CancellationToken ct = context.RequestAborted;
				// look up the endpoint in our collection of pending subscriptions
				Subscription sub = this.subscriptions.GetSubscription(endPoint);
				if (sub != null)
				{
					WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
					this.logger.LogInformation($"Accepted websocket connection requested from {sub.Channel.Endpoint}; topic:{sub.Topic}.");
					// echo back hub subscription to client as verification intent
					WebSocketMessage vMessage = new WebSocketMessage()
					{
						Timestamp = DateTime.Now,
						Id = Guid.NewGuid().ToString("n"),
						Subscription = sub
					};
					this.logger.LogDebug($"Sending intent verification to {sub.Channel.Endpoint}:\r\n{vMessage}.");
					await WebSocketLib.SendStringAsync(ws, vMessage.ToString());
					// no more acks/nacks
					//// get acknowledgement
					//this.logger.LogDebug("Intent verification sent. Waiting for response..");
					//string socketData = await WebSocketLib.ReceiveStringAsync(ws);
					//WebSocketMessage ack = JsonConvert.DeserializeObject<WebSocketMessage>(socketData);
					//this.logger.LogDebug($"Intent verification response received from {sub.Channel.Endpoint}:\r\n{ack}");
					//if (null != ack.Headers)
					//{
					//	int statusCode = 0;
					//	try
					//	{
					//		statusCode = Convert.ToInt32(ack.Headers["statusCode"]);
					//	}
					//	catch(Exception)
					//	{
					//		this.logger.LogWarning($"invalid status code received from endpoint in response to intent verification at {sub.Channel.Endpoint}");
					//	}
					//	if (statusCode >= 200 && statusCode < 300)
					//	{
					this.logger.LogDebug("successful intent verification response received.");
					this.connections.AddConnection(sub.Channel.Endpoint, ws);
					//	}
					//	else
					//	{
					//		this.logger.LogWarning($"Intent verification not accepted. Removing subscription and closing websocket.");
					//		this.subscriptions.RemoveSubscription(sub.Channel.Endpoint);
					//		ws.Dispose();
					//	}
					//}
					// Loop here until the socket is disconnected - reading and handling
					// each message sent by the client
					while (ws.State == WebSocketState.Open && !context.RequestAborted.IsCancellationRequested)
					{
						//TODO: figure out how to gracefully handle socket closures.
						string socketData = null;
						try
						{
							socketData = await WebSocketLib.ReceiveStringAsync(ws, new CancellationToken());
						}
						catch (Exception ex)
						{
							this.logger.LogError($"Exception occurred reading from websocket at {sub.Channel.Endpoint}:\r\n{ex.Message}.");
						}
						if (string.IsNullOrEmpty(socketData))
						{
							this.logger.LogDebug($"The websocket reader at {sub.Channel.Endpoint} read an empty packet. Socket state: {ws.State}, CancelRequested: {context.RequestAborted.IsCancellationRequested}, Closing status: {ws.CloseStatus}/{ws.CloseStatusDescription}");
						}
						else
						{
							WebSocketMessage nMessage = null;
							try
							{
								nMessage = JsonConvert.DeserializeObject<WebSocketMessage>(socketData);
							}
							catch(Exception)
							{
								this.logger.LogError($"The websocket reader at {sub.Channel.Endpoint} received an invalid event notification message:\r\n{socketData}");
								continue;
							}
							//WebSocketMessage response = new WebSocketMessage
							//{
							//	Headers = new Dictionary<string, string>
							//	{
							//		{ "status" , "OK" },
							//		{ "statusCode", "200" }
							//	}
							//};
							NotificationEvent ev = null;
							if (null != nMessage.Event)
							{
								// process message, it can only be a event notification 
								ev = nMessage.Event;
							}
							if (null == ev)
							{
								this.logger.LogError($"Received invalid event notification message:\r\n{socketData}");
							}
							else
							{
								this.logger.LogDebug($"Event notification received:\r\n{ev}");
								// Forward notifications to Websocket connected subscribers
								Notification n = null;
								try
								{
									n = new Notification
									{
										Timestamp = nMessage.Timestamp,
										Id = nMessage.Id,
										Event = ev
									};
								}
								catch (Exception ex)
								{
									this.logger.LogError($"Unexpected exception processing inbound notification event:\r\n{ex.ToString()}");
									//response = new WebSocketMessage
									//{
									//	Headers = new Dictionary<string, string>
									//	{
									//		{ "Status" , "INVALID" },
									//		{ "StatusCode", "400" }
									//	}
									//};
									continue;
								}
								var subs = this.subscriptions.GetSubscriptions(ev.Topic, ev.HubEvent);
								foreach (var s in subs)
								{
									await notifications.SendNotification(n, s);
								}
							}
							//await WebSocketLib.SendStringAsync(ws, response.ToString()); // todo: process response
						}
					}
					// Remove the subscription and websocket connection if it has not already been done by the hub
					// controller due to an unsubscribe request from the client.
					this.connections.RemoveConnection(sub.Channel.Endpoint);	
					this.subscriptions.RemoveSubscription(sub.Channel.Endpoint);
				}
				else
				{
					this.logger.LogWarning($"Rejected websocket connection from {socketAddress}. A Subscription not found for endpoint {endPoint}.");
				}
			}
		}
	}
}
