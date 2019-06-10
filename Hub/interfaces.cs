using dotnet.FHIR.common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace dotnet.FHIR.hub
{
	public interface ISubscriptionValidator
	{
		Task<ClientValidationOutcome> ValidateSubscription(Subscription subscription);
	}

	public interface ISubscriptions
	{
		Subscription GetSubscription(string channelEndpoint);
		ICollection<Subscription> GetActiveSubscriptions();
		void AddSubscription(Subscription subscription);
		void RemoveSubscription(string channelEndpoint);
		ICollection<Subscription> GetSubscriptions(string topic, string notificationEvent);
	}

	public interface INotifications
	{
		Task SendNotification(Notification notification, Subscription subscription);
	}

	public interface IWebsocketConnections
	{
		void AddConnection(string channelEndpoint, WebSocket ws);
		WebSocket GetConnection(string channelEndpoint);
		void RemoveConnection(string channelEndpoint);
		List<WebSocket> GetTopicConnections(string topic, string notificationEvent);
	}
}
