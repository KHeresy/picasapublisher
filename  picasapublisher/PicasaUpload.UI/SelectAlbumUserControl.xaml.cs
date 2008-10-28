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

		public SelectAlbumUserControl(Picasa picasaApi)
		{
			this.InitializeComponent();
			_picasaApi = picasaApi;
			
			// Insert code required on object creation below this point.
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Albums = _picasaApi.GetAlbums();
		}
	}
}