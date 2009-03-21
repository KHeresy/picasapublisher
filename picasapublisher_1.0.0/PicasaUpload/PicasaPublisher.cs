using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoogleApi;
using System.Xml;

namespace PicasaUpload
{
	public class PicasaPublisher : Microsoft.WindowsLive.PublishPlugins.IPublishPlugin
	{
		/*
		 * Following: http://code.google.com/apis/picasaweb/developers_guide_protocol.html#Auth
		 * http://msdn.microsoft.com/en-us/library/cc967073.aspx
		 */
		#region IPublishPlugin Members

		public bool HasPublishResults(System.Xml.XmlDocument sessionXml)
		{
			return false;
		}

		public bool HasSummaryInformation(System.Xml.XmlDocument sessionXml)
		{
			return false;
		}

		public void LaunchPublishResults(System.Xml.XmlDocument sessionXml)
		{
			throw new NotImplementedException();
		}

		public bool PublishItem(System.Windows.Forms.IWin32Window parentWindow, string mediaObjectId, System.IO.Stream stream, System.Xml.XmlDocument sessionXml, Microsoft.WindowsLive.PublishPlugins.IPublishProperties publishProperties, Microsoft.WindowsLive.PublishPlugins.IPublishProgressCallback callback, System.Threading.EventWaitHandle cancelEvent)
		{
			//get our filename out of the sessionXml
			XmlNode itemPublishing = sessionXml.SelectSingleNode(string.Format("//PhotoGalleryPublishSession/ItemSet/Item[@id=\"{0}\"]",mediaObjectId));

			string filename = itemPublishing["OriginalFileName"].InnerText;

			//get our information out of session:
			XmlElement googleSettings = sessionXml.DocumentElement["GoogleSettings"];
			string googleAuthKey = googleSettings["AuthKey"].InnerText;
			string albumId = googleSettings["AlbumId"].InnerText;

			return GoogleApi.PicasaWebAlbums.PicasaWebAlbumsRequest.PostPhotoWithoutMetadata(albumId, stream, filename, new GoogleApi.GoogleAuthorizationToken(googleAuthKey));

		}

		public bool ShowConfigurationSettings(System.Windows.Forms.IWin32Window parentWindow, System.Xml.XmlDocument sessionXml, System.Xml.XmlDocument persistXml, Microsoft.WindowsLive.PublishPlugins.IPublishProperties publishProperties)
		{
			try
			{
				PicasaPubliserForm form = new PicasaPubliserForm();

				if (form.ShowDialog() == DialogResult.OK)
				{
					XmlElement settings = sessionXml["GoogleSettings"];
					XmlElement authKey = null;
					XmlElement albumId = null;
					if (settings == null)
					{
						settings = sessionXml.CreateElement("GoogleSettings");
						authKey = sessionXml.CreateElement("AuthKey");
						albumId = sessionXml.CreateElement("AlbumId");
						sessionXml.DocumentElement.AppendChild(settings);
						settings.AppendChild(authKey);
						settings.AppendChild(albumId);
					}
					else
					{
						authKey = settings["AuthKey"];
						albumId = settings["AlbumId"];
					}

					authKey.InnerText = form.GoogleAuthKey.AuthorizationToken;
					albumId.InnerText = form.SelectedAlbumId;

					return true;
				}
			}
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			///*set the settings */

			return false;
		}

		public void ShowSummaryInformation(System.Windows.Forms.IWin32Window parentWindow, System.Xml.XmlDocument sessionXml)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
