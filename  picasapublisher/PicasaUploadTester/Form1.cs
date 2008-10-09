using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PicasaUploadTester
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		
		private void _cmdTestLogin_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			PicasaUpload.LoginForm form = new PicasaUpload.LoginForm();
			if (form.ShowDialog() == DialogResult.OK)
			{
				//choose file
				openFileDialog1.Filter = "Image Files|*.jpeg,*.jpg,*.png,*.gif,*.bmp|All Files|*.*";

				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					string filename = openFileDialog1.FileName;

					GoogleApi.PicasaWebAlbums.PicasaWebAlbumsRequest.PostPhotoWithoutMetadata(form.SelectedAlbumId, File.OpenRead(filename), Path.GetFileName(filename), form.GoogleAuthKey);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			PicasaUpload.UI.SelectAlbum.SelectAlbumUI();
		}
	}
}
