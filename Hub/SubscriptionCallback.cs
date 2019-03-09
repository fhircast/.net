using dotnet.FHIR.common;
using System.Linq;
using System.Web;
using System;
using System.Reflection;

namespace dotnet.FHIR.hub
{
	public class SubscriptionCallback
	{
		public Uri GetCallbackUri(Subscription sub)
		{
			var properties = sub.GetType().GetProperties()
				.Where(x => x.GetValue(sub, null) != null)
				.Select(x => "hub." + x.Name + "=" + HttpUtility.UrlEncode(x.GetValue(sub, null).ToString()));

			var addedParamters = String.Join("&", properties.ToArray());

			var newUri = new UriBuilder(sub.Callback);
			newUri.Query += addedParamters;

			return newUri.Uri;
		}
	}
}
