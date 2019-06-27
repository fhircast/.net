using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using System.Net.WebSockets;
using System;

namespace dotnet.FHIR.hub
{
	public class WebSocketConnections : IWebsocketConnections
	{
		private readonly ILogger<Subscriptions> logger;
		private readonly ISubscriptions subscriptions;
		private ConcurrentDictionary<string, WebSocket> connections = new ConcurrentDictionary<string, WebSocket>(); 

		public WebSocketConnections(ILogger<Subscriptions> logger, ISubscriptions subscriptions)
		{
			this.logger = logger;
			this.subscriptions = subscriptions;
		}
		public void AddConnection(string channelEndpoint, WebSocket ws)
		{
			if (!this.connections.TryAdd(channelEndpoint, ws))
			{
				this.logger.LogWarning($"WebSocketConnections.AddConnection failed for endpoint {channelEndpoint}.");
			}
		}
		public void RemoveConnection(string channelEndpoint)
		{
			WebSocket ws = null;
			if (!this.connections.TryRemove(channelEndpoint, out ws))
			{
				this.logger.LogWarning($"WebSocketConnections.RemoveConnection failed for endpoint {channelEndpoint}.");
			}
			else
			{
				//ws.Dispose();
			}
		}
		public WebSocket GetConnection(string channelEndpoint)
		{
			WebSocket ws = null;
			if (!this.connections.TryGetValue(channelEndpoint, out ws))
			{
				this.logger.LogWarning($"WebSocketConnections.GetConnection failed for endpoint {channelEndpoint}.");
			}
			return ws;
		}
	}
}
