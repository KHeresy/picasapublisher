using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoogleApi;
using System.Xml;

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
		private const string PERSIST_XML_FORMAT = "<PicasaPublisherPersistInfo><rememberUserEmail>{0}</rememberUserEmail><userEmail>{1}</userEmail></PicasaPublisherPersistInfo>";
		private const string GOOGLE_SETTINGS_NODE_NAME = "GoogleSettings";
		private const string AUTH_KEY_NODE_NAME = "AuthKey";
		private const string SELECTED_ALBUM_NODE_NAME = "AlbumId";

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
			string albumId = googleSettings[SELECTED_ALBUM_NODE_NAME].InnerText;

			//Actually post the photo:
			return GoogleApi.PicasaWebAlbums.PicasaWebAlbumsRequest.PostPhotoWithoutMetadata(albumId, stream, filename, new GoogleApi.GoogleAuthorizationToken(googleAuthKey));

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
				LoginForm form = new LoginForm();

				//User may have choosen to save their username, so get that information from PersistXml:
				bool rememberUserEmail;
				string userEmail;
				LoadPersistInformation(persistXml, out rememberUserEmail, out userEmail);

				form.RememberUserEmail = rememberUserEmail;
				form.UserEmail = userEmail;


				if (form.ShowDialog() == DialogResult.OK)
				{
					//We need to remember the Google Authorization Token, and the album the user selected:

					SaveSessionInformation(sessionXml, form.GoogleAuthKey, form.SelectedAlbumId);

					SavePersistInformation(persistXml, form.RememberUserEmail, form.UserEmail);

					return true;
				}
			}
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}


			return false;
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
		private void LoadPersistInformation(XmlDocument persistXml, out bool rememberUserEmail, out string userEmail)
		{
			//initialize our out variables
			rememberUserEmail = false;
			userEmail = string.Empty;

			//Get our xml node, and return if it does not exist:
			XmlNode persistXmlNode = persistXml[PERSIST_NODE_NAME];
			if (persistXmlNode == null)
			{
				return;
			}

			//Get our values from our persisXmlNode:
			rememberUserEmail = bool.Parse(persistXmlNode[REMEMBER_USER_EMAIL_NODE_NAME].InnerText);
			userEmail = persistXmlNode[USER_EMAIL_NODE_NAME].InnerText;

		}

		/// <summary>
		/// Saves the persist information:
		/// </summary>
		/// <param name="persistXml"></param>
		/// <param name="rememberUserEmail"></param>
		/// <param name="userEmail"></param>
		private void SavePersistInformation(XmlDocument persistXml, bool rememberUserEmail, string userEmail)
		{
			//Get our persist node format (If we start saving more then this, then we might have to start manipulating the XmlDocument directly:
			string persistNodeXml = string.Format(PERSIST_XML_FORMAT, rememberUserEmail.ToString(), rememberUserEmail ? userEmail : string.Empty);
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
		private static void SaveSessionInformation(System.Xml.XmlDocument sessionXml, GoogleApi.GoogleAuthorizationToken token, string selectedAlbumId)
		{
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

			authKey.InnerText = token.AuthorizationToken;
			albumId.InnerText = selectedAlbumId;
		}

	}
}
