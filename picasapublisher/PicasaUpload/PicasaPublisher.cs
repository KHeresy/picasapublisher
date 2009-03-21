using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PicasaUpload.UI;
using PicasaUpload.GoogleApi;

namespace PicasaUpload
{
	/// <summary>
	/// This is where we interface with Windows Live Photot Gallery:
	/// </summary>
	public class PicasaPublisher : Microsoft.WindowsLive.PublishPlugins.IPublishPlugin
	{
		#region constants
		private const string PERSIST_NODE_NAME = "PicasaPublisherPersistInfo";
		private const string REMEMBER_USER_EMAIL_NODE_NAME = "rememberUserEmail";
		private const string USER_EMAIL_NODE_NAME = "userEmail";
        private const string LAST_UPDATE_CHECK_NODE_NAME = "lastUpdateCheck";
        private const string LAST_UPDATE_VALUE_NODE_NAME = "lastUpdateValue";
        private const string PHOTO_SIZE_NODE_NAME = "photoSize";
		private const string PERSIST_XML_FORMAT = "<PicasaPublisherPersistInfo><rememberUserEmail>{0}</rememberUserEmail><userEmail>{1}</userEmail><lastUpdateCheck>{2}</lastUpdateCheck><lastUpdateValue>{3}</lastUpdateValue><photoSize>{4:d}</photoSize></PicasaPublisherPersistInfo>";
		private const string GOOGLE_SETTINGS_NODE_NAME = "GoogleSettings";
		private const string AUTH_KEY_NODE_NAME = "AuthKey";
		private const string SELECTED_ALBUM_NODE_NAME = "AlbumId";
        private const string DATE_FORMAT = "yyyy-MM-dd";


		#endregion
		/*
		 * Documentation for Photo Gallery plugin:
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

		/// <summary>
		/// This function is called to publish individual photos:
		/// </summary>
		/// <param name="parentWindow"></param>
		/// <param name="mediaObjectId"></param>
		/// <param name="stream"></param>
		/// <param name="sessionXml"></param>
		/// <param name="publishProperties"></param>
		/// <param name="callback"></param>
		/// <param name="cancelEvent"></param>
		/// <returns></returns>
		public bool PublishItem(System.Windows.Forms.IWin32Window parentWindow, string mediaObjectId, System.IO.Stream stream, System.Xml.XmlDocument sessionXml, Microsoft.WindowsLive.PublishPlugins.IPublishProperties publishProperties, Microsoft.WindowsLive.PublishPlugins.IPublishProgressCallback callback, System.Threading.EventWaitHandle cancelEvent)
		{
			//get our filename out of the sessionXml
			XmlNode itemPublishing = sessionXml.SelectSingleNode(string.Format("//PhotoGalleryPublishSession/ItemSet/Item[@id=\"{0}\"]",mediaObjectId));

			string filename = itemPublishing["OriginalFileName"].InnerText;

			//get our information out of session:
			XmlElement googleSettings = sessionXml.DocumentElement[GOOGLE_SETTINGS_NODE_NAME];
			string googleAuthKey = googleSettings[AUTH_KEY_NODE_NAME].InnerText;
			string albumName = googleSettings[SELECTED_ALBUM_NODE_NAME].InnerText;

			//Actually post the photo:
			//return GoogleApi.PicasaWebAlbums.PicasaWebAlbumsRequest.PostPhotoWithoutMetadata(albumName, stream, filename, new GoogleApi.GoogleAuthorizationToken(googleAuthKey));

            try
            {
                GoogleApi.Picasa picasa = new Picasa(SelectAlbum.APP_NAME, googleAuthKey);
                picasa.PostPhoto(albumName, stream, filename);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;

		}

		/// <summary>
		/// This function will display the login dialog box, and allow the user to select an album:
		/// </summary>
		/// <param name="parentWindow"></param>
		/// <param name="sessionXml"></param>
		/// <param name="persistXml"></param>
		/// <param name="publishProperties"></param>
		/// <returns></returns>
		public bool ShowConfigurationSettings(System.Windows.Forms.IWin32Window parentWindow, System.Xml.XmlDocument sessionXml, System.Xml.XmlDocument persistXml, Microsoft.WindowsLive.PublishPlugins.IPublishProperties publishProperties)
		{
			try
			{
				//User may have choosen to save their username, so get that information from PersistXml:
				bool rememberUserEmail;
				string userEmail;
                DateTime lastUpdateCheck;
                bool updateAtLastCheck;
                int photoSize;
				LoadPersistInformation(persistXml, out rememberUserEmail, out userEmail, out lastUpdateCheck, out updateAtLastCheck, out photoSize);

                SelectAlbumDataSet albumSelectedDS = SelectAlbum.SelectAlbumUI(rememberUserEmail, userEmail, lastUpdateCheck, updateAtLastCheck, photoSize);
                if (albumSelectedDS == null)
                {
                    return false;
                }

                SelectAlbumDataSet.SelectAlbumTableRow selectedAlbumRow = (SelectAlbumDataSet.SelectAlbumTableRow)albumSelectedDS.SelectAlbumTable.Rows[0];
                SaveSessionInformation(sessionXml, selectedAlbumRow.AuthenticationToken, selectedAlbumRow.SelectedAlbumEntry.Title.Text, selectedAlbumRow.PhotoSize);
                SavePersistInformation(persistXml, selectedAlbumRow.RememberUsername, selectedAlbumRow.Username, selectedAlbumRow.LastCheckForUpdate, selectedAlbumRow.LastUpdateValue, selectedAlbumRow.PhotoSize);

                return true;
			}
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}


		public void ShowSummaryInformation(System.Windows.Forms.IWin32Window parentWindow, System.Xml.XmlDocument sessionXml)
		{
			throw new NotImplementedException();
		}

		#endregion




		/// <summary>
		/// Loads the persist information:
		/// </summary>
		/// <param name="persistXml"></param>
		/// <param name="rememberUserEmail"></param>
		/// <param name="userEmail"></param>
		private void LoadPersistInformation(XmlDocument persistXml, out bool rememberUserEmail, out string userEmail, out DateTime lastUpdateCheck, out bool lastUpdateValue, out int photoSize)
		{
			//initialize our out variables
			rememberUserEmail = false;
			userEmail = string.Empty;
            lastUpdateCheck = DateTime.MinValue;
            lastUpdateValue = false;
            photoSize = 0;

			//Get our xml node, and return if it does not exist:
			XmlNode persistXmlNode = persistXml[PERSIST_NODE_NAME];
			if (persistXmlNode == null)
			{
				return;
			}

			//Get our values from our persisXmlNode:
			rememberUserEmail = bool.Parse(persistXmlNode[REMEMBER_USER_EMAIL_NODE_NAME].InnerText);
			userEmail = persistXmlNode[USER_EMAIL_NODE_NAME].InnerText;

			XmlElement lastUpdateCheckElement = persistXml[LAST_UPDATE_CHECK_NODE_NAME];
			if (lastUpdateCheckElement != null)
			{
				lastUpdateCheck = DateTime.ParseExact(DATE_FORMAT, lastUpdateCheckElement.InnerText, System.Globalization.CultureInfo.InvariantCulture);
			}

			XmlElement lastUpdateValueElement = persistXml[LAST_UPDATE_VALUE_NODE_NAME];
			if (lastUpdateValueElement != null)
			{
				lastUpdateValue = bool.Parse(lastUpdateValueElement.InnerText);
			}

            XmlElement photoSizeElement = persistXml[PHOTO_SIZE_NODE_NAME];
            if (photoSizeElement != null)
            {
                photoSize = int.Parse(photoSizeElement.InnerText);
            }

		}

		/// <summary>
		/// Saves the persist information:
		/// </summary>
		/// <param name="persistXml"></param>
		/// <param name="rememberUserEmail"></param>
		/// <param name="userEmail"></param>
        private void SavePersistInformation(XmlDocument persistXml, bool rememberUserEmail, string userEmail, DateTime lastUpdateCheck, bool lastUpdateValue, int photoSize)
		{
			//Get our persist node format (If we start saving more then this, then we might have to start manipulating the XmlDocument directly:
			string persistNodeXml = string.Format(PERSIST_XML_FORMAT, 
                                                    rememberUserEmail.ToString(), 
                                                    rememberUserEmail ? userEmail : string.Empty,
                                                    lastUpdateCheck.ToString(DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture), 
                                                    lastUpdateValue.ToString(),
                                                    photoSize
                                                 );
			XmlDocument newPersistDoc = new XmlDocument();
			newPersistDoc.LoadXml(persistNodeXml);

			//Remove our old node, if it exists:
			XmlNode persistXmlNode = persistXml[PERSIST_NODE_NAME];
			if (persistXmlNode != null)
			{
				persistXml.RemoveChild(persistXmlNode);
			}

			//Get our new Persist Xml Node:
			persistXmlNode = newPersistDoc[PERSIST_NODE_NAME];

			//Import this node to our new document:
			XmlNode importedNode = persistXml.ImportNode(persistXmlNode, true);
			persistXml.AppendChild(importedNode);
		}

		/// <summary>
		/// This function will save session information to the sessionXml:
		/// </summary>
		/// <param name="sessionXml"></param>
		/// <param name="token"></param>
		/// <param name="selectedAlbumId"></param>
		private static void SaveSessionInformation(System.Xml.XmlDocument sessionXml, string authToken, string selectedAlbumId, int photoSize)
		{

            //update maxWidth, maxHeight:
            XmlNode publishParameters = sessionXml["PublishParameters"];
            publishParameters["MaxWidth"].InnerText = photoSize.ToString();
            publishParameters["MaxHeight"].InnerText = photoSize.ToString();


			XmlElement settings = sessionXml[GOOGLE_SETTINGS_NODE_NAME];
			XmlElement authKey = null;
			XmlElement albumId = null;
			if (settings == null)
			{
				settings = sessionXml.CreateElement(GOOGLE_SETTINGS_NODE_NAME);
				authKey = sessionXml.CreateElement(AUTH_KEY_NODE_NAME);
				albumId = sessionXml.CreateElement(SELECTED_ALBUM_NODE_NAME);
				sessionXml.DocumentElement.AppendChild(settings);
				settings.AppendChild(authKey);
				settings.AppendChild(albumId);
			}
			else
			{
				authKey = settings[AUTH_KEY_NODE_NAME];
				albumId = settings[SELECTED_ALBUM_NODE_NAME];
			}

			authKey.InnerText = authToken;
			albumId.InnerText = selectedAlbumId;
		}

	}
}
