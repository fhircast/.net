namespace Nuance.PowerCast.TestPowerCast
{
	partial class SendForm
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
			this.txtSendJson = new System.Windows.Forms.TextBox();
			this.btnSendUpdateStudy = new System.Windows.Forms.Button();
			this.btnSendCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtSendJson
			// 
			this.txtSendJson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSendJson.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSendJson.Location = new System.Drawing.Point(13, 53);
			this.txtSendJson.Multiline = true;
			this.txtSendJson.Name = "txtSendJson";
			this.txtSendJson.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSendJson.Size = new System.Drawing.Size(754, 425);
			this.txtSendJson.TabIndex = 0;
			this.txtSendJson.WordWrap = false;
			// 
			// btnSendUpdateStudy
			// 
			this.btnSendUpdateStudy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnSendUpdateStudy.Location = new System.Drawing.Point(1, 484);
			this.btnSendUpdateStudy.Name = "btnSendUpdateStudy";
			this.btnSendUpdateStudy.Size = new System.Drawing.Size(154, 23);
			this.btnSendUpdateStudy.TabIndex = 1;
			this.btnSendUpdateStudy.Text = "Send Update";
			this.btnSendUpdateStudy.UseVisualStyleBackColor = true;
			// 
			// btnSendCancel
			// 
			this.btnSendCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnSendCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnSendCancel.Location = new System.Drawing.Point(161, 484);
			this.btnSendCancel.Name = "btnSendCancel";
			this.btnSendCancel.Size = new System.Drawing.Size(154, 23);
			this.btnSendCancel.TabIndex = 2;
			this.btnSendCancel.Text = "Cancel";
			this.btnSendCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(332, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Edit the Json text below and click send to update existing resources. ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(468, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Valid request methods are: POST (to add new), PUT (to update existing) and DELETE" +
    " (to remove)";
			// 
			// SendForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(779, 519);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSendCancel);
			this.Controls.Add(this.btnSendUpdateStudy);
			this.Controls.Add(this.txtSendJson);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SendForm";
			this.ShowInTaskbar = false;
			this.Text = "Update Report";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSendJson;
		private System.Windows.Forms.Button btnSendUpdateStudy;
		private System.Windows.Forms.Button btnSendCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}