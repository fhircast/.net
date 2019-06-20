﻿using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Net.WebSockets;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace dotnet.FHIR.hub
{
	public class Notifications : INotifications
	{
		private ILogger<Notifications> logger;
		private readonly IWebsocketConnections connections;

		public Notifications(ILogger<Notifications> logger, IWebsocketConnections connections)
		{
			this.logger = logger;
			this.connections = connections;
		}

		public async Task SendNotification(Notification notification, Subscription sub)
		{
			HttpResponseMessage response;
			if (sub.Channel.Type == ChannelType.Rest)
			{
				this.logger.LogInformation($"Sending notification {notification} to callback {sub.Callback}");
				var content = new StringContent(JsonConvert.SerializeObject(notification));
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var client = new HttpClient();
				response = await client.PostAsync(sub.Callback, content);
				this.logger.LogDebug($"Got response from posting notification:{Environment.NewLine}{response}{Environment.NewLine}{await response.Content.ReadAsStringAsync()}.");
			}
			else if (sub.Channel.Type == ChannelType.Websocket)
			{
				this.logger.LogInformation($"Sending notification {notification} to topic {sub.Topic}");
				List<WebSocket> conList = this.connections.GetTopicConnections(sub.Topic, notification.Event.HubEvent);
				int conCount = conList.Count;
				if (conCount == 0)
				{
					this.logger.LogError($"Websocket connection not found for topic {sub.Topic}, event {notification.Event.HubEvent}");
				}
				else
				{
					this.logger.LogInformation($"Sending notification {conCount} websockets...");
					foreach (WebSocket ws in conList)
					{
						WebSocketMessage wsMessage = new WebSocketMessage
						{
							Timestamp = notification.Timestamp,
							Id = notification.Id,
							Event = notification.Event
						};
						await WebSocketLib.SendStringAsync(ws, JsonConvert.SerializeObject(wsMessage));
						this.logger.LogInformation($"Notification sent.");
						// TODO: replace message acknowledgements with a more comprehensive error handling mechanism
						//string r = await WebSocketLib.ReceiveStringAsync(ws);
						//WebSocketMessage ack = JsonConvert.DeserializeObject<WebSocketMessage>(r);
						//this.logger.LogInformation($"Notification response:\r\n{ack}");
						//if (null != ack.Headers)
						//{
						//	int statusCode = 0;
						//	try
						//	{
						//		statusCode = Convert.ToInt32(ack.Headers["statusCode"]);
						//	}
						//	catch (Exception)
						//	{
						//		this.logger.LogWarning($"invalid status code received from endpoint in response to notification to {sub.Channel.Endpoint}");
						//	}
						//	if (statusCode >= 200 && statusCode < 300)
						//	{
						//		this.logger.LogDebug("Notification response accepted.");
						//	}
						//	else
						//	{
						//		this.logger.LogWarning($"Notification message not accepted.");
						//	}
						//}
					}
				}
			}
		}
	}
}
