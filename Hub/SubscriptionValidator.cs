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
			string challenge = Guid.NewGuid().ToString("n");
			var callbackUri = new UriBuilder(sub.Callback);
			callbackUri.Query = $"hub.challenge={challenge}&hub.events={sub.Events}&hub.mode={sub.Mode}&hub.topic={sub.Topic}";

			HttpClient client = new HttpClient();
			//client.Timeout = TimeSpan.FromSeconds(15);
			var response = await client.GetAsync(callbackUri.ToString());
			logger.LogDebug($"Response code from callback url: {response.StatusCode}");
			if (!response.IsSuccessStatusCode)
			{
				logger.LogInformation($"Callback failed - Status code: {response.StatusCode}");
				return ClientValidationOutcome.NotVerified;
			}
			var responseBody = (await response.Content.ReadAsStringAsync());
			logger.LogDebug($"Response from challenge: {responseBody}");
			if (responseBody != challenge)
			{
				logger.LogInformation($"Callback result for verification request was not equal to challenge. Response body: '{responseBody}', Challenge: '{challenge}'.");
				return ClientValidationOutcome.NotVerified;
			}
			logger.LogDebug($"Challenge succeeded - intent verified.");
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
