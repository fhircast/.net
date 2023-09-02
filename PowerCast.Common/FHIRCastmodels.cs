using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Nuance.PowerCast.Common
{

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

		[JsonProperty(PropertyName = "hub.callback", Order = 7)]
		[BindProperty(Name = "hub.callback")]
		public string Callback { get; set; }

		[JsonProperty(PropertyName = "hub.channel", Order = 4)]
		[BindProperty(Name = "hub.channel")]
		public Channel Channel { get; set; } = new Channel();

		[JsonProperty(PropertyName = "hub.events", Order = 2)]
		[BindProperty(Name = "hub.events")]
		public string Events { get; set; }

		[JsonProperty(PropertyName = "hub.lease_seconds", Order = 8)]
		[BindProperty(Name = "hub.lease_seconds")]
		public int? LeaseSeconds { get; set; }

		[JsonProperty(PropertyName = "hub.mode", Order = 5)]
		[BindProperty(Name = "hub.mode")]
		public string Mode { get; set; }

		[JsonProperty(PropertyName = "hub.secret", Order = 6)]
		[BindProperty(Name = "hub.secret")]
		public string Secret { get; set; }

		/// <summary>
		/// Subscribing application name
		/// </summary>
		[JsonProperty(PropertyName = "hub.subscriber", Order = 3)]
		[BindProperty(Name = "hub.subscriber")]
		public string Subscriber { get; set; }

		[JsonProperty(PropertyName = "hub.topic", Order = 1)]
		[BindProperty(Name = "hub.topic")]
		public string Topic { get; set; }

		// The following members are not part of the API
		[JsonProperty(Order = 9)]
		public string ClientName { get; set; }

		[JsonProperty(Order = 10)]
		public DateTime? DateTimeSubscribed { get; set; }

		public bool ShouldSerializeMode()
		{
			return (!string.IsNullOrWhiteSpace(Mode));
		}

		public bool ShouldSerializeSecret()
		{
			return (!string.IsNullOrWhiteSpace(Secret));
		}

		public bool ShouldSerializeChannel()
		{
			return (Channel != null);
		}

		public bool ShouldSerializeCallback()
		{
			return (!string.IsNullOrWhiteSpace(Callback));
		}
	}

	public sealed class Channel
	{
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "endpoint")]
		public string Endpoint { get; set; }
	}

	public class SubscriptionResponse
	{
		[JsonProperty(PropertyName = "hub.channel.endpoint")]
		public string websocket_endpoint { get; set; }
		public List<HubContext> contexts { get; set; }
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

	public class NotificationEvent : ModelBase
	{
		[JsonProperty(PropertyName = "hub.topic")]
		public string Topic { get; set; }

		[JsonProperty(PropertyName = "hub.event")]
		public string HubEvent { get; set; }

		[JsonProperty(PropertyName = "context.versionId")]
		public string ContextVersionId { get; set; }

		[JsonProperty(PropertyName = "context.priorVersionId")]
		public string PriorContextVersionId { get; set; }

		[JsonProperty(PropertyName = "context")]
		public List<ContextItem> Context { get; set; }
	}

	public class HubContext
	{
		public HubContext() { }
		public HubContext(string contextType, List<ContextItem> context, string contextVersionId = null, string priorContextVersionId = null)
		{
			this.contextVersionId = contextVersionId;
			this.priorContextVersionId = priorContextVersionId;
			this.contextType = contextType;
			this.context = context;
		}

		[JsonProperty(PropertyName = "context.versionId")]
		public string contextVersionId { get; set; }
		[JsonProperty(PropertyName = "context.priorVersionId")]
		public string priorContextVersionId { get; set; }
		public string contextType { get; set; }
		public List<ContextItem> context { get; set; }
	}

	public class DictTypes
	{
		public static Dictionary<Type, string> FhirTypes = new Dictionary<Type, string>
		{
			{typeof(ImagingStudy), "ImagingStudy"},
			{typeof(DiagnosticReport), "DiagnosticReport"},
			{typeof(Patient), "Patient"},
			{typeof(Observation), "Observation"},
			{typeof(Bundle), "Bundle"},
			{typeof(OperationOutcome), "OperationOutcome"}
		};
	}

	public sealed class ContextItem
	{
		private object _resource;

		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "reference")]
		public string Reference { get; set; }

		[JsonProperty(PropertyName = "resource")]
		public object Resource
		{
			get
			{
				return _resource;
			}
			set
			{
				if (null == value) return;
				Type valType = value.GetType();
				if (DictTypes.FhirTypes.ContainsKey(valType))
				{
					switch (DictTypes.FhirTypes[valType])
					{
						case "ImagingStudy":
							{
								ImagingStudy res = (ImagingStudy)value;
								_resource = JsonConvert.DeserializeObject(res.ToJson());
							}
							break;
						case "DiagnosticReport":
							{
								DiagnosticReport res = (DiagnosticReport)value;
								_resource = JsonConvert.DeserializeObject(res.ToJson());
							}
							break;
						case "Patient":
							{
								Patient res = (Patient)value;
								_resource = JsonConvert.DeserializeObject(res.ToJson());
							}
							break;
						case "Observation":
							{
								Observation res = (Observation)value;
								_resource = JsonConvert.DeserializeObject(res.ToJson());
							}
							break;
						case "Bundle":
							{
								Bundle res = (Bundle)value;
								_resource = JsonConvert.DeserializeObject(res.ToJson());
							}
							break;
						case "OperationOutcome":
							{
								OperationOutcome res = (OperationOutcome)value;
								_resource = JsonConvert.DeserializeObject(res.ToJson());
							}
							break;
						default: // This should never happen - just a safeguard
							_resource = value;
							break;
					}
				}
				else
				{
					_resource = value;
				}
			}
		}
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

	public class ConfigurationData
	{
		public ConfigurationData() { }
		public string authorization_endpoint { get; set; }
		public string token_endpoint { get; set; }
		public string hub_endpoint { get; set; }
		public string topic { get; set; }
	}

	public static class FhirParser
	{
		private static readonly Dictionary<string, string> ContextKeys = new Dictionary<string, string>()
		{
			{ "Patient", "patient" },
			{ "ImagingStudy", "study" },
			{ "Observation", "observation" },
			{ "DiagnosticReport", "report" }
		};

		private static FhirJsonParser _fhirParser = new FhirJsonParser(new ParserSettings
		{
			AcceptUnknownMembers = true,
			AllowUnrecognizedEnums = true
		});

		public static ImagingStudy ToImagingStudy(this object resource)
		{
			return _fhirParser.Parse<ImagingStudy>(JsonConvert.SerializeObject(resource));
		}
		public static DiagnosticReport ToDiagnosticReport(this object resource)
		{
			return _fhirParser.Parse<DiagnosticReport>(JsonConvert.SerializeObject(resource));
		}

		public static Observation ToObservation(this object resource)
		{
			return _fhirParser.Parse<Observation>(JsonConvert.SerializeObject(resource));
		}

		public static Patient ToPatient(this object resource)
		{
			return _fhirParser.Parse<Patient>(JsonConvert.SerializeObject(resource));
		}

		public static DomainResource ToDomainResource(this object resource)
		{
			return _fhirParser.Parse<DomainResource>(JsonConvert.SerializeObject(resource));
		}

		public static OperationOutcome ToOperationOutcome(this object resource)
		{
			return _fhirParser.Parse<OperationOutcome>(JsonConvert.SerializeObject(resource));
		}

		public static List<DomainResource> ToListDomainResource(this List<ContextItem> contextItems)
		{
			List<DomainResource> listDr = new List<DomainResource>();
			foreach (ContextItem ci in contextItems)
			{
				if (null != ci.Resource)
				{
					listDr.Add(_fhirParser.Parse<DomainResource>(JsonConvert.SerializeObject(ci.Resource)));
				}
			}
			return listDr;
		}

		public static List<ContextItem> ToListContextItem(this List<DomainResource> resources)
		{
			List<ContextItem> listCi = new List<ContextItem>();
			foreach (DomainResource dr in resources)
			{
				if (dr.TryDeriveResourceType(out ResourceType resourceType))
				{
					var key = ContextKeys[resourceType.ToString()];
					listCi.Add(new ContextItem()
					{
						Key = key,
						Resource = dr
					});
				}
			}
			return listCi;
		}

		public static Bundle GetUpdateBundle(this List<ContextItem> contextItems)
		{
			if (contextItems != null && contextItems.Any())
			{
				var bundleItem = contextItems.Find(ci => ci.Key == "updates" || ci.Key == "bundle" || ci.Key == "content");
				if (bundleItem != null && bundleItem.Resource != null)
				{
					return _fhirParser.Parse<Bundle>(JsonConvert.SerializeObject(bundleItem.Resource));
				}
			}

			return new Bundle();
		}

		public static List<Bundle> GetUpdateBundles(this List<ContextItem> contextItems)
		{
			if (contextItems != null && contextItems.Any())
			{
				var bundleItems = contextItems.Where(ci => ci.Resource != null && (ci.Key.ToLower() == "updates" || ci.Key.ToLower() == "bundle" || ci.Key.ToLower() == "content"));
				if (bundleItems != null && bundleItems.Any())
				{
					return bundleItems.Select(b => _fhirParser.Parse<Bundle>(JsonConvert.SerializeObject(b.Resource))).ToList();
				}
			}

			return new List<Bundle>();
		}
	}

	public static class FhirBuilder
	{
		public static Patient FhirPatient(string id, string mrn)
		{
			Patient p = new Patient()
			{
				Id = id,
				Identifier = new List<Hl7.Fhir.Model.Identifier>()
				{ new Hl7.Fhir.Model.Identifier()
					{
						Use = Hl7.Fhir.Model.Identifier.IdentifierUse.Usual,
						Type = new Hl7.Fhir.Model.CodeableConcept()
						{
							Coding = new List<Hl7.Fhir.Model.Coding>()
							{ new Hl7.Fhir.Model.Coding()
								{
									System = "http://terminology.hl7.org/CodeSystem/v2-0203",
									Code = "MR"
								}
							}
						},
						Value = mrn
					}
				}
			};
			return p;
		}

		public static ImagingStudy FhirStudy(string id, string accession, string refPatientId, ImagingStudy.ImagingStudyStatus? status = null)
		{
			ImagingStudy s = new ImagingStudy()
			{
				Id = id,
				Subject = new ResourceReference()
				{
					Reference = $"Patient/{refPatientId}"
				},
				Identifier = new List<Hl7.Fhir.Model.Identifier>()
				{ new Hl7.Fhir.Model.Identifier()
					{
						Use = Hl7.Fhir.Model.Identifier.IdentifierUse.Usual,
						Type = new Hl7.Fhir.Model.CodeableConcept()
						{
							Coding = new List<Hl7.Fhir.Model.Coding>()
							{ new Hl7.Fhir.Model.Coding()
								{
									System = "http://terminology.hl7.org/CodeSystem/v2-0203",
									Code = "ACSN"
								}
							}
						},
						Value = accession
					}
				}
			};
			if (null != status)
			{
				s.Status = status;
			}
			return s;
		}
	}
}