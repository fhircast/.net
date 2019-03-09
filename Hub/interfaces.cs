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
		ICollection<Subscription> GetActiveSubscriptions();
		void AddSubscription(Subscription subscription);
		void RemoveSubscription(Subscription subscription);
		ICollection<Subscription> GetSubscriptions(string topic, string notificationEvent);
	}

	public interface INotifications
	{
		Task SendNotification(Notification notification, Subscription subscription);
	}

	public interface IWebsocketConnections
	{
		void AddConnection(string topic, WebSocket ws);
		void RemoveConnection(WebSocket ws);
		List<WebSocketConnection> GetConnections(string topic);
	}
}
