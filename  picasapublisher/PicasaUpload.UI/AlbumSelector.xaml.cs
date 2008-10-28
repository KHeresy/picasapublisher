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


namespace PicasaUpload.UI
{
	public partial class AlbumSelector
	{
		public AlbumSelector()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

        private PicasaFeed _albums = null;
        public PicasaFeed Albums 
		{ 
			get { return _albums; } 
			set 
			{ 
				_albums = value;
				LoadAlbums();
			} 
		}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
			LoadAlbums();
        }

		private void LoadAlbums()
		{
			if (this.IsLoaded == false)
			{
				return;
			}
			if (_albums == null)
			{
				return;
			}

			//Clear out all of the children:
			_gridLayout.Children.Clear();

			//Setup the counts, and size of the grid layout
			int count = _albums.Entries.Count;
			int columns = 2;
			int rows = count / columns;

			_gridLayout.Rows = rows + 1;
			_gridLayout.Columns = columns;

			//Go through each album, and add an AlbumItem
			foreach (Google.GData.Client.AtomEntry entry in _albums.Entries)
			{
				PicasaEntry album = entry as PicasaEntry;
				if (album == null)
				{
					continue;
				}

				AlbumItem albumItem = new AlbumItem();
				albumItem.AlbumEntry = album;
				_gridLayout.Children.Add(albumItem);
			}
		}
    }
}