using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dotnet.FHIR.common
{
	public sealed class ChannelType
	{
		public static readonly string Rest = "rest-hook";
		public static readonly string Websocket = "websocket";
	}

	public sealed class SubscriptionMode
	{
		public static readonly string Subscribe = "subscribe";
		public static readonly string Unsubscribe = "unsubscribe";
	}

	public abstract class ModelBase
	{
		public override string ToString()
		{
			JsonSerializerSettings s = new JsonSerializerSettings();
			s.NullValueHandling = NullValueHandling.Ignore;
			return JsonConvert.SerializeObject(this, Formatting.Indented, s);
		}
	}

	public class Subscription : ModelBase
	{
		public static IEqualityComparer<Subscription> DefaultComparer => new SubscriptionComparer();

		[JsonProperty(PropertyName = "hub.channel")]
		public Channel Channel { get; set; }

		[JsonProperty(PropertyName = "hub.secret")]
		public string Secret { get; set; }

		[JsonProperty(PropertyName = "hub.callback")]
		public string Callback { get; set; }

		[JsonProperty(PropertyName = "hub.mode")]
		public string Mode { get; set; }

		[JsonProperty(PropertyName = "hub.topic")]
		public string Topic { get; set; }

		[JsonProperty(PropertyName = "hub.events")]
		public string Events { get; set; }

		//[JsonProperty(PropertyName = "hub.lease_seconds")]
		public int? LeaseSeconds { get; set; }

		//[JsonIgnore]
		public TimeSpan? Lease => this.LeaseSeconds.HasValue ? TimeSpan.FromSeconds(this.LeaseSeconds.Value) : (TimeSpan?)null;
	}

	public sealed class Channel
	{
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "endpoint")]
		public string Endpoint { get; set; }
	}

	public sealed class SubscriptionCancelled : Subscription
	{
		//[JsonProperty(PropertyName = "hub.reason")]
		public string Reason { get; set; }
	}

	public sealed class Notification : ModelBase
	{
		[JsonProperty(PropertyName = "timestamp")]
		public DateTime Timestamp { get; set; }

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "event")]
		public NotificationEvent Event { get; set; }
	}

	public sealed class NotificationEvent : ModelBase
	{
		[JsonProperty(PropertyName = "hub.topic")]
		public string Topic { get; set; }

		[JsonProperty(PropertyName = "hub.event")]
		public string HubEvent { get; set; }

		[JsonProperty(PropertyName = "context")]
		public List<Context> Contexts { get; set; }
	}

	public sealed class Context
	{
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "resource")]
		public object Resource { get; set; }    // FHIR resource  
	}

	public class SubscriptionComparer : IEqualityComparer<Subscription>
	{
		public bool Equals(Subscription sub1, Subscription sub2)
		{
			return sub1.Topic == sub2.Topic;
		}

		public int GetHashCode(Subscription subscription)
		{
			return subscription.Topic.GetHashCode();
		}
	}
}
