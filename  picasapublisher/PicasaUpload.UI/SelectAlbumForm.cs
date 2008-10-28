using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicasaUpload.UI
{
	public partial class SelectAlbumForm : Form
	{
		private PicasaUpload.GoogleApi.Picasa _picasaPublisherApi;
		public PicasaUpload.GoogleApi.Picasa PicasaPublisherApi
		{
			get { return _picasaPublisherApi; }
			set
			{
				_picasaPublisherApi = value;
			}
		}

		public SelectAlbumForm()
		{
			InitializeComponent();
		}

		SelectAlbumUserControl _selectAlbumUserControl;
		private void SelectAlbumForm_Load(object sender, EventArgs e)
		{
			_selectAlbumUserControl = new SelectAlbumUserControl(_picasaPublisherApi);

			_wpfHost.Child = _selectAlbumUserControl;
		}
	}
}
