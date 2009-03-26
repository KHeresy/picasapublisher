using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PicasaUpload.UI;
using PicasaUpload.GoogleApi;

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
            string filename = @"D:\house\Pictures\2009-01-05 - Various\100_0960.JPG";

            SelectAlbumDataSet ds = PicasaUpload.UI.SelectAlbum.SelectAlbumUI(true, "mlsteeves@gmail.com", DateTime.Now, false, 1600);
            SelectAlbumDataSet.SelectAlbumTableRow selectedAlbumRow = (SelectAlbumDataSet.SelectAlbumTableRow)ds.SelectAlbumTable.Rows[0];

            
            Picasa pic = new Picasa(SelectAlbum.APP_NAME, selectedAlbumRow.AuthenticationToken);
            Google.GData.Photos.PicasaFeed feed = pic.GetAlbums();
            
            pic.PostPhoto(selectedAlbumRow.SelectedAlbumEntry, File.OpenRead(filename), filename);
            
		}

		private void button2_Click(object sender, EventArgs e)
		{
			PicasaUpload.UI.SelectAlbum.SelectAlbumUI(true, "mlsteeves@gmail.com", DateTime.Now, false, 1600);
		}
	}
}
