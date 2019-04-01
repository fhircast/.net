using dotnet.FHIR.common;
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
				this.logger.LogInformation($"Sending notification {notification} to Websocket {sub.Channel. Endpoint}");
				List<WebSocket> connections = this.connections.GetTopicConnections(sub.Topic);
				int conCount = connections.Count;
				if (conCount == 0)
				{
					this.logger.LogError($"Websocket connection not found for topic: {sub.Topic}");
				}
				else
				{
					this.logger.LogInformation($"Sending notification {conCount} websockets...");
					foreach (WebSocket ws in connections)
					{
						WebSocketMessage wsMessage = new WebSocketMessage
						{
							Header = { },
							Body = new MessageBody
							{
								Timestamp = notification.Timestamp,
								Id = notification.Id,
								Event = notification.Event
							}
						};
						var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(wsMessage));
						var segment = new ArraySegment<byte>(buffer);
						await ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
						this.logger.LogInformation($"Notification sent successfully.");
					}
				}
			}
		}
	}
}
