using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace dotnet.FHIR.hub
{
	public class Subscriptions : ISubscriptions
	{
		private readonly ILogger<Subscriptions> logger;
		private ImmutableHashSet<Subscription> subscriptions = ImmutableHashSet<Subscription>.Empty.WithComparer(Subscription.DefaultComparer);

		public Subscriptions(ILogger<Subscriptions> logger)
		{
			this.logger = logger;
		}

		public ICollection<Subscription> GetActiveSubscriptions()
		{
			return this.subscriptions;
		}
		public Subscription GetSubscription(string channelEndpoint)
		{
			ICollection<Subscription> subs = this.subscriptions
				.Where(s => s.Channel.Endpoint.ToLower() == channelEndpoint.ToLower())
				.ToArray();
			if (subs != null && subs.Count > 0)
			{
				Subscription sub = subs.First();
				this.logger.LogDebug($"GetSubscription found subscription for endpoint {channelEndpoint}");
				return sub;
			}
			else
			{
				this.logger.LogDebug($"GetSubscription: did not find subscription for endpoint {channelEndpoint}");
				return null;
			}
		}

		public ICollection<Subscription> GetSubscriptions(string topic, string notificationEvent)
		{
			this.logger.LogDebug($"Finding subscriptions for topic: {topic} and event: {notificationEvent}");

			ICollection <Subscription> subs = this.subscriptions
				.Where(x => x.Topic == topic)
				.Where(x => x.Events.Split(',').Contains(notificationEvent))
				.ToArray();
			this.logger.LogDebug($"Found {subs.Count} matching subscriptions:");
			foreach(Subscription s in subs)
			{
				this.logger.LogDebug($"Topic: {s.Topic}, Events: {s.Events}, Channel: {s.Channel.Type}, Endpoint: {s.Channel.Endpoint}");
			}
			return subs;
		}

		public void AddSubscription(Subscription subscription)
		{
			this.logger.LogInformation($"Adding subscription for topic {subscription.Topic}.");
			this.subscriptions = this.subscriptions.Add(subscription);
		}

		public void RemoveSubscription(string channelEndpoint)
		{
			this.logger.LogInformation($"Removing subscription at {channelEndpoint}.");
			Subscription sub = GetSubscription(channelEndpoint);
			if (null != sub)
			{
				this.subscriptions = this.subscriptions.Remove(sub);
			}
			else
			{
				this.logger.LogDebug($"RemoveSubscription: subscription not found for endpoint {channelEndpoint}.");
			}
		}
	}
}
