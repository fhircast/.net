using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace dotnet.FHIR.hub
{
	public class ValidateSubscriptionJob
	{
		private readonly ISubscriptionValidator validator;
		private readonly ISubscriptions subscriptions;
		private readonly ILogger<ValidateSubscriptionJob> logger;

		public ValidateSubscriptionJob(ISubscriptionValidator validator, ISubscriptions subscriptions, ILogger<ValidateSubscriptionJob> logger)
		{
			this.validator = validator;
			this.subscriptions = subscriptions;
			this.logger = logger;
		}

		public async Task Run(Subscription sub)
		{
			if (sub.Mode == SubscriptionMode.Subscribe)
			{
				//HubValidationOutcome validationOutcome = simulateCancellation ? HubValidationOutcome.Canceled : HubValidationOutcome.Valid;
				var validationResult = await this.validator.ValidateSubscription(sub);
				if (validationResult == ClientValidationOutcome.Verified)
				{
					this.logger.LogInformation($"Adding verified subscription: {sub}.");
					this.subscriptions.AddSubscription(sub);
				}
				else
				{
					this.logger.LogInformation($"Not adding unverified subscription: {sub}.");
				}
			}
			else if (sub.Mode == SubscriptionMode.Unsubscribe)
			{
				this.subscriptions.RemoveSubscription(sub.Channel.Endpoint);
			}
		}
	}
}
