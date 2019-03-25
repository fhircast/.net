using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System;

namespace dotnet.FHIR.hub
{

	public class WebSocketConnection : IEqualityComparer<WebSocketConnection>
	{
		public WebSocketConnection(string id, DateTime timeStamp, string topic, System.Net.WebSockets.WebSocket ws)
		{
			this.TimeStamp = timeStamp;
			this.Topic = topic;
			this.WebSocket = ws;
		}
		public string ID;
		public DateTime TimeStamp;
		public string Topic;
		public System.Net.WebSockets.WebSocket WebSocket;

		public bool Equals(WebSocketConnection x, WebSocketConnection y)
		{
			return x.ID == y.ID;
		}

		public int GetHashCode(WebSocketConnection obj)
		{
			WebSocketConnection wsc = (WebSocketConnection)obj;
			return wsc.ID.GetHashCode();
		}
	}

	public class WebSocketConnections : IWebsocketConnections
	{
		private readonly ILogger<Subscriptions> logger;
		private ConcurrentDictionary<string, WebSocketConnection> connections = new ConcurrentDictionary<string, WebSocketConnection>(); 

		public WebSocketConnections(ILogger<Subscriptions> logger)
		{
			this.logger = logger;
		}
		public void AddConnection(string id,string topic, WebSocket ws)
		{
			this.connections.TryAdd(id, new WebSocketConnection(id, DateTime.Now, topic, ws));
			//TODO: handle possible duplicates???
		}
		public void RemoveConnection(string id)
		{
			WebSocketConnection wsc;
			this.connections.TryRemove(id, out wsc);
		}
		public WebSocketConnection GetConnection(string id)
		{
			WebSocketConnection wsc = null;
			this.connections.TryGetValue(id, out wsc);
			return wsc;
		}

		public List<WebSocket> GetTopicConnections(string topic)
		{
			List<WebSocket> sockets = new List<WebSocket>();
			foreach (var obj in this.connections)
			{
				WebSocketConnection wsc = (WebSocketConnection)obj.Value;
				if (wsc.Topic == topic)
					sockets.Add(wsc.WebSocket);
			}
			return sockets;
		}
	}
}
