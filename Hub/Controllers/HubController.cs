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

		public HubController(ILogger<HubController> logger, IBackgroundJobClient backgroundJobClient, ISubscriptions subscriptions, INotifications notifications, IConfiguration config)
		{
			this.backgroundJobClient = backgroundJobClient;
			this.logger = logger;
			this.subscriptions = subscriptions;
			this.notifications = notifications;
			this.settings = config;
#if DEBUG
			this.connectionString = settings.GetConnectionString("Development");
#else
			this.connectionString = settings.GetConnectionString("Default");
#endif
			this.logger.LogInformation($"Using database connection string: {this.connectionString}");
		}

		#region REST Methods

		[HttpGet("GetTopic")]
		public string GetTopic(string username, string secret)
		{
			this.logger.LogDebug($"GetTopic({username}, {secret}");
			string topic = null;
			using (SqlConnection connection = new SqlConnection(this.connectionString))
			{
				string queryString = 
					@"SELECT ca.Name as AppName, ui.Topic, ui.FirstName, ui.LastName
					FROM ClientApp as ca
						LEFT JOIN ClientAppUser as cau on cau.ClientAppID = ca.ClientAppID
						LEFT JOIN UserIdentity as ui on ui.UserIdentityID = cau.UserIdentityID
						WHERE cau.UserName=@username AND ca.secret=@secret";
				SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
				adapter.SelectCommand.Parameters.AddWithValue("@username", username);
				adapter.SelectCommand.Parameters.AddWithValue("@secret", secret);
				DataSet client = new DataSet();
				try { adapter.Fill(client); }
				catch (Exception ex)
				{
					this.logger.LogError($"Exception in Connect:\r\n{ex.ToString()}");
					return null;
				}
				if (client.Tables.Count > 0 && client.Tables[0].Rows.Count > 0)
				{
					DataRow row = client.Tables[0].Rows[0];
					topic = row["Topic"].ToString();
					string firstName = row["FirstName"].ToString();
					string lastName = row["LastName"].ToString();
					string appName = row["AppName"].ToString();
					this.logger.LogInformation($"User validated successfully: app name:'{appName}', user:'{firstName} {lastName}, topic:'{topic}'");
				}
				else
				{
					this.logger.LogInformation($"{username}/{secret} did not valildate; returning null topic");
				}
			}
			return topic;
		}

		[HttpPost]
		public IActionResult Subscribe([FromForm] Subscription hub)
		{
			this.logger.LogDebug($"Subscribe...");
			Subscription sub = new Subscription();
			sub.Callback = Request.Form["hub.callback"];
			sub.Channel = new Channel();
			sub.Channel.Type = Request.Form["hub.channel.type"];
			sub.Events = Request.Form["hub.events"];
			sub.Mode = Request.Form["hub.mode"];
			sub.Secret = Request.Form["hub.secret"];
			sub.Topic = Request.Form["hub.topic"];
			this.logger.LogDebug($"Subscribe; subscription:{Environment.NewLine}{sub}");
			if (null == sub.Channel)
			{
				sub.Channel = new Channel() { Type = ChannelType.Rest };
			}
			if (sub.Channel.Type.ToLower() == ChannelType.Websocket)
			{
				this.logger.LogInformation($"Processing subscription request for websocket on topic {sub.Topic}.");
				if (sub.Mode == SubscriptionMode.Subscribe)
				{
					this.logger.LogInformation($"Adding subscription: {sub}.");
					this.subscriptions.AddSubscription(sub);
					return this.Ok();
				}
				else
				{
					this.logger.LogInformation($"Unsubscribing subscription: {sub}.");
					this.subscriptions.RemoveSubscription(sub);
					return this.Ok("unsubscribed");
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
		/// Gets all active subscriptions.
		/// </summary>
		/// <returns>All active subscriptions.</returns>
		[HttpGet]
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
			return this.Ok();
		}
#endregion
	}
}
