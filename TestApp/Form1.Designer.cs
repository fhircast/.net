namespace dotnet.FHIR.TestApp
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
			this.btnNotify = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.ttAccession = new System.Windows.Forms.ToolTip(this.components);
			this.txtAccession = new PlaceHolderTextBox();
			this.txtAccession.PlaceHolderText = "accession number";
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
			this.groupBox2.Controls.Add(this.txtAccession);
			this.groupBox2.Controls.Add(this.btnNotify);
			this.groupBox2.Location = new System.Drawing.Point(406, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(317, 175);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Notify";
			// 
			// btnNotify
			// 
			this.btnNotify.Location = new System.Drawing.Point(6, 26);
			this.btnNotify.Name = "btnNotify";
			this.btnNotify.Size = new System.Drawing.Size(85, 23);
			this.btnNotify.TabIndex = 22;
			this.btnNotify.Text = "Open Report";
			this.btnNotify.Click += new System.EventHandler(this.btnNotify_Click);
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
			// txtAccession
			// 
			this.txtAccession.Location = new System.Drawing.Point(97, 28);
			this.txtAccession.Name = "txtAccession";
			this.txtAccession.Size = new System.Drawing.Size(214, 20);
			this.txtAccession.TabIndex = 23;
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
		private System.Windows.Forms.TextBox txtTopic;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtHubUrl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.CheckBox chkWS;
		private System.Windows.Forms.ToolTip ttAccession;
		private PlaceHolderTextBox txtAccession;

	}
}

