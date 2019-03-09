using dotnet.FHIR.common;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace dotnet.FHIR.hub
{
	public class SubscriptionValidator : ISubscriptionValidator
	{
		private readonly ILogger logger = null;

		public SubscriptionValidator(ILogger<SubscriptionValidator> logger)
		{
			this.logger = logger;
		}

		public async Task<ClientValidationOutcome> ValidateSubscription(Subscription sub)
		{
			if (sub == null)
			{
				throw new ArgumentNullException(nameof(sub));
			}

			logger.LogDebug("Verifying sub.");
			SubscriptionVerification callbackParameters = new SubscriptionVerification
			{
				// Note that this is not necessarily cryptographically random/secure.
				Challenge = Guid.NewGuid().ToString("n"),
				Callback = sub.Callback,
				Events = sub.Events,
				Mode = sub.Mode,
				Topic = sub.Topic
			};

			logger.LogDebug($"Calling callback url: {sub.Callback}");
			var callbackUri = new SubscriptionCallback().GetCallbackUri(sub);
			var response = await new HttpClient().GetAsync(callbackUri);

			if (!response.IsSuccessStatusCode)
			{
				logger.LogInformation($"Status code was not success but instead {response.StatusCode}");
				return ClientValidationOutcome.NotVerified;
			}
			var challenge = callbackParameters.Challenge;
			var responseBody = (await response.Content.ReadAsStringAsync());
			if (responseBody != challenge)
			{
				logger.LogInformation($"Callback result for verification request was not equal to challenge. Response body: '{responseBody}', Challenge: '{challenge}'.");
				return ClientValidationOutcome.NotVerified;
			}
			return ClientValidationOutcome.Verified;
		}
	}

	public enum HubValidationOutcome
	{
		Valid,
		Canceled,
	}

	public enum ClientValidationOutcome
	{
		Verified,
		NotVerified,
	}
}
