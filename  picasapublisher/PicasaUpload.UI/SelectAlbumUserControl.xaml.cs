using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Google.GData.Photos;
using PicasaUpload.GoogleApi;

namespace PicasaUpload.UI
{
	public partial class SelectAlbumUserControl
	{
		private Picasa _picasaApi;
		public PicasaFeed Albums { get { return _albumSelectedUC.Albums; } set { _albumSelectedUC.Albums = value; } }

        private int _photoSize;
        public int PhotoSize { get { return _photoSize; } set { _photoSize = value; } }

		public SelectAlbumUserControl(Picasa picasaApi, int photoSize)
		{
			this.InitializeComponent();
			_picasaApi = picasaApi;

            _photoSize = photoSize;
			
			// Insert code required on object creation below this point.
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Albums = _picasaApi.GetAlbums();

            //load photosize:
            LoadPhotoSizeCombo();
		}

        private void LoadPhotoSizeCombo()
        {
            int selectedIndex = 0;

            if (_photoSize == 2048)
            {
                selectedIndex = 1;
            }
            else if (_photoSize == 1600)
            {
                selectedIndex = 2;
            }
            else if (_photoSize == 800)
            {
                selectedIndex = 3;
            }

            _cboPhotoSize.SelectedIndex = selectedIndex;
        }
        private void SavePhotoSizeCombo()
        {
            _photoSize = int.Parse((string)((ComboBoxItem)_cboPhotoSize.SelectedItem).Tag);
        }


		#region Dialog Management
		private bool _okClicked = false;
		public bool OkClicked { get { return _okClicked; } }

		private void _cmdOK_Click(object sender, RoutedEventArgs e)
		{
			_okClicked = true;
			OnCloseWindow();
		}

		private void _cmdCancel_Click(object sender, RoutedEventArgs e)
		{
			OnCloseWindow();
		}

		public delegate void CloseWindowHandler(object sender, EventArgs e);
		public event CloseWindowHandler CloseWindow;
		protected void OnCloseWindow()
		{
            //validate:
            if (ValidateUI() == false)
            {
                return;
            }

			if( CloseWindow == null )
			{
				return;
			}

            //set photoSize based on cbo:
            SavePhotoSizeCombo();

			CloseWindow(this, EventArgs.Empty);
		}


        //is the UI ok:
        private bool ValidateUI()
        {
            if (string.IsNullOrEmpty(SelectedAlbum))
            {
                if (string.IsNullOrEmpty(AlbumName))
                {
                    MessageBox.Show("Please enter an Album Name.", "Enter an Album Name");
                    return false;
                }
            }

            return true;

        }


		public string SelectedAlbum { get { return _albumSelectedUC.SelectedAlbum; } }
        public PicasaEntry SelectedAlbumEntry { get { return _albumSelectedUC.SelectedAlbumEntry; } }
        public string AlbumName { get { return _albumSelectedUC.SelectedAlbumName; } }
        public string AlbumSummary { get { return _albumSelectedUC.SelectedAlbumSummary; } }
        public string AlbumRights { get { return _albumSelectedUC.SelectedAlbumRights; } }

		#endregion

	}
}