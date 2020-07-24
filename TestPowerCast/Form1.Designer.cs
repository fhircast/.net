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
			this.tabUpdateStudies = new System.Windows.Forms.TabPage();
			this.btnDeleteStudy = new System.Windows.Forms.Button();
			this.btnUpdateStudy = new System.Windows.Forms.Button();
			this.btnAddStudy = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.lvStudies = new System.Windows.Forms.ListView();
			this.tabReportOpenClose = new System.Windows.Forms.TabPage();
			this.txtMRN = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtAccession = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.btnGetReport = new System.Windows.Forms.Button();
			this.btnCloseReport = new System.Windows.Forms.Button();
			this.btnNotify = new System.Windows.Forms.Button();
			this.tabSubscribe = new System.Windows.Forms.TabPage();
			this.txtHubUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTopic = new System.Windows.Forms.TextBox();
			this.txtSubEvents = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.rbUseResthook = new System.Windows.Forms.RadioButton();
			this.rbWebsocket = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.btnSubscribe = new System.Windows.Forms.Button();
			this.tabLogin = new System.Windows.Forms.TabPage();
			this.txtLaunchUsername = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.tabConfiguration = new System.Windows.Forms.TabPage();
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
			this.tabUpdateObs = new System.Windows.Forms.TabPage();
			this.btnUpdateObservation = new System.Windows.Forms.Button();
			this.btnAddObservation = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.lvObservations = new System.Windows.Forms.ListView();
			this.btnDownloadHubLogs = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.btnOpenLogFolder = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnDeleteObservation = new System.Windows.Forms.Button();
			this.tabUpdateStudies.SuspendLayout();
			this.tabReportOpenClose.SuspendLayout();
			this.tabSubscribe.SuspendLayout();
			this.tabLogin.SuspendLayout();
			this.tabConfiguration.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabUpdateObs.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLog
			// 
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
			this.btnCopy.Location = new System.Drawing.Point(99, 525);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 12;
			this.btnCopy.Text = "Copy Log";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
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
			this.tabReportOpenClose.Text = "Report Open/Close/Get";
			this.tabReportOpenClose.UseVisualStyleBackColor = true;
			// 
			// txtMRN
			// 
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
			this.btnGetReport.Location = new System.Drawing.Point(188, 43);
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
			this.tabSubscribe.Controls.Add(this.txtHubUrl);
			this.tabSubscribe.Controls.Add(this.label1);
			this.tabSubscribe.Controls.Add(this.txtTopic);
			this.tabSubscribe.Controls.Add(this.txtSubEvents);
			this.tabSubscribe.Controls.Add(this.label4);
			this.tabSubscribe.Controls.Add(this.rbUseResthook);
			this.tabSubscribe.Controls.Add(this.rbWebsocket);
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
			// txtHubUrl
			// 
			this.txtHubUrl.Location = new System.Drawing.Point(72, 15);
			this.txtHubUrl.Name = "txtHubUrl";
			this.txtHubUrl.ReadOnly = true;
			this.txtHubUrl.Size = new System.Drawing.Size(478, 20);
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
			this.txtTopic.Location = new System.Drawing.Point(72, 42);
			this.txtTopic.Name = "txtTopic";
			this.txtTopic.ReadOnly = true;
			this.txtTopic.Size = new System.Drawing.Size(478, 20);
			this.txtTopic.TabIndex = 17;
			// 
			// txtSubEvents
			// 
			this.txtSubEvents.Location = new System.Drawing.Point(72, 69);
			this.txtSubEvents.Name = "txtSubEvents";
			this.txtSubEvents.Size = new System.Drawing.Size(478, 20);
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
			// rbUseResthook
			// 
			this.rbUseResthook.AutoSize = true;
			this.rbUseResthook.Location = new System.Drawing.Point(112, 101);
			this.rbUseResthook.Name = "rbUseResthook";
			this.rbUseResthook.Size = new System.Drawing.Size(96, 17);
			this.rbUseResthook.TabIndex = 23;
			this.rbUseResthook.Text = "Use Rest-hook";
			this.rbUseResthook.UseVisualStyleBackColor = true;
			// 
			// rbWebsocket
			// 
			this.rbWebsocket.AutoSize = true;
			this.rbWebsocket.Checked = true;
			this.rbWebsocket.Location = new System.Drawing.Point(4, 101);
			this.rbWebsocket.Name = "rbWebsocket";
			this.rbWebsocket.Size = new System.Drawing.Size(102, 17);
			this.rbWebsocket.TabIndex = 21;
			this.rbWebsocket.TabStop = true;
			this.rbWebsocket.Text = "Use Websocket";
			this.rbWebsocket.UseVisualStyleBackColor = true;
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
			this.btnSubscribe.Anchor = System.Windows.Forms.AnchorStyles.None;
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
			// txtLaunchUsername
			// 
			this.txtLaunchUsername.Location = new System.Drawing.Point(6, 12);
			this.txtLaunchUsername.Name = "txtLaunchUsername";
			this.txtLaunchUsername.Size = new System.Drawing.Size(214, 20);
			this.txtLaunchUsername.TabIndex = 1;
			// 
			// btnLogin
			// 
			this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
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
			// txtConfigTopic
			// 
			this.txtConfigTopic.Location = new System.Drawing.Point(150, 123);
			this.txtConfigTopic.Name = "txtConfigTopic";
			this.txtConfigTopic.ReadOnly = true;
			this.txtConfigTopic.Size = new System.Drawing.Size(533, 20);
			this.txtConfigTopic.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 13);
			this.label5.TabIndex = 24;
			this.label5.Text = "Topic:";
			// 
			// txtConfigHubUrl
			// 
			this.txtConfigHubUrl.Location = new System.Drawing.Point(150, 95);
			this.txtConfigHubUrl.Name = "txtConfigHubUrl";
			this.txtConfigHubUrl.ReadOnly = true;
			this.txtConfigHubUrl.Size = new System.Drawing.Size(533, 20);
			this.txtConfigHubUrl.TabIndex = 7;
			// 
			// txtConfigTokenUrl
			// 
			this.txtConfigTokenUrl.Location = new System.Drawing.Point(150, 66);
			this.txtConfigTokenUrl.Name = "txtConfigTokenUrl";
			this.txtConfigTokenUrl.ReadOnly = true;
			this.txtConfigTokenUrl.Size = new System.Drawing.Size(533, 20);
			this.txtConfigTokenUrl.TabIndex = 5;
			// 
			// txtConfigAuthUrl
			// 
			this.txtConfigAuthUrl.Location = new System.Drawing.Point(150, 37);
			this.txtConfigAuthUrl.Name = "txtConfigAuthUrl";
			this.txtConfigAuthUrl.ReadOnly = true;
			this.txtConfigAuthUrl.Size = new System.Drawing.Size(533, 20);
			this.txtConfigAuthUrl.TabIndex = 3;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(7, 69);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(88, 13);
			this.label8.TabIndex = 17;
			this.label8.Text = "Auth0 Token Url:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 98);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "PowerCast Hub Url:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Auth0 Authorization Url:";
			// 
			// btnGetConfig
			// 
			this.btnGetConfig.Anchor = System.Windows.Forms.AnchorStyles.None;
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
			this.tabControl1.Controls.Add(this.tabConfiguration);
			this.tabControl1.Controls.Add(this.tabLogin);
			this.tabControl1.Controls.Add(this.tabSubscribe);
			this.tabControl1.Controls.Add(this.tabReportOpenClose);
			this.tabControl1.Controls.Add(this.tabUpdateStudies);
			this.tabControl1.Controls.Add(this.tabUpdateObs);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(707, 177);
			this.tabControl1.TabIndex = 13;
			// 
			// tabUpdateObs
			// 
			this.tabUpdateObs.Controls.Add(this.btnDeleteObservation);
			this.tabUpdateObs.Controls.Add(this.btnUpdateObservation);
			this.tabUpdateObs.Controls.Add(this.btnAddObservation);
			this.tabUpdateObs.Controls.Add(this.label9);
			this.tabUpdateObs.Controls.Add(this.lvObservations);
			this.tabUpdateObs.Location = new System.Drawing.Point(4, 22);
			this.tabUpdateObs.Name = "tabUpdateObs";
			this.tabUpdateObs.Padding = new System.Windows.Forms.Padding(3);
			this.tabUpdateObs.Size = new System.Drawing.Size(699, 151);
			this.tabUpdateObs.TabIndex = 5;
			this.tabUpdateObs.Text = "Observations";
			this.tabUpdateObs.UseVisualStyleBackColor = true;
			// 
			// btnUpdateObservation
			// 
			this.btnUpdateObservation.Enabled = false;
			this.btnUpdateObservation.Location = new System.Drawing.Point(507, 54);
			this.btnUpdateObservation.Name = "btnUpdateObservation";
			this.btnUpdateObservation.Size = new System.Drawing.Size(155, 23);
			this.btnUpdateObservation.TabIndex = 43;
			this.btnUpdateObservation.Text = "Update...";
			this.btnUpdateObservation.UseVisualStyleBackColor = true;
			this.btnUpdateObservation.Click += new System.EventHandler(this.btnUpdateObservation_Click);
			// 
			// btnAddObservation
			// 
			this.btnAddObservation.Location = new System.Drawing.Point(507, 25);
			this.btnAddObservation.Name = "btnAddObservation";
			this.btnAddObservation.Size = new System.Drawing.Size(155, 23);
			this.btnAddObservation.TabIndex = 41;
			this.btnAddObservation.Text = "Add new...";
			this.btnAddObservation.UseVisualStyleBackColor = true;
			this.btnAddObservation.Click += new System.EventHandler(this.btnAddObservation_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 6);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(196, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "Observations contained in current report";
			// 
			// lvObservations
			// 
			this.lvObservations.CheckBoxes = true;
			this.lvObservations.HideSelection = false;
			this.lvObservations.Location = new System.Drawing.Point(6, 25);
			this.lvObservations.MultiSelect = false;
			this.lvObservations.Name = "lvObservations";
			this.lvObservations.Size = new System.Drawing.Size(473, 120);
			this.lvObservations.TabIndex = 0;
			this.lvObservations.UseCompatibleStateImageBehavior = false;
			// 
			// btnDownloadHubLogs
			// 
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
			// btnDeleteObservation
			// 
			this.btnDeleteObservation.Enabled = false;
			this.btnDeleteObservation.Location = new System.Drawing.Point(507, 83);
			this.btnDeleteObservation.Name = "btnDeleteObservation";
			this.btnDeleteObservation.Size = new System.Drawing.Size(155, 23);
			this.btnDeleteObservation.TabIndex = 44;
			this.btnDeleteObservation.Text = "Delete...";
			this.btnDeleteObservation.UseVisualStyleBackColor = true;
			this.btnDeleteObservation.Click += new System.EventHandler(this.btnDeleteObservation_Click);
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
			this.tabUpdateObs.ResumeLayout(false);
			this.tabUpdateObs.PerformLayout();
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
		private System.Windows.Forms.RadioButton rbUseResthook;
		private System.Windows.Forms.RadioButton rbWebsocket;
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
		private System.Windows.Forms.TabPage tabUpdateObs;
		private System.Windows.Forms.Button btnUpdateObservation;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListView lvObservations;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnAddObservation;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox txtMRN;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtAccession;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnDeleteStudy;
		private System.Windows.Forms.Button btnDeleteObservation;
	}
}

