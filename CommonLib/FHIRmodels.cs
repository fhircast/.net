using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dotnet.FHIR.common
{
	#region string constants classes
	public sealed class HubEventType
	{
		public static readonly string OpenImagingStudy = "open-imaging-study";
		public static readonly string SwitchImagingStudy = "switch-imaging-study";
		public static readonly string CloseImagingStudy = "close-imaging-study";
		public static readonly string UserLogout = "user-logout";
	}

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
	#endregion

	public abstract class ModelBase
	{
		public override string ToString()
		{
			JsonSerializerSettings s = new JsonSerializerSettings();
			s.NullValueHandling = NullValueHandling.Ignore;
			return JsonConvert.SerializeObject(this, s);
		}
	}

	public class Subscription : ModelBase
	{
		public static IEqualityComparer<Subscription> DefaultComparer => new SubscriptionComparer();

		[JsonProperty(PropertyName = "hub.channel")]
		public Channel Channel { get; set; }

		[JsonIgnore]
		public string HubURL { get; set; }

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
	}

	public sealed class Channel
	{
		//[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		//[JsonProperty(PropertyName = "endpoint")]
		public string Endpoint { get; set; }
	}

	public abstract class SubscriptionWithLease : Subscription
	{
		//[JsonProperty(PropertyName = "hub.lease_seconds")]
		public int? LeaseSeconds { get; set; }

		//[JsonIgnore]
		public TimeSpan? Lease => this.LeaseSeconds.HasValue ? TimeSpan.FromSeconds(this.LeaseSeconds.Value) : (TimeSpan?)null;
	}

	public sealed class SubscriptionVerification : SubscriptionWithLease
	{
		//[JsonProperty(PropertyName = "hub.challenge")]
		public string Challenge { get; set; }
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

		[JsonProperty(PropertyName = "status")]
		public string Status { get; set; }

		[JsonProperty(PropertyName = "statuscode")]
		public int StatusCode { get; set; }
	}

	public sealed class WebSocketResponse : ModelBase
	{
		[JsonProperty(PropertyName = "timestamp")]
		public DateTime Timestamp { get; set; }

		[JsonProperty(PropertyName = "status")]
		public string Status { get; set; }

		[JsonProperty(PropertyName = "statuscode")]
		public int StatusCode { get; set; }
	}

	public class WebSocketConnection : ModelBase
	{
		public WebSocketConnection(DateTime timeStamp, string topic, System.Net.WebSockets.WebSocket ws)
		{
			this.TimeStamp = timeStamp;
			this.Topic = topic;
			this.WebSocket = ws;
		}
		public DateTime TimeStamp;
		public string Topic;
		public System.Net.WebSockets.WebSocket WebSocket;
	}

	public sealed class NotificationEvent : ModelBase
	{
		[JsonProperty(PropertyName = "hub.topic")]
		public string Topic { get; set; }

		[JsonProperty(PropertyName = "hub.event")]
		public string HubEvent { get; set; }

		[JsonProperty(PropertyName = "context")]
		public Context[] Contexts { get; set; }
	}

	public sealed class Context : ModelBase
	{
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "resource")]
		public Resource Resource { get; set; }
	}

	// Can be a Patient Resource or ImagingStudy resource
	// TODO: create subclasses?
	public sealed class Resource : ModelBase
	{
		[JsonProperty(PropertyName = "resourceType")]
		public string ResourceType { get; set; }

		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "identifier")]
		public Identifier[] Identifiers { get; set; }

		[JsonProperty(PropertyName = "uid")]
		public string Uid { get; set; }

		[JsonProperty(PropertyName = "patient")]
		public ResourceReference Patient { get; set; }

		[JsonProperty(PropertyName = "accession")]
		public Identifier Accession { get; set; }
	}

	public class Identifier : ModelBase
	{
		[JsonProperty(PropertyName = "use")]
		public string Use { get; set; }

		[JsonProperty(PropertyName = "type")]
		public Coding Type { get; set; }

		[JsonProperty(PropertyName = "system")]
		public string System { get; set; }

		[JsonProperty(PropertyName = "value")]
		public string Value { get; set; }
	}

	public sealed class CodeableConcept : ModelBase
	{
		[JsonProperty(PropertyName = "coding")]
		public Coding Coding;

		[JsonProperty(PropertyName = "text")]
		public Coding Text;
	}

	public sealed class Coding : ModelBase
	{
		[JsonProperty(PropertyName = "system")]
		public string system { get; set; }

		[JsonProperty(PropertyName = "version")]
		public string version { get; set; }

		[JsonProperty(PropertyName = "code")]
		public string Code { get; set; }

		[JsonProperty(PropertyName = "display")]
		public string Display { get; set; }

		[JsonProperty(PropertyName = "userSelected")]
		public bool UserSelected { get; set; }
	}

	public sealed class ResourceReference : ModelBase
	{
		[JsonProperty(PropertyName = "reference")]
		public string Reference { get; set; }
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
