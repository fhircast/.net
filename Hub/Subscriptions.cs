using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace dotnet.FHIR.hub
{
	public class Subscriptions : ISubscriptions
	{
		private readonly ILogger<Subscriptions> logger;
		private Dictionary<string, Subscription> subscriptions = new Dictionary<string, Subscription>();

		public Subscriptions(ILogger<Subscriptions> logger)
		{
			this.logger = logger;
		}

		public List<Subscription> GetActiveSubscriptions()
		{
			return this.subscriptions.Values.ToList<Subscription>();
		}
		public Subscription GetSubscription(string endpoint)
		{
			string allSubs = "";
			int i = 1;
			foreach (string k in this.subscriptions.Keys)
			{
				allSubs +=$"\r\n{i++}. {k}";
			}
			this.logger.LogDebug($"Existing subscription endpoints: {allSubs}");
			Subscription s = null;
			this.subscriptions.TryGetValue(endpoint, out s);
			if (null != s)
			{
				this.logger.LogDebug($"FOUND");
				return s;
			}
			else
			{
				this.logger.LogWarning($"NOT FOUND");
				return null;
			}
		}

		public List<Subscription> GetSubscriptions(string topic, string notificationEvent)
		{
			this.logger.LogDebug($"Finding subscriptions for topic: {topic} and event: {notificationEvent}");

			List<Subscription> subs = this.subscriptions.Values
				.Where(s => s.Topic == topic)
				.Where(s => s.Events.Split(',').Contains(notificationEvent))
				.ToList<Subscription>();
			this.logger.LogDebug($"Found {subs.Count} matching subscriptions");
			return subs;
		}

		public void AddSubscription(Subscription subscription)
		{
			this.logger.LogInformation($"Adding subscription for topic {subscription.Topic}.");
			string endpoint = subscription.Channel.Type == ChannelType.Websocket ? subscription.Channel.Endpoint : subscription.Callback;
			this.subscriptions.Add(endpoint, subscription);
		}

		public void RemoveSubscription(string endpoint)
		{
			this.logger.LogInformation($"Removing subscription at {endpoint}.");
			if (this.subscriptions.Remove(endpoint))
			{
				this.logger.LogDebug($"RemoveSubscription: subscription removed for endpoint {endpoint}.");
			}
			else
			{
				this.logger.LogDebug($"RemoveSubscription: subscription not found for endpoint {endpoint}.");
			}
		}
	}
}
