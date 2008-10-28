using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Google.GData.Extensions.MediaRss;
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
			
			//is there a thumbnail, and if so, look for the closest to 160x160:
			int thumbnails = _albumEntry.Media.Thumbnails.Count;
			MediaThumbnail displayPicture = null;
			foreach( MediaThumbnail thumb in _albumEntry.Media.Thumbnails)
			{
				//for now we'll just default to the first thumbnail.
				//Have to do some reading to see iff the 160x160 thumbnail is guaranteed
				if( displayPicture == null )
				{
					displayPicture = thumb;
				}
				int width = int.Parse(thumb.Width);
				int height = int.Parse(thumb.Height);
				
				if( width == 160 && height == 160 )
				{
					displayPicture = thumb;
				}
			}

			if( displayPicture != null )
			{
				//TODO: Images not loading:
				//I don't think we can use these URL's directly, maybe have to load it up through
				//another picasa object.  (I'm thinking that you may have to be logged in)
				//BitmapImage img = new BitmapImage(new Uri(displayPicture.Url));
				//_albumPicture.Source = img; 

				//something like this?
				//PhotoQuery query = new PhotoQuery(displayPicture.Url);
				
			}
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