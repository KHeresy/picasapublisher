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
		//KEep track if we are the album selected:
		private bool _selected = false;
		public bool Selected 
		{ 
			get 
			{ 
				return _selected; 
			} 
		}

		//Allows the user of this control to set the selected item:
		public void SetSelected(bool selected)
		{
			SetSelected(selected, false);
		}
		private void SetSelected(bool selected, bool fireEvent)
		{
			//if the selection state is not changing, get out:
			if (_selected == selected)
			{
				return;
			}
			_selected = selected;

			Storyboard selectedStoryBoard = (Storyboard)Resources["SelectedStoryboard"];
			if (selected)
			{
				selectedStoryBoard.Begin(this, true);			
			}
			else
			{
				selectedStoryBoard.Remove(this);
			}

			if (fireEvent)
			{
				OnSelectionChanged();
			}
		}

		#region Selection Changed Event -- Notify parent when the user changes selection
		//create an event, so the parent knows when our selection changed:
		public delegate void SelectionChangedHandler(object sender, EventArgs e);
		public event SelectionChangedHandler SelectionChanged;
		protected void OnSelectionChanged()
		{
			if (SelectionChanged == null)
			{
				return;
			}

			SelectionChanged(this, EventArgs.Empty);
		}
		#endregion

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

		private void LayoutRoot_GotFocus(object sender, RoutedEventArgs e)
		{
		}

		private void UserControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			SetSelected(true, true);
		}

		private void UserControl_LostFocus(object sender, RoutedEventArgs e)
		{
		}

		private void UserControl_GotFocus(object sender, RoutedEventArgs e)
		{
			SetSelected(true, true);
		}
	}
}