namespace Nuance.PowerCast.Common
{
    public class PowerCastHubEvent
    {
        public const string DiagnosticReportOpen = "DiagnosticReport-open";
        public const string DiagnosticReportClose = "DiagnosticReport-close";
        public const string DiagnosticReportUpdate = "DiagnosticReport-update";
        public const string DiagnosticReportSelect = "DiagnosticReport-select";

        public const string ImagingStudyOpen = "ImagingStudy-open";
        public const string ImagingStudyClose = "ImagingStudy-close";

        public const string PatientOpen = "Patient-open";
        public const string PatientClose = "Patient-close";

        public const string UserLogout = "userlogout";
        public const string UserHibernate = "userhibernate";

        public const string SyncError = "syncerror";
        public const string DmvaMessage = "DMVAMessage";

        public const string DiagnosticReportOpened = "DiagnosticReport-opened";
        public const string DiagnosticReportClosed = "DiagnosticReport-closed";
        public const string DiagnosticReportUpdated = "DiagnosticReport-updated";
    }
}
