﻿namespace dotnet.FHIR.app
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
			this.chkWS = new System.Windows.Forms.CheckBox();
			this.txtTopic = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSubscribe = new System.Windows.Forms.Button();
			this.txtSubEvents = new System.Windows.Forms.TextBox();
			this.txtHubUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtNotAccession = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnNotify = new System.Windows.Forms.Button();
			this.txtNotEvent = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtNotTopic = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkWS);
			this.groupBox1.Controls.Add(this.txtTopic);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.btnSubscribe);
			this.groupBox1.Controls.Add(this.txtSubEvents);
			this.groupBox1.Controls.Add(this.txtHubUrl);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(388, 175);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Subscribe";
			// 
			// chkWS
			// 
			this.chkWS.AutoSize = true;
			this.chkWS.Location = new System.Drawing.Point(92, 146);
			this.chkWS.Name = "chkWS";
			this.chkWS.Size = new System.Drawing.Size(103, 17);
			this.chkWS.TabIndex = 13;
			this.chkWS.Text = "use WebSocket";
			this.chkWS.UseVisualStyleBackColor = true;
			// 
			// txtTopic
			// 
			this.txtTopic.Location = new System.Drawing.Point(73, 81);
			this.txtTopic.Name = "txtTopic";
			this.txtTopic.Size = new System.Drawing.Size(301, 20);
			this.txtTopic.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(37, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Topic:";
			// 
			// btnSubscribe
			// 
			this.btnSubscribe.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnSubscribe.Location = new System.Drawing.Point(6, 142);
			this.btnSubscribe.Name = "btnSubscribe";
			this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
			this.btnSubscribe.TabIndex = 10;
			this.btnSubscribe.Text = "Subscribe";
			this.btnSubscribe.UseVisualStyleBackColor = true;
			this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
			// 
			// txtSubEvents
			// 
			this.txtSubEvents.Location = new System.Drawing.Point(73, 109);
			this.txtSubEvents.Name = "txtSubEvents";
			this.txtSubEvents.Size = new System.Drawing.Size(301, 20);
			this.txtSubEvents.TabIndex = 8;
			// 
			// txtHubUrl
			// 
			this.txtHubUrl.Location = new System.Drawing.Point(6, 45);
			this.txtHubUrl.Name = "txtHubUrl";
			this.txtHubUrl.Size = new System.Drawing.Size(368, 20);
			this.txtHubUrl.TabIndex = 2;
			this.txtHubUrl.Text = "https://connect.nuancepowerscribe.com/powercast";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(354, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Base hub URL (ex: https://connect.nuancepowerscribe.com/powercast):";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Events:";
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(12, 209);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(711, 275);
			this.txtLog.TabIndex = 5;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtNotAccession);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.btnNotify);
			this.groupBox2.Controls.Add(this.txtNotEvent);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtNotTopic);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Location = new System.Drawing.Point(406, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(317, 175);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Notify";
			// 
			// txtNotAccession
			// 
			this.txtNotAccession.Location = new System.Drawing.Point(62, 78);
			this.txtNotAccession.Name = "txtNotAccession";
			this.txtNotAccession.Size = new System.Drawing.Size(249, 20);
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
			this.btnNotify.Location = new System.Drawing.Point(236, 146);
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
			this.txtNotEvent.Size = new System.Drawing.Size(249, 20);
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
			this.txtNotTopic.Size = new System.Drawing.Size(249, 20);
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
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(18, 501);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 11;
			this.btnClear.Text = "Clear Log";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(104, 501);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 12;
			this.btnCopy.Text = "Copy Log";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(735, 535);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnClear);
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
		private System.Windows.Forms.TextBox txtNotAccession;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtTopic;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtHubUrl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.CheckBox chkWS;
	}
}

