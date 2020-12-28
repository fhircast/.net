using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nuance.PowerCast.TestPowerCast
{
	public partial class ImageForm : Form
	{
		public ImageForm()
		{
			InitializeComponent();
		}

		public void LoadImage(byte[] data)
		{
			MemoryStream mStream = new MemoryStream();
			mStream.Write(data, 0, Convert.ToInt32(data.Length));
			Bitmap bm = new Bitmap(mStream, false);
			mStream.Dispose();
			pictureBox1.Image = bm;
			pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
		}
	}
}
