using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicasaUpload
{
	public partial class AlbumUserControl : UserControl
	{
		public AlbumUserControl()
		{
			InitializeComponent();
		}

		private void AlbumUserControl_Enter(object sender, EventArgs e)
		{
			BackColor = Color.BurlyWood;
		}

		private void AlbumUserControl_Leave(object sender, EventArgs e)
		{
			BackColor = Color.White;
		}

		private void AlbumUserControl_MouseEnter(object sender, EventArgs e)
		{
			BackColor = Color.Brown;
		}

		private void AlbumUserControl_MouseMove(object sender, MouseEventArgs e)
		{
			BackColor = Color.Black;
		}


	}
}
