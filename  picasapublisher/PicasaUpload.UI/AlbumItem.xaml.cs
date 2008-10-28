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
using System.Windows.Interop;

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

			//Initialize this as a new album:
			if (_albumEntry == null)
			{
				_txtAlbumName.Text = "<New Album>";
				_txtAlbumSummary.Text = "<Enter Summary>";
				_cboAlbumRights.SelectedIndex = 0;

				_albumPicture.Source = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.NewAlbumImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
				return;
			}

			//Make everything readonly:
			_txtAlbumName.IsReadOnly = true;
			_txtAlbumSummary.IsReadOnly = true;
			_cboAlbumRights.IsEnabled = false;


			
			_txtAlbumName.Text = _albumEntry.Title.Text;
			_txtAlbumSummary.Text = _albumEntry.Summary.Text;
			string rights = _albumEntry.Rights.Text;
			_cboAlbumRights.SelectedIndex = (rights == "public" ? 0 : 1);

						
			//is there a thumbnail, and if so, look for the closest to 160x160:
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
				//2008-10-28 - Images not loading:
				//I don't think we can use these URL's directly, maybe have to load it up through
				//another picasa object.  (I'm thinking that you may have to be logged in)

				//2008-10-28 - They are working now, maybe a problem with my network?
				BitmapImage img = new BitmapImage(new Uri(displayPicture.Url));
				_albumPicture.Source = img; 
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