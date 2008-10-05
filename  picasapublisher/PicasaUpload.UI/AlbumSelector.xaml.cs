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
        public PicasaFeed Albums { get { return _albums; } set { _albums = value; } }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_albums == null)
            {
                return;
            }
            int count = _albums.Entries.Count;
            int columns = 2;
            int rows = count / columns;
            _gridLayout.Rows = rows;
            _gridLayout.Columns = columns;

            foreach( Google.GData.Client.AtomEntry entry in _albums.Entries )
            {
                AlbumEntry album = entry as AlbumEntry;
                if( album == null )
                {
                    continue;
                }

                AlbumItem albumItem = new AlbumItem();
                _gridLayout.Children.Add( albumItem );
            }
        }
    }
}