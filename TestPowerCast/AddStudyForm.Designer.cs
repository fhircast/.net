namespace Nuance.PowerCast.TestPowerCast
{
	partial class AddStudyForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtAddAccession = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.dtStudy = new System.Windows.Forms.DateTimePicker();
			this.rbCurrentStudy = new System.Windows.Forms.RadioButton();
			this.rbPrior = new System.Windows.Forms.RadioButton();
			this.btnAddStudy = new System.Windows.Forms.Button();
			this.btnCancelAddStudy = new System.Windows.Forms.Button();
			this.txtAddStudyJson = new System.Windows.Forms.TextBox();
			this.btnApplyStudy = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Accession Number";
			// 
			// txtAddAccession
			// 
			this.txtAddAccession.Location = new System.Drawing.Point(175, 10);
			this.txtAddAccession.Name = "txtAddAccession";
			this.txtAddAccession.Size = new System.Drawing.Size(200, 20);
			this.txtAddAccession.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Exam Date";
			// 
			// dtStudy
			// 
			this.dtStudy.Location = new System.Drawing.Point(175, 38);
			this.dtStudy.Name = "dtStudy";
			this.dtStudy.Size = new System.Drawing.Size(200, 20);
			this.dtStudy.TabIndex = 4;
			// 
			// rbCurrentStudy
			// 
			this.rbCurrentStudy.AutoSize = true;
			this.rbCurrentStudy.Checked = true;
			this.rbCurrentStudy.Location = new System.Drawing.Point(16, 70);
			this.rbCurrentStudy.Name = "rbCurrentStudy";
			this.rbCurrentStudy.Size = new System.Drawing.Size(85, 17);
			this.rbCurrentStudy.TabIndex = 5;
			this.rbCurrentStudy.TabStop = true;
			this.rbCurrentStudy.Text = "Active Study";
			this.rbCurrentStudy.UseVisualStyleBackColor = true;
			// 
			// rbPrior
			// 
			this.rbPrior.AutoSize = true;
			this.rbPrior.Location = new System.Drawing.Point(114, 70);
			this.rbPrior.Name = "rbPrior";
			this.rbPrior.Size = new System.Drawing.Size(136, 17);
			this.rbPrior.TabIndex = 6;
			this.rbPrior.Text = "Prior/Comparison Study";
			this.rbPrior.UseVisualStyleBackColor = true;
			// 
			// btnAddStudy
			// 
			this.btnAddStudy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnAddStudy.Location = new System.Drawing.Point(17, 451);
			this.btnAddStudy.Name = "btnAddStudy";
			this.btnAddStudy.Size = new System.Drawing.Size(91, 23);
			this.btnAddStudy.TabIndex = 11;
			this.btnAddStudy.Text = "Add";
			this.btnAddStudy.UseVisualStyleBackColor = true;
			this.btnAddStudy.Click += new System.EventHandler(this.btnAddStudy_Click);
			// 
			// btnCancelAddStudy
			// 
			this.btnCancelAddStudy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancelAddStudy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelAddStudy.Location = new System.Drawing.Point(114, 451);
			this.btnCancelAddStudy.Name = "btnCancelAddStudy";
			this.btnCancelAddStudy.Size = new System.Drawing.Size(91, 23);
			this.btnCancelAddStudy.TabIndex = 12;
			this.btnCancelAddStudy.Text = "Cancel";
			this.btnCancelAddStudy.UseVisualStyleBackColor = true;
			// 
			// txtAddStudyJson
			// 
			this.txtAddStudyJson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtAddStudyJson.Font = new System.Drawing.Font("Lucida Sans Typewriter", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAddStudyJson.Location = new System.Drawing.Point(16, 134);
			this.txtAddStudyJson.Multiline = true;
			this.txtAddStudyJson.Name = "txtAddStudyJson";
			this.txtAddStudyJson.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtAddStudyJson.Size = new System.Drawing.Size(631, 304);
			this.txtAddStudyJson.TabIndex = 9;
			this.txtAddStudyJson.WordWrap = false;
			// 
			// btnApplyStudy
			// 
			this.btnApplyStudy.Location = new System.Drawing.Point(16, 105);
			this.btnApplyStudy.Name = "btnApplyStudy";
			this.btnApplyStudy.Size = new System.Drawing.Size(91, 23);
			this.btnApplyStudy.TabIndex = 7;
			this.btnApplyStudy.Text = "Apply";
			this.btnApplyStudy.UseVisualStyleBackColor = true;
			this.btnApplyStudy.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// AddStudyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 486);
			this.ControlBox = false;
			this.Controls.Add(this.btnApplyStudy);
			this.Controls.Add(this.txtAddStudyJson);
			this.Controls.Add(this.btnCancelAddStudy);
			this.Controls.Add(this.btnAddStudy);
			this.Controls.Add(this.rbPrior);
			this.Controls.Add(this.rbCurrentStudy);
			this.Controls.Add(this.dtStudy);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtAddAccession);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddStudyForm";
			this.Text = "Add Study";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtAddAccession;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtStudy;
		private System.Windows.Forms.RadioButton rbCurrentStudy;
		private System.Windows.Forms.RadioButton rbPrior;
		private System.Windows.Forms.Button btnAddStudy;
		private System.Windows.Forms.Button btnCancelAddStudy;
		private System.Windows.Forms.TextBox txtAddStudyJson;
		private System.Windows.Forms.Button btnApplyStudy;
	}
}