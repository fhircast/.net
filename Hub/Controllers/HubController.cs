using dotnet.FHIR.common;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net.WebSockets;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace dotnet.FHIR.hub.Controllers
{
	[Route("api/[controller]")]
	public class HubController : Controller
	{
		private readonly ILogger<HubController> logger;
		private readonly IBackgroundJobClient backgroundJobClient;
		private readonly ISubscriptions subscriptions;
		private readonly INotifications notifications;
		private readonly IConfiguration settings;

		private string connectionString;
		private string webSocketProtocol;

		public HubController(ILogger<HubController> logger, IBackgroundJobClient backgroundJobClient, ISubscriptions subscriptions, INotifications notifications, IConfiguration config)
		{
			this.backgroundJobClient = backgroundJobClient;
			this.logger = logger;
			this.subscriptions = subscriptions;
			this.notifications = notifications;
			this.settings = config;
#if DEBUG
			this.connectionString = settings.GetConnectionString("Development");
			this.webSocketProtocol = "ws://";
#else
			this.connectionString = settings.GetConnectionString("Default");
			this.webSocketProtocol = "wss://";
#endif
			this.logger.LogInformation($"Using database connection string: {this.connectionString}");
		}

		#region REST Methods

		[HttpPost]
		public IActionResult Subscribe()
		{
			this.logger.LogDebug($"Subscribe...");
			Subscription sub = new Subscription();
			sub.Callback = Request.Form["hub.callback"];
			sub.Channel = new Channel();
			sub.Channel.Type = Request.Form["hub.channel.type"];
			sub.Channel.Endpoint = Request.Form["hub.channel.endpoint"];
			sub.Events = Request.Form["hub.events"];
			sub.Mode = Request.Form["hub.mode"];
			sub.Secret = Request.Form["hub.secret"];
			sub.Topic = Request.Form["hub.topic"];
			this.logger.LogDebug($"Subscribe; subscription:{Environment.NewLine}{sub}");
			if (null == sub.Channel.Type)
			{
				sub.Channel.Type = ChannelType.Rest;
			}
			if (sub.Channel.Type.ToLower() == ChannelType.Websocket)
			{
				this.logger.LogInformation($"Processing {sub.Mode} request for websocket on topic {sub.Topic}.");
				if (sub.Mode == SubscriptionMode.Subscribe)
				{
					string guid = Guid.NewGuid().ToString("n");
					string wsUrl = $"{this.webSocketProtocol}{this.HttpContext.Request.Host}/ws/{sub.Topic}_{guid}";
					sub.Channel.Endpoint = wsUrl;
					this.subscriptions.AddSubscription(sub);
					this.logger.LogDebug($"Subscription added pending intent verification. Returning websocket url: {wsUrl}");
					return this.Accepted((object)wsUrl);	// Needs to be cast as object so that it is put into the body
				}
				else
				{
					this.logger.LogInformation($"Removing subscription at {sub.Channel.Endpoint}.");
					this.subscriptions.RemoveSubscription(sub.Channel.Endpoint);
					return this.Accepted();
				}
			}
			else
			{
				this.logger.LogDebug($"Enqueuing background job to perform callback...");
				this.backgroundJobClient.Enqueue<ValidateSubscriptionJob>(job => job.Run(sub));
				return this.Accepted();
			}
		}

		/// <summary>
		/// Gets all active subscriptions. This is for diagnostic purposes only. It should not be available
		/// to consumers.
		/// </summary>
		/// <returns>All active subscriptions.</returns>
		[HttpGet("GetSubscriptions")]
		public IEnumerable<Subscription> GetSubscriptions()
		{
			this.logger.LogDebug($"Default method: GetSubscriptions...");
			ICollection<Subscription> subs = this.subscriptions.GetActiveSubscriptions();
			this.logger.LogInformation($"{subs.Count} active subscriptions found.");
			foreach(Subscription sub in subs)
			{
				this.logger.LogDebug($"Subscription:\r\n\t{sub}");
			}
			return subs;
		}

		[HttpPost("{topic}")]
		public async Task<IActionResult> Notify(string topic, [FromBody] Notification notification)
		{
			this.logger.LogInformation($"Received notification from client: {notification}");
			var subscriptions = this.subscriptions.GetSubscriptions(notification.Event.Topic, notification.Event.HubEvent);
			this.logger.LogDebug($"Found {subscriptions.Count} subscriptions matching client event");

			foreach (var sub in subscriptions)
			{
				await this.notifications.SendNotification(notification, sub);
			}
			return this.Accepted();
		}
#endregion
	}
}
