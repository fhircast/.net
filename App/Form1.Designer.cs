namespace dotnet.FHIR.app
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnSubscribe = new System.Windows.Forms.Button();
			this.txtSubEvents = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtNotMRN = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtNotAccession = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnNotify = new System.Windows.Forms.Button();
			this.txtNotEvent = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtNotTopic = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtHubUrl = new System.Windows.Forms.TextBox();
			this.txtSecret = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtUserName);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.btnSubscribe);
			this.groupBox1.Controls.Add(this.txtSubEvents);
			this.groupBox1.Controls.Add(this.txtHubUrl);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtSecret);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(310, 185);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Subscribe/Connect";
			// 
			// btnSubscribe
			// 
			this.btnSubscribe.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnSubscribe.Location = new System.Drawing.Point(229, 156);
			this.btnSubscribe.Name = "btnSubscribe";
			this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
			this.btnSubscribe.TabIndex = 10;
			this.btnSubscribe.Text = "Subscribe";
			this.btnSubscribe.UseVisualStyleBackColor = true;
			this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
			// 
			// txtSubEvents
			// 
			this.txtSubEvents.Location = new System.Drawing.Point(73, 124);
			this.txtSubEvents.Name = "txtSubEvents";
			this.txtSubEvents.Size = new System.Drawing.Size(231, 20);
			this.txtSubEvents.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 127);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Events:";
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(12, 203);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(593, 253);
			this.txtLog.TabIndex = 5;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtNotMRN);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.txtNotAccession);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.btnNotify);
			this.groupBox2.Controls.Add(this.txtNotEvent);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtNotTopic);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Location = new System.Drawing.Point(328, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(277, 185);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Notify";
			// 
			// txtNotMRN
			// 
			this.txtNotMRN.Location = new System.Drawing.Point(62, 104);
			this.txtNotMRN.Name = "txtNotMRN";
			this.txtNotMRN.Size = new System.Drawing.Size(199, 20);
			this.txtNotMRN.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 107);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(35, 13);
			this.label8.TabIndex = 23;
			this.label8.Text = "MRN:";
			// 
			// txtNotAccession
			// 
			this.txtNotAccession.Location = new System.Drawing.Point(62, 78);
			this.txtNotAccession.Name = "txtNotAccession";
			this.txtNotAccession.Size = new System.Drawing.Size(199, 20);
			this.txtNotAccession.TabIndex = 16;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 81);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(36, 13);
			this.label5.TabIndex = 21;
			this.label5.Text = "Acc#:";
			// 
			// btnNotify
			// 
			this.btnNotify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNotify.Enabled = false;
			this.btnNotify.Location = new System.Drawing.Point(196, 156);
			this.btnNotify.Name = "btnNotify";
			this.btnNotify.Size = new System.Drawing.Size(75, 23);
			this.btnNotify.TabIndex = 20;
			this.btnNotify.Text = "Notify";
			this.btnNotify.UseVisualStyleBackColor = true;
			this.btnNotify.Click += new System.EventHandler(this.btnNotify_Click);
			// 
			// txtNotEvent
			// 
			this.txtNotEvent.Location = new System.Drawing.Point(62, 52);
			this.txtNotEvent.Name = "txtNotEvent";
			this.txtNotEvent.Size = new System.Drawing.Size(199, 20);
			this.txtNotEvent.TabIndex = 14;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(4, 55);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Event:";
			// 
			// txtNotTopic
			// 
			this.txtNotTopic.Location = new System.Drawing.Point(62, 26);
			this.txtNotTopic.Name = "txtNotTopic";
			this.txtNotTopic.Size = new System.Drawing.Size(199, 20);
			this.txtNotTopic.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 29);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(37, 13);
			this.label7.TabIndex = 3;
			this.label7.Text = "Topic:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(231, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Base hub URL (ex: http://fhirhub.nuance.com):";
			// 
			// txtHubUrl
			// 
			this.txtHubUrl.Location = new System.Drawing.Point(6, 45);
			this.txtHubUrl.Name = "txtHubUrl";
			this.txtHubUrl.Size = new System.Drawing.Size(298, 20);
			this.txtHubUrl.TabIndex = 2;
			// 
			// txtSecret
			// 
			this.txtSecret.Location = new System.Drawing.Point(75, 71);
			this.txtSecret.Name = "txtSecret";
			this.txtSecret.Size = new System.Drawing.Size(229, 20);
			this.txtSecret.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "App Secret:";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(73, 97);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(231, 20);
			this.txtUserName.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(61, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "User name:";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(616, 466);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "FHIRCast Test App";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtSubEvents;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnSubscribe;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnNotify;
		private System.Windows.Forms.TextBox txtNotEvent;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtNotTopic;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtNotMRN;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtNotAccession;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtHubUrl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSecret;
		private System.Windows.Forms.Label label2;
	}
}

