using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace Nuance.PowerCast.TestPowerCast
{
	class Common
	{
		public static ImagingStudy CreateImagingStudy(string acsn, string started, bool active)
		{
			ImagingStudy study = new ImagingStudy
			{
				Id = Guid.NewGuid().ToString(),
				Identifier = {
					new Identifier {
						Type = new CodeableConcept
						{
							Coding = new List<Coding>
							{
								new Coding
								{
									System = "http://terminology.hl7.org/CodeSystem/v2-0203",
									Code = "ACSN"
								}
							}
						},
						Value = acsn
					}
				}
			};
			if (!active)
				study.Status = ImagingStudy.ImagingStudyStatus.Available;
			if (null != started)
				study.Started = started;
			return study;
		}
	}
}
