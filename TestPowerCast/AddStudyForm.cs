using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Nuance.PowerCast.TestPowerCast
{
	public partial class AddStudyForm : Form
	{
		public string studyJson = null;

		public AddStudyForm()
		{
			InitializeComponent();
		}

		private void btnAddStudy_Click(object sender, EventArgs e)
		{
			studyJson = txtAddStudyJson.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			//create the ImagingStudy json using form data
			ImagingStudy study = Common.CreateImagingStudy(txtAddAccession.Text, dtStudy.Value.ToString("yyyy-MM-dd"), rbCurrentStudy.Checked);
			var serializer = new FhirJsonSerializer(new SerializerSettings() { Pretty = true });
			txtAddStudyJson.Text = serializer.SerializeToString(study);
		}
	}
}
