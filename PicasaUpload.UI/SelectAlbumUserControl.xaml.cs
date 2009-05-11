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
using System.Threading;

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
            _lblLoading.Visibility = Visibility.Visible;
            _pbLoading.Visibility = Visibility.Visible;

            ThreadStart threadDelegate = new ThreadStart(LoadAlbums);
            Thread newThread = new Thread(threadDelegate);
            newThread.Start();

            //load photosize:
            LoadPhotoSizeCombo();
		}

        private void LoadAlbums()
        {
            try
            {
                LoadAlbumsCompleted(_picasaApi.GetAlbums());
            }
            catch (Exception ex)
            {
                LoadAlbumsErrored(ex);
            }
        }

        private delegate void LoadAlbumsCompletedDelegate(PicasaFeed albums);
        private void LoadAlbumsCompleted(PicasaFeed albums)
        {
            //are we good to change stuff
            if (this.Dispatcher.Thread == Thread.CurrentThread)
            {
                Albums = albums;
                _lblLoading.Visibility = Visibility.Hidden;
                _pbLoading.Visibility = Visibility.Hidden;
            }
            else
            {
                //no, so call invoke:
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (LoadAlbumsCompletedDelegate)LoadAlbumsCompleted, albums);
            }            
        }

        private delegate void LoadAlbumsErroredDelegate(Exception ex);
        private void LoadAlbumsErrored(Exception ex)
        {
            try
            {
                //are we good to change stuff
                if (this.Dispatcher.Thread == Thread.CurrentThread)
                {
                    MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error", MessageBoxButton.OK);
                }
                else
                {
                    //no, so call invoke:
                    this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (LoadAlbumsErroredDelegate)LoadAlbumsErrored, ex);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(string.Format("Error: {0}", x.Message), "Error", MessageBoxButton.OK);
            }
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
            SavePhotoSizeCombo();
            DialogResult = true;
		}

		private void _cmdCancel_Click(object sender, RoutedEventArgs e)
		{
            DialogResult = false;
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