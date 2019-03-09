using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;

namespace dotnet.FHIR.hub
{
	public class WebSocketConnections : IWebsocketConnections
	{
		private readonly ILogger<Subscriptions> logger;
		private List<WebSocketConnection> connections = new List<WebSocketConnection>(); 

		public WebSocketConnections(ILogger<Subscriptions> logger)
		{
			this.logger = logger;
		}
		public void AddConnection(string topic, WebSocket ws)
		{
			this.connections.Add(new WebSocketConnection(System.DateTime.Now, topic, ws));
		}
		public void RemoveConnection(WebSocket ws)
		{
			foreach (WebSocketConnection wsc in this.connections)
			{
				if (wsc.WebSocket == ws)
					this.connections.Remove(wsc);
			}
			//TODO: remove subscription?
		}
		public List<WebSocketConnection> GetConnections(string topic)
		{
			List<WebSocketConnection> connections = new List<WebSocketConnection>();
			foreach(WebSocketConnection wsc in this.connections)
			{
				if (wsc.Topic == topic)
					connections.Add(wsc);
			}
			return connections;
		}
	}
}
