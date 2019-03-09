using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnet.FHIR.common;

namespace dotnet.FHIR.commons
{
	public class ClientIdentity : ModelBase
	{
		public int ClientIdentityID { get; set; }
		public string Topic { get; set; }
		public string FirstName { get; set; }
		public string LastName {get; set;}
	}

	public class ClientApp : ModelBase
	{
		public int ClientAppID { get; set; }
		public string Name { get; set; }
		public string Secret { get; set; }
	}

	public class UserClient : ModelBase
	{
		public int UserClientID { get; set; }
		public int ClientAppID { get; set; }
		public int ClientIdentityID { get; set; }
		public string UserName { get; set; }
	}

}
