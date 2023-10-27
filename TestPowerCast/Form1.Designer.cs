namespace Nuance.PowerCast.TestPowerCast
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.ttAccession = new System.Windows.Forms.ToolTip(this.components);
			this.buttonGetSubscriptions = new System.Windows.Forms.Button();
			this.tabUpdateStudies = new System.Windows.Forms.TabPage();
			this.btnDeleteStudy = new System.Windows.Forms.Button();
			this.btnUpdateStudy = new System.Windows.Forms.Button();
			this.btnAddStudy = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.lvStudies = new System.Windows.Forms.ListView();
			this.tabReportOpenClose = new System.Windows.Forms.TabPage();
			this.btnCloseStudy = new System.Windows.Forms.Button();
			this.btnOpenStudy = new System.Windows.Forms.Button();
			this.btnPrelimReport = new System.Windows.Forms.Button();
			this.btnSignReport = new System.Windows.Forms.Button();
			this.btnNotifyError = new System.Windows.Forms.Button();
			this.txtMRN = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtAccession = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.btnGetReport = new System.Windows.Forms.Button();
			this.btnCloseReport = new System.Windows.Forms.Button();
			this.btnNotify = new System.Windows.Forms.Button();
			this.tabSubscribe = new System.Windows.Forms.TabPage();
			this.chkMultipleInstances = new System.Windows.Forms.CheckBox();
			this.txtLeaseSeconds = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.btnUnsubscribe = new System.Windows.Forms.Button();
			this.txtHubUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTopic = new System.Windows.Forms.TextBox();
			this.txtSubEvents = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSubscribe = new System.Windows.Forms.Button();
			this.tabLogin = new System.Windows.Forms.TabPage();
			this.btnUserLogout = new System.Windows.Forms.Button();
			this.txtLaunchUsername = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.tabConfiguration = new System.Windows.Forms.TabPage();
			this.lblAuthToken = new System.Windows.Forms.Label();
			this.txtAuthToken = new System.Windows.Forms.TextBox();
			this.useAuthToken = new System.Windows.Forms.CheckBox();
			this.txtConfigTopic = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtConfigHubUrl = new System.Windows.Forms.TextBox();
			this.txtConfigTokenUrl = new System.Windows.Forms.TextBox();
			this.txtConfigAuthUrl = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnGetConfig = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabUpdateContent = new System.Windows.Forms.TabPage();
			this.btnDeleteDevice = new System.Windows.Forms.Button();
			this.btnAddObservationDevice = new System.Windows.Forms.Button();
			this.cbxIncludeCurrentContext = new System.Windows.Forms.CheckBox();
			this.btnLoadReference = new System.Windows.Forms.Button();
			this.btnDeleteContent = new System.Windows.Forms.Button();
			this.btnUpdateContent = new System.Windows.Forms.Button();
			this.btnAddContent = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.lvContent = new System.Windows.Forms.ListView();
			this.btnDownloadHubLogs = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.btnOpenLogFolder = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.tabUpdateStudies.SuspendLayout();
			this.tabReportOpenClose.SuspendLayout();
			this.tabSubscribe.SuspendLayout();
			this.tabLogin.SuspendLayout();
			this.tabConfiguration.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabUpdateContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.Location = new System.Drawing.Point(12, 195);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(703, 324);
			this.txtLog.TabIndex = 5;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(13, 525);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 11;
			this.btnClear.Text = "Clear Log";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopy.Location = new System.Drawing.Point(99, 525);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 12;
			this.btnCopy.Text = "Copy Log";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// buttonGetSubscriptions
			// 
			this.buttonGetSubscriptions.Location = new System.Drawing.Point(200, 88);
			this.buttonGetSubscriptions.Name = "buttonGetSubscriptions";
			this.buttonGetSubscriptions.Size = new System.Drawing.Size(101, 23);
			this.buttonGetSubscriptions.TabIndex = 42;
			this.buttonGetSubscriptions.Text = "Get Subscriptions";
			this.ttAccession.SetToolTip(this.buttonGetSubscriptions, "Gets a list of subscriptions for the current topic");
			this.buttonGetSubscriptions.Click += new System.EventHandler(this.buttonGetSubscriptions_Click);
			// 
			// tabUpdateStudies
			// 
			this.tabUpdateStudies.Controls.Add(this.btnDeleteStudy);
			this.tabUpdateStudies.Controls.Add(this.btnUpdateStudy);
			this.tabUpdateStudies.Controls.Add(this.btnAddStudy);
			this.tabUpdateStudies.Controls.Add(this.label6);
			this.tabUpdateStudies.Controls.Add(this.lvStudies);
			this.tabUpdateStudies.Location = new System.Drawing.Point(4, 22);
			this.tabUpdateStudies.Name = "tabUpdateStudies";
			this.tabUpdateStudies.Padding = new System.Windows.Forms.Padding(3);
			this.tabUpdateStudies.Size = new System.Drawing.Size(699, 151);
			this.tabUpdateStudies.TabIndex = 4;
			this.tabUpdateStudies.Text = "Studies";
			this.tabUpdateStudies.UseVisualStyleBackColor = true;
			// 
			// btnDeleteStudy
			// 
			this.btnDeleteStudy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteStudy.Enabled = false;
			this.btnDeleteStudy.Location = new System.Drawing.Point(446, 83);
			this.btnDeleteStudy.Name = "btnDeleteStudy";
			this.btnDeleteStudy.Size = new System.Drawing.Size(155, 23);
			this.btnDeleteStudy.TabIndex = 40;
			this.btnDeleteStudy.Text = "Delete...";
			this.btnDeleteStudy.UseVisualStyleBackColor = true;
			this.btnDeleteStudy.Click += new System.EventHandler(this.btnDeleteStudy_Click);
			// 
			// btnUpdateStudy
			// 
			this.btnUpdateStudy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdateStudy.Enabled = false;
			this.btnUpdateStudy.Location = new System.Drawing.Point(446, 54);
			this.btnUpdateStudy.Name = "btnUpdateStudy";
			this.btnUpdateStudy.Size = new System.Drawing.Size(155, 23);
			this.btnUpdateStudy.TabIndex = 39;
			this.btnUpdateStudy.Text = "Update...";
			this.btnUpdateStudy.UseVisualStyleBackColor = true;
			this.btnUpdateStudy.Click += new System.EventHandler(this.btnUpdateStudy_Click);
			// 
			// btnAddStudy
			// 
			this.btnAddStudy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddStudy.Location = new System.Drawing.Point(446, 25);
			this.btnAddStudy.Name = "btnAddStudy";
			this.btnAddStudy.Size = new System.Drawing.Size(155, 23);
			this.btnAddStudy.TabIndex = 37;
			this.btnAddStudy.Text = "Add new...";
			this.btnAddStudy.UseVisualStyleBackColor = true;
			this.btnAddStudy.Click += new System.EventHandler(this.btnAddStudy_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(366, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Studies contained in current report (to refresh, issue Report-Get in other tab.)";
			// 
			// lvStudies
			// 
			this.lvStudies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvStudies.HideSelection = false;
			this.lvStudies.Location = new System.Drawing.Point(6, 25);
			this.lvStudies.MultiSelect = false;
			this.lvStudies.Name = "lvStudies";
			this.lvStudies.Size = new System.Drawing.Size(420, 120);
			this.lvStudies.TabIndex = 0;
			this.lvStudies.UseCompatibleStateImageBehavior = false;
			// 
			// tabReportOpenClose
			// 
			this.tabReportOpenClose.Controls.Add(this.buttonGetSubscriptions);
			this.tabReportOpenClose.Controls.Add(this.btnCloseStudy);
			this.tabReportOpenClose.Controls.Add(this.btnOpenStudy);
			this.tabReportOpenClose.Controls.Add(this.btnPrelimReport);
			this.tabReportOpenClose.Controls.Add(this.btnSignReport);
			this.tabReportOpenClose.Controls.Add(this.btnNotifyError);
			this.tabReportOpenClose.Controls.Add(this.txtMRN);
			this.tabReportOpenClose.Controls.Add(this.label11);
			this.tabReportOpenClose.Controls.Add(this.txtAccession);
			this.tabReportOpenClose.Controls.Add(this.label10);
			this.tabReportOpenClose.Controls.Add(this.btnGetReport);
			this.tabReportOpenClose.Controls.Add(this.btnCloseReport);
			this.tabReportOpenClose.Controls.Add(this.btnNotify);
			this.tabReportOpenClose.Location = new System.Drawing.Point(4, 22);
			this.tabReportOpenClose.Name = "tabReportOpenClose";
			this.tabReportOpenClose.Padding = new System.Windows.Forms.Padding(3);
			this.tabReportOpenClose.Size = new System.Drawing.Size(699, 151);
			this.tabReportOpenClose.TabIndex = 3;
			this.tabReportOpenClose.Text = "Actions";
			this.tabReportOpenClose.UseVisualStyleBackColor = true;
			// 
			// btnCloseStudy
			// 
			this.btnCloseStudy.Location = new System.Drawing.Point(279, 43);
			this.btnCloseStudy.Name = "btnCloseStudy";
			this.btnCloseStudy.Size = new System.Drawing.Size(85, 23);
			this.btnCloseStudy.TabIndex = 41;
			this.btnCloseStudy.Text = "Close Study";
			this.btnCloseStudy.Click += new System.EventHandler(this.btnCloseStudy_Click);
			// 
			// btnOpenStudy
			// 
			this.btnOpenStudy.Location = new System.Drawing.Point(188, 43);
			this.btnOpenStudy.Name = "btnOpenStudy";
			this.btnOpenStudy.Size = new System.Drawing.Size(85, 23);
			this.btnOpenStudy.TabIndex = 40;
			this.btnOpenStudy.Text = "Open Study";
			this.btnOpenStudy.Click += new System.EventHandler(this.btnOpenStudy_Click);
			// 
			// btnPrelimReport
			// 
			this.btnPrelimReport.Location = new System.Drawing.Point(461, 43);
			this.btnPrelimReport.Name = "btnPrelimReport";
			this.btnPrelimReport.Size = new System.Drawing.Size(85, 23);
			this.btnPrelimReport.TabIndex = 39;
			this.btnPrelimReport.Text = "Prelim Report";
			this.btnPrelimReport.Click += new System.EventHandler(this.btnPrelimReport_Click);
			// 
			// btnSignReport
			// 
			this.btnSignReport.Location = new System.Drawing.Point(370, 43);
			this.btnSignReport.Name = "btnSignReport";
			this.btnSignReport.Size = new System.Drawing.Size(85, 23);
			this.btnSignReport.TabIndex = 38;
			this.btnSignReport.Text = "Sign Report";
			this.btnSignReport.Click += new System.EventHandler(this.btnSignReport_Click);
			// 
			// btnNotifyError
			// 
			this.btnNotifyError.Location = new System.Drawing.Point(97, 88);
			this.btnNotifyError.Name = "btnNotifyError";
			this.btnNotifyError.Size = new System.Drawing.Size(96, 23);
			this.btnNotifyError.TabIndex = 37;
			this.btnNotifyError.Text = "Notify Sync Error";
			this.btnNotifyError.Click += new System.EventHandler(this.btnNotifyError_Click);
			// 
			// txtMRN
			// 
			this.txtMRN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMRN.Location = new System.Drawing.Point(517, 12);
			this.txtMRN.Name = "txtMRN";
			this.txtMRN.Size = new System.Drawing.Size(125, 20);
			this.txtMRN.TabIndex = 28;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(423, 15);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(88, 13);
			this.label11.TabIndex = 36;
			this.label11.Text = "Patient ID (MRN)";
			// 
			// txtAccession
			// 
			this.txtAccession.Location = new System.Drawing.Point(116, 12);
			this.txtAccession.Name = "txtAccession";
			this.txtAccession.Size = new System.Drawing.Size(300, 20);
			this.txtAccession.TabIndex = 26;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 15);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(107, 13);
			this.label10.TabIndex = 34;
			this.label10.Text = "Accession Number(s)";
			// 
			// btnGetReport
			// 
			this.btnGetReport.Location = new System.Drawing.Point(6, 88);
			this.btnGetReport.Name = "btnGetReport";
			this.btnGetReport.Size = new System.Drawing.Size(85, 23);
			this.btnGetReport.TabIndex = 33;
			this.btnGetReport.Text = "Get Context";
			this.btnGetReport.Click += new System.EventHandler(this.btnGetReport_Click);
			// 
			// btnCloseReport
			// 
			this.btnCloseReport.Location = new System.Drawing.Point(97, 43);
			this.btnCloseReport.Name = "btnCloseReport";
			this.btnCloseReport.Size = new System.Drawing.Size(85, 23);
			this.btnCloseReport.TabIndex = 31;
			this.btnCloseReport.Text = "Close Report";
			this.btnCloseReport.Click += new System.EventHandler(this.btnCloseReport_Click);
			// 
			// btnNotify
			// 
			this.btnNotify.Location = new System.Drawing.Point(6, 43);
			this.btnNotify.Name = "btnNotify";
			this.btnNotify.Size = new System.Drawing.Size(85, 23);
			this.btnNotify.TabIndex = 29;
			this.btnNotify.Text = "Open Report";
			this.btnNotify.Click += new System.EventHandler(this.btnNotify_Click);
			// 
			// tabSubscribe
			// 
			this.tabSubscribe.Controls.Add(this.chkMultipleInstances);
			this.tabSubscribe.Controls.Add(this.txtLeaseSeconds);
			this.tabSubscribe.Controls.Add(this.label12);
			this.tabSubscribe.Controls.Add(this.btnUnsubscribe);
			this.tabSubscribe.Controls.Add(this.txtHubUrl);
			this.tabSubscribe.Controls.Add(this.label1);
			this.tabSubscribe.Controls.Add(this.txtTopic);
			this.tabSubscribe.Controls.Add(this.txtSubEvents);
			this.tabSubscribe.Controls.Add(this.label4);
			this.tabSubscribe.Controls.Add(this.label3);
			this.tabSubscribe.Controls.Add(this.btnSubscribe);
			this.tabSubscribe.Location = new System.Drawing.Point(4, 22);
			this.tabSubscribe.Name = "tabSubscribe";
			this.tabSubscribe.Padding = new System.Windows.Forms.Padding(3);
			this.tabSubscribe.Size = new System.Drawing.Size(699, 151);
			this.tabSubscribe.TabIndex = 2;
			this.tabSubscribe.Text = "Subscribe";
			this.tabSubscribe.UseVisualStyleBackColor = true;
			// 
			// chkMultipleInstances
			// 
			this.chkMultipleInstances.AutoSize = true;
			this.chkMultipleInstances.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkMultipleInstances.Location = new System.Drawing.Point(3, 94);
			this.chkMultipleInstances.Margin = new System.Windows.Forms.Padding(2);
			this.chkMultipleInstances.Name = "chkMultipleInstances";
			this.chkMultipleInstances.Size = new System.Drawing.Size(142, 17);
			this.chkMultipleInstances.TabIndex = 32;
			this.chkMultipleInstances.Text = "Allow Multiple Instances:";
			this.chkMultipleInstances.UseVisualStyleBackColor = true;
			// 
			// txtLeaseSeconds
			// 
			this.txtLeaseSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLeaseSeconds.Location = new System.Drawing.Point(505, 15);
			this.txtLeaseSeconds.Name = "txtLeaseSeconds";
			this.txtLeaseSeconds.Size = new System.Drawing.Size(115, 20);
			this.txtLeaseSeconds.TabIndex = 16;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(422, 18);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(84, 13);
			this.label12.TabIndex = 30;
			this.label12.Text = "Lease Seconds:";
			// 
			// btnUnsubscribe
			// 
			this.btnUnsubscribe.Enabled = false;
			this.btnUnsubscribe.Location = new System.Drawing.Point(87, 120);
			this.btnUnsubscribe.Name = "btnUnsubscribe";
			this.btnUnsubscribe.Size = new System.Drawing.Size(75, 23);
			this.btnUnsubscribe.TabIndex = 29;
			this.btnUnsubscribe.Text = "Unsubscribe";
			this.btnUnsubscribe.UseVisualStyleBackColor = true;
			this.btnUnsubscribe.Click += new System.EventHandler(this.btnUnsubscribe_Click);
			// 
			// txtHubUrl
			// 
			this.txtHubUrl.Enabled = false;
			this.txtHubUrl.Location = new System.Drawing.Point(72, 15);
			this.txtHubUrl.Name = "txtHubUrl";
			this.txtHubUrl.ReadOnly = true;
			this.txtHubUrl.Size = new System.Drawing.Size(344, 20);
			this.txtHubUrl.TabIndex = 15;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 28;
			this.label1.Text = "Hub URL:";
			// 
			// txtTopic
			// 
			this.txtTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTopic.Enabled = false;
			this.txtTopic.Location = new System.Drawing.Point(72, 42);
			this.txtTopic.Name = "txtTopic";
			this.txtTopic.Size = new System.Drawing.Size(551, 20);
			this.txtTopic.TabIndex = 17;
			// 
			// txtSubEvents
			// 
			this.txtSubEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSubEvents.Location = new System.Drawing.Point(72, 69);
			this.txtSubEvents.Name = "txtSubEvents";
			this.txtSubEvents.Size = new System.Drawing.Size(551, 20);
			this.txtSubEvents.TabIndex = 19;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Topic:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Events:";
			// 
			// btnSubscribe
			// 
			this.btnSubscribe.Location = new System.Drawing.Point(6, 120);
			this.btnSubscribe.Name = "btnSubscribe";
			this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
			this.btnSubscribe.TabIndex = 25;
			this.btnSubscribe.Text = "Subscribe";
			this.btnSubscribe.UseVisualStyleBackColor = true;
			this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
			// 
			// tabLogin
			// 
			this.tabLogin.Controls.Add(this.btnUserLogout);
			this.tabLogin.Controls.Add(this.txtLaunchUsername);
			this.tabLogin.Controls.Add(this.btnLogin);
			this.tabLogin.Location = new System.Drawing.Point(4, 22);
			this.tabLogin.Name = "tabLogin";
			this.tabLogin.Padding = new System.Windows.Forms.Padding(3);
			this.tabLogin.Size = new System.Drawing.Size(699, 151);
			this.tabLogin.TabIndex = 1;
			this.tabLogin.Text = "Login PowerScribe";
			this.tabLogin.UseVisualStyleBackColor = true;
			// 
			// btnUserLogout
			// 
			this.btnUserLogout.Location = new System.Drawing.Point(6, 67);
			this.btnUserLogout.Name = "btnUserLogout";
			this.btnUserLogout.Size = new System.Drawing.Size(135, 23);
			this.btnUserLogout.TabIndex = 14;
			this.btnUserLogout.Text = "User Logout";
			this.btnUserLogout.UseVisualStyleBackColor = true;
			this.btnUserLogout.Click += new System.EventHandler(this.btnUserLogout_Click);
			// 
			// txtLaunchUsername
			// 
			this.txtLaunchUsername.Location = new System.Drawing.Point(6, 12);
			this.txtLaunchUsername.Name = "txtLaunchUsername";
			this.txtLaunchUsername.Size = new System.Drawing.Size(214, 20);
			this.txtLaunchUsername.TabIndex = 1;
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(6, 38);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(135, 23);
			this.btnLogin.TabIndex = 13;
			this.btnLogin.Text = "Login PowerScribe";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// tabConfiguration
			// 
			this.tabConfiguration.Controls.Add(this.lblAuthToken);
			this.tabConfiguration.Controls.Add(this.txtAuthToken);
			this.tabConfiguration.Controls.Add(this.useAuthToken);
			this.tabConfiguration.Controls.Add(this.txtConfigTopic);
			this.tabConfiguration.Controls.Add(this.label5);
			this.tabConfiguration.Controls.Add(this.txtConfigHubUrl);
			this.tabConfiguration.Controls.Add(this.txtConfigTokenUrl);
			this.tabConfiguration.Controls.Add(this.txtConfigAuthUrl);
			this.tabConfiguration.Controls.Add(this.label8);
			this.tabConfiguration.Controls.Add(this.label7);
			this.tabConfiguration.Controls.Add(this.label2);
			this.tabConfiguration.Controls.Add(this.btnGetConfig);
			this.tabConfiguration.Location = new System.Drawing.Point(4, 22);
			this.tabConfiguration.Name = "tabConfiguration";
			this.tabConfiguration.Padding = new System.Windows.Forms.Padding(3);
			this.tabConfiguration.Size = new System.Drawing.Size(699, 151);
			this.tabConfiguration.TabIndex = 0;
			this.tabConfiguration.Text = "Configuration Data";
			this.tabConfiguration.UseVisualStyleBackColor = true;
			// 
			// lblAuthToken
			// 
			this.lblAuthToken.AutoSize = true;
			this.lblAuthToken.Location = new System.Drawing.Point(8, 92);
			this.lblAuthToken.Name = "lblAuthToken";
			this.lblAuthToken.Size = new System.Drawing.Size(117, 13);
			this.lblAuthToken.TabIndex = 27;
			this.lblAuthToken.Text = "Auth Token:                 ";
			this.lblAuthToken.Visible = false;
			// 
			// txtAuthToken
			// 
			this.txtAuthToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAuthToken.Enabled = false;
			this.txtAuthToken.Location = new System.Drawing.Point(150, 92);
			this.txtAuthToken.Name = "txtAuthToken";
			this.txtAuthToken.Size = new System.Drawing.Size(533, 20);
			this.txtAuthToken.TabIndex = 26;
			this.txtAuthToken.Visible = false;
			// 
			// useAuthToken
			// 
			this.useAuthToken.AutoSize = true;
			this.useAuthToken.Location = new System.Drawing.Point(150, 12);
			this.useAuthToken.Name = "useAuthToken";
			this.useAuthToken.Size = new System.Drawing.Size(143, 17);
			this.useAuthToken.TabIndex = 25;
			this.useAuthToken.Text = "Use Existing Auth Token";
			this.useAuthToken.UseVisualStyleBackColor = true;
			this.useAuthToken.CheckedChanged += new System.EventHandler(this.useAuthToken_CheckedChanged);
			// 
			// txtConfigTopic
			// 
			this.txtConfigTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConfigTopic.Location = new System.Drawing.Point(150, 66);
			this.txtConfigTopic.Name = "txtConfigTopic";
			this.txtConfigTopic.Size = new System.Drawing.Size(533, 20);
			this.txtConfigTopic.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 66);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Topic:";
			// 
			// txtConfigHubUrl
			// 
			this.txtConfigHubUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConfigHubUrl.Location = new System.Drawing.Point(150, 40);
			this.txtConfigHubUrl.Name = "txtConfigHubUrl";
			this.txtConfigHubUrl.Size = new System.Drawing.Size(533, 20);
			this.txtConfigHubUrl.TabIndex = 7;
			// 
			// txtConfigTokenUrl
			// 
			this.txtConfigTokenUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConfigTokenUrl.Location = new System.Drawing.Point(150, 118);
			this.txtConfigTokenUrl.Name = "txtConfigTokenUrl";
			this.txtConfigTokenUrl.Size = new System.Drawing.Size(533, 20);
			this.txtConfigTokenUrl.TabIndex = 5;
			// 
			// txtConfigAuthUrl
			// 
			this.txtConfigAuthUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConfigAuthUrl.Location = new System.Drawing.Point(150, 92);
			this.txtConfigAuthUrl.Name = "txtConfigAuthUrl";
			this.txtConfigAuthUrl.Size = new System.Drawing.Size(533, 20);
			this.txtConfigAuthUrl.TabIndex = 3;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(7, 118);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 13);
			this.label8.TabIndex = 17;
			this.label8.Text = "Auth0 Token Url:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 43);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "PowerCast Hub Url:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Auth0 Authorization Url:";
			// 
			// btnGetConfig
			// 
			this.btnGetConfig.Location = new System.Drawing.Point(6, 7);
			this.btnGetConfig.Name = "btnGetConfig";
			this.btnGetConfig.Size = new System.Drawing.Size(135, 23);
			this.btnGetConfig.TabIndex = 1;
			this.btnGetConfig.Text = "Get Configuration Data";
			this.btnGetConfig.UseVisualStyleBackColor = true;
			this.btnGetConfig.Click += new System.EventHandler(this.btnGetConfig_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabConfiguration);
			this.tabControl1.Controls.Add(this.tabLogin);
			this.tabControl1.Controls.Add(this.tabSubscribe);
			this.tabControl1.Controls.Add(this.tabReportOpenClose);
			this.tabControl1.Controls.Add(this.tabUpdateStudies);
			this.tabControl1.Controls.Add(this.tabUpdateContent);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(707, 177);
			this.tabControl1.TabIndex = 13;
			// 
			// tabUpdateContent
			// 
			this.tabUpdateContent.Controls.Add(this.btnDeleteDevice);
			this.tabUpdateContent.Controls.Add(this.btnAddObservationDevice);
			this.tabUpdateContent.Controls.Add(this.cbxIncludeCurrentContext);
			this.tabUpdateContent.Controls.Add(this.btnLoadReference);
			this.tabUpdateContent.Controls.Add(this.btnDeleteContent);
			this.tabUpdateContent.Controls.Add(this.btnUpdateContent);
			this.tabUpdateContent.Controls.Add(this.btnAddContent);
			this.tabUpdateContent.Controls.Add(this.label9);
			this.tabUpdateContent.Controls.Add(this.lvContent);
			this.tabUpdateContent.Location = new System.Drawing.Point(4, 22);
			this.tabUpdateContent.Name = "tabUpdateContent";
			this.tabUpdateContent.Padding = new System.Windows.Forms.Padding(3);
			this.tabUpdateContent.Size = new System.Drawing.Size(699, 151);
			this.tabUpdateContent.TabIndex = 5;
			this.tabUpdateContent.Text = "Content";
			this.tabUpdateContent.UseVisualStyleBackColor = true;
			// 
			// btnDeleteDevice
			// 
			this.btnDeleteDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteDevice.Enabled = false;
			this.btnDeleteDevice.Location = new System.Drawing.Point(580, 65);
			this.btnDeleteDevice.Name = "btnDeleteDevice";
			this.btnDeleteDevice.Size = new System.Drawing.Size(90, 23);
			this.btnDeleteDevice.TabIndex = 50;
			this.btnDeleteDevice.Text = "Delete Device";
			this.btnDeleteDevice.UseVisualStyleBackColor = true;
			this.btnDeleteDevice.Click += new System.EventHandler(this.btnDeleteDevice_Click);
			// 
			// btnAddObservationDevice
			// 
			this.btnAddObservationDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddObservationDevice.Enabled = false;
			this.btnAddObservationDevice.Location = new System.Drawing.Point(580, 35);
			this.btnAddObservationDevice.Name = "btnAddObservationDevice";
			this.btnAddObservationDevice.Size = new System.Drawing.Size(90, 23);
			this.btnAddObservationDevice.TabIndex = 49;
			this.btnAddObservationDevice.Text = "Add Device";
			this.btnAddObservationDevice.UseVisualStyleBackColor = true;
			this.btnAddObservationDevice.Click += new System.EventHandler(this.btnAddObservationDevice_Click);
			// 
			// cbxIncludeCurrentContext
			// 
			this.cbxIncludeCurrentContext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbxIncludeCurrentContext.AutoSize = true;
			this.cbxIncludeCurrentContext.Location = new System.Drawing.Point(507, 12);
			this.cbxIncludeCurrentContext.Name = "cbxIncludeCurrentContext";
			this.cbxIncludeCurrentContext.Size = new System.Drawing.Size(137, 17);
			this.cbxIncludeCurrentContext.TabIndex = 46;
			this.cbxIncludeCurrentContext.Text = "Include Current Context";
			this.cbxIncludeCurrentContext.UseVisualStyleBackColor = true;
			// 
			// btnLoadReference
			// 
			this.btnLoadReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadReference.Enabled = false;
			this.btnLoadReference.Location = new System.Drawing.Point(507, 122);
			this.btnLoadReference.Name = "btnLoadReference";
			this.btnLoadReference.Size = new System.Drawing.Size(163, 23);
			this.btnLoadReference.TabIndex = 45;
			this.btnLoadReference.Text = "Load Reference";
			this.btnLoadReference.UseVisualStyleBackColor = true;
			this.btnLoadReference.Click += new System.EventHandler(this.btnLoadReference_Click);
			// 
			// btnDeleteContent
			// 
			this.btnDeleteContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteContent.Enabled = false;
			this.btnDeleteContent.Location = new System.Drawing.Point(507, 93);
			this.btnDeleteContent.Name = "btnDeleteContent";
			this.btnDeleteContent.Size = new System.Drawing.Size(163, 23);
			this.btnDeleteContent.TabIndex = 44;
			this.btnDeleteContent.Text = "Delete...";
			this.btnDeleteContent.UseVisualStyleBackColor = true;
			this.btnDeleteContent.Click += new System.EventHandler(this.btnDeleteContent_Click);
			// 
			// btnUpdateContent
			// 
			this.btnUpdateContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdateContent.Enabled = false;
			this.btnUpdateContent.Location = new System.Drawing.Point(507, 64);
			this.btnUpdateContent.Name = "btnUpdateContent";
			this.btnUpdateContent.Size = new System.Drawing.Size(67, 23);
			this.btnUpdateContent.TabIndex = 43;
			this.btnUpdateContent.Text = "Update...";
			this.btnUpdateContent.UseVisualStyleBackColor = true;
			this.btnUpdateContent.Click += new System.EventHandler(this.btnUpdateContent_Click);
			// 
			// btnAddContent
			// 
			this.btnAddContent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddContent.Location = new System.Drawing.Point(507, 35);
			this.btnAddContent.Name = "btnAddContent";
			this.btnAddContent.Size = new System.Drawing.Size(67, 23);
			this.btnAddContent.TabIndex = 41;
			this.btnAddContent.Text = "Add new...";
			this.btnAddContent.UseVisualStyleBackColor = true;
			this.btnAddContent.Click += new System.EventHandler(this.btnAddContent_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 6);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(171, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "Content contained in current report";
			// 
			// lvContent
			// 
			this.lvContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvContent.CheckBoxes = true;
			this.lvContent.HideSelection = false;
			this.lvContent.Location = new System.Drawing.Point(6, 25);
			this.lvContent.MultiSelect = false;
			this.lvContent.Name = "lvContent";
			this.lvContent.Size = new System.Drawing.Size(473, 120);
			this.lvContent.TabIndex = 0;
			this.lvContent.UseCompatibleStateImageBehavior = false;
			// 
			// btnDownloadHubLogs
			// 
			this.btnDownloadHubLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDownloadHubLogs.Enabled = false;
			this.btnDownloadHubLogs.Location = new System.Drawing.Point(180, 525);
			this.btnDownloadHubLogs.Name = "btnDownloadHubLogs";
			this.btnDownloadHubLogs.Size = new System.Drawing.Size(123, 23);
			this.btnDownloadHubLogs.TabIndex = 14;
			this.btnDownloadHubLogs.Text = "Download Hub Logs";
			this.btnDownloadHubLogs.UseVisualStyleBackColor = true;
			this.btnDownloadHubLogs.Click += new System.EventHandler(this.btnDownloadHubLogs_Click);
			// 
			// btnOpenLogFolder
			// 
			this.btnOpenLogFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenLogFolder.Location = new System.Drawing.Point(309, 525);
			this.btnOpenLogFolder.Name = "btnOpenLogFolder";
			this.btnOpenLogFolder.Size = new System.Drawing.Size(123, 23);
			this.btnOpenLogFolder.TabIndex = 15;
			this.btnOpenLogFolder.Text = "Open Local Log Folder";
			this.btnOpenLogFolder.UseVisualStyleBackColor = true;
			this.btnOpenLogFolder.Click += new System.EventHandler(this.btnOpenLogFolder_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(731, 558);
			this.Controls.Add(this.btnOpenLogFolder);
			this.Controls.Add(this.btnDownloadHubLogs);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.txtLog);
			this.Name = "Form1";
			this.Text = "Test PowerCast";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabUpdateStudies.ResumeLayout(false);
			this.tabUpdateStudies.PerformLayout();
			this.tabReportOpenClose.ResumeLayout(false);
			this.tabReportOpenClose.PerformLayout();
			this.tabSubscribe.ResumeLayout(false);
			this.tabSubscribe.PerformLayout();
			this.tabLogin.ResumeLayout(false);
			this.tabLogin.PerformLayout();
			this.tabConfiguration.ResumeLayout(false);
			this.tabConfiguration.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabUpdateContent.ResumeLayout(false);
			this.tabUpdateContent.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolTip ttAccession;
        private System.Windows.Forms.TabPage tabUpdateStudies;
        private System.Windows.Forms.TabPage tabReportOpenClose;
        private System.Windows.Forms.Button btnNotify;
        private System.Windows.Forms.TabPage tabSubscribe;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.TextBox txtSubEvents;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.TabPage tabLogin;
        private System.Windows.Forms.TextBox txtLaunchUsername;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TabPage tabConfiguration;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnGetReport;
        private System.Windows.Forms.Button btnCloseReport;
        private System.Windows.Forms.Button btnGetConfig;
        private System.Windows.Forms.TextBox txtHubUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConfigHubUrl;
        private System.Windows.Forms.TextBox txtConfigTokenUrl;
        private System.Windows.Forms.TextBox txtConfigAuthUrl;
        private System.Windows.Forms.Button btnDownloadHubLogs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnOpenLogFolder;
        private System.Windows.Forms.TextBox txtConfigTopic;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUpdateStudy;
        private System.Windows.Forms.Button btnAddStudy;
        private System.Windows.Forms.ListView lvStudies;
        private System.Windows.Forms.TabPage tabUpdateContent;
        private System.Windows.Forms.Button btnUpdateContent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView lvContent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAddContent;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtMRN;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAccession;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDeleteStudy;
        private System.Windows.Forms.Button btnDeleteContent;
        private System.Windows.Forms.Button btnUnsubscribe;
        private System.Windows.Forms.TextBox txtLeaseSeconds;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnUserLogout;
        private System.Windows.Forms.Button btnLoadReference;
        private System.Windows.Forms.Button btnNotifyError;
        private System.Windows.Forms.Button btnSignReport;
        private System.Windows.Forms.Button btnPrelimReport;
        private System.Windows.Forms.CheckBox cbxIncludeCurrentContext;
        private System.Windows.Forms.Button btnCloseStudy;
        private System.Windows.Forms.Button btnOpenStudy;
        private System.Windows.Forms.Button buttonGetSubscriptions;
        private System.Windows.Forms.CheckBox useAuthToken;
        private System.Windows.Forms.TextBox txtAuthToken;
        private System.Windows.Forms.Label lblAuthToken;
		private System.Windows.Forms.Button btnDeleteDevice;
		private System.Windows.Forms.Button btnAddObservationDevice;
		private System.Windows.Forms.CheckBox chkMultipleInstances;
	}
}

