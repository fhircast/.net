namespace Nuance.PowerCast.Common
{
	public class PowerCastHubEvent
	{
		public const string DiagnosticReportOpen = "DiagnosticReport-open";
		public const string DiagnosticReportClose = "DiagnosticReport-close";
		public const string DiagnosticReportUpdate = "DiagnosticReport-update";
		public const string DiagnosticReportGet = "DiagnosticReport-get";
		public const string DiagnosticReportSet = "DiagnosticReport-set";
		public const string UserLogout = "userlogout";
		public const string SyncError = "syncerror";
		public const string DmvaMessage = "DMVAMessage";
	}
}
