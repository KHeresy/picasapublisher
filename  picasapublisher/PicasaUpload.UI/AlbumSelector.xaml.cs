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
			int count = _albums.Entries.Count + 1;	//add one for the new item:
			int columns = 2;
			int rows = count / columns;

			_gridLayout.Rows = rows + 1;
			_gridLayout.Columns = columns;

			//add a blank item at the top.  This is the one for the user to enter a new album:
			AlbumItem blankItem = new AlbumItem();
			blankItem.SelectionChanged += new AlbumItem.SelectionChangedHandler(albumItem_SelectionChanged);
			_gridLayout.Children.Add(blankItem);

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

				albumItem.SelectionChanged += new AlbumItem.SelectionChangedHandler(albumItem_SelectionChanged);
			}
		}

		#region "Selected Album"
		private AlbumItem _selectedAlbumControl = null;
		void albumItem_SelectionChanged(object sender, EventArgs e)
		{
			if (_selectedAlbumControl != null)
			{
				_selectedAlbumControl.SetSelected(false);
			}
			_selectedAlbumControl = (AlbumItem)sender;
		}

		public string SelectedAlbum 
        { 
			get 
			{
				if (_selectedAlbumControl.AlbumEntry == null)
				{
					return string.Empty;
				}

				return _selectedAlbumControl.AlbumEntry.Id.Uri.ToString(); 
			} 
		}

        public string SelectedAlbumName { get { return _selectedAlbumControl.AlbumName; } }
        public string SelectedAlbumSummary { get { return _selectedAlbumControl.AlbumSummary; } }
        public string SelectedAlbumRights { get { return _selectedAlbumControl.AlbumRights; } }


		#endregion




	}
}