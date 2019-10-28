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
				this.logger.LogInformation($"Sending notification to Rest client at {sub.Callback}:\r\n{notification}");
				var content = new StringContent(JsonConvert.SerializeObject(notification));
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var client = new HttpClient();
				response = await client.PostAsync(sub.Callback, content);
				this.logger.LogDebug($"Got response from posting notification:{Environment.NewLine}{response}{Environment.NewLine}{await response.Content.ReadAsStringAsync()}.");
			}
			else if (sub.Channel.Type == ChannelType.Websocket)
			{
				this.logger.LogInformation($"Sending notification to websocket endpoint {sub.Channel.Endpoint}");
				WebSocket ws = connections.GetConnection(sub.Channel.Endpoint);
				if (null == ws)
				{
					this.logger.LogError("websocket connection not found at endpoint.");
				}
				else
				{
					Notification wsMessage = new Notification
					{
						Timestamp = notification.Timestamp,
						Id = notification.Id,
						Event = notification.Event
					};
					await WebSocketLib.SendStringAsync(ws, JsonConvert.SerializeObject(wsMessage));
					this.logger.LogInformation($"Notification sent:\r\n{wsMessage}");
				}
			}
		}
	}
}
