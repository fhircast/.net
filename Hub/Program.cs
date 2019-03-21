//using Hangfire.MemoryStorage;
//using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using NLog.Web;

namespace dotnet.FHIR.hub
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
			//var host = new WebHostBuilder()
			//	.UseKestrel()
			//	.UseContentRoot(System.IO.Directory.GetCurrentDirectory())
			//	.UseIISIntegration()
			//	.UseStartup<Startup>()
			//	.UseNLog()
			//	.Build();
			//host.Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseKestrel()
			.UseContentRoot(System.IO.Directory.GetCurrentDirectory())
			.UseIISIntegration()
			.UseStartup<Startup>()
			.UseNLog()
			.Build();
	}
}
