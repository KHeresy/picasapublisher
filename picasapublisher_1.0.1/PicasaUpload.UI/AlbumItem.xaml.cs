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
	public partial class AlbumItem
	{
		private PicasaEntry _albumEntry = null;
		public PicasaEntry AlbumEntry 
		{ 
			get { return _albumEntry; } 
			set 
			{ 
				_albumEntry = value;
				LoadAlbumEntry();
			} 
		}

		private void LoadAlbumEntry()
		{
			if (!IsLoaded)
			{
				return;
			}

			if (_albumEntry == null)
			{
				return;
			}

			_txtAlbumName.Text = _albumEntry.Title.Text;
		}

		public AlbumItem()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

		private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
		{
			LoadAlbumEntry();
		}
	}
}