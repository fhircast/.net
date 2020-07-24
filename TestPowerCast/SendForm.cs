using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nuance.PowerCast.TestPowerCast
{
	public partial class SendForm : Form
	{
		public string FinalJson = null;

		public SendForm(string json)
		{
			InitializeComponent();
			txtSendJson.Text = json;
			btnSendUpdateStudy.Click += BtnSendUpdateStudy_Click;
		}

		private void BtnSendUpdateStudy_Click(object sender, EventArgs e)
		{
			FinalJson = txtSendJson.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
