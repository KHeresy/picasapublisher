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
            //_loadPhotoAlbumsBackground.DoWork += new DoWorkEventHandler(_loadPhotoAlbumsBackground_DoWork);
            //_loadPhotoAlbumsBackground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_loadPhotoAlbumsBackground_RunWorkerCompleted);
            //_loadPhotoAlbumsBackground.RunWorkerAsync(new Object[] {_picasaPublisherApi, _photoS

			_selectAlbumUserControl = new SelectAlbumUserControl(_picasaPublisherApi, _photoSize);
			_selectAlbumUserControl.CloseWindow += new SelectAlbumUserControl.CloseWindowHandler(_selectAlbumUserControl_CloseWindow);

			_wpfHost.Child = _selectAlbumUserControl;
		}



		void _selectAlbumUserControl_CloseWindow(object sender, EventArgs e)
		{
			DialogResult = _selectAlbumUserControl.OkClicked ? DialogResult.OK : DialogResult.Cancel;
            _photoSize = _selectAlbumUserControl.PhotoSize;
		}

        public Google.GData.Photos.PicasaEntry SelectedAlbumEntry { get { return _selectAlbumUserControl.SelectedAlbumEntry; } }
		public string SelectedAlbum { get { return _selectAlbumUserControl.SelectedAlbum; } }
        public string AlbumName { get { return _selectAlbumUserControl.AlbumName; } }
        public string AlbumSummary { get { return _selectAlbumUserControl.AlbumSummary; } }
        public string AlbumRights { get { return _selectAlbumUserControl.AlbumRights; } }
        private int _photoSize;
        public int PhotoSize { get { return _photoSize; } set { _photoSize = value; } }


        #region Thread to load data
        void _loadPhotoAlbumsBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
        void _loadPhotoAlbumsBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
