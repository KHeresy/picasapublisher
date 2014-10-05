using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PicasaUpload.UI;
using PicasaUpload.GoogleApi;
using Google.GData.Photos;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;


namespace PicasaUpload
{
	/// <summary>
	/// This is where we interface with Windows Live Photot Gallery:
	/// </summary>
	public class PicasaPublisher : Microsoft.WindowsLive.PublishPlugins.IPublishPlugin
	{
		private int iPhotoSize = 2048;

		#region constants
		private const string PERSIST_NODE_NAME = "PicasaPublisherPersistInfo";
		private const string REMEMBER_USER_EMAIL_NODE_NAME = "rememberUserEmail";
		private const string USER_EMAIL_NODE_NAME = "userEmail";
		private const string LAST_UPDATE_CHECK_NODE_NAME = "lastUpdateCheck";
		private const string LAST_UPDATE_VALUE_NODE_NAME = "lastUpdateValue";
		private const string PHOTO_SIZE_NODE_NAME = "photoSize";
		private const string RESULT_URL_NODE_NAME = "resultUrl";
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
			return true;
		}

		public bool HasSummaryInformation(System.Xml.XmlDocument sessionXml)
		{
			return false;
		}

		public void LaunchPublishResults(System.Xml.XmlDocument sessionXml)
		{
			XmlElement googleSettings = sessionXml.DocumentElement[GOOGLE_SETTINGS_NODE_NAME];
			string resultUrl = googleSettings[RESULT_URL_NODE_NAME].InnerText;

			Process.Start(resultUrl);

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
			iPhotoSize = int.Parse(sessionXml.SelectSingleNode("//MaxWidth").InnerText);
			if( iPhotoSize > 0 )
			{
				Image mImg = Image.FromStream(stream);
				// check size
				if (mImg.Width > iPhotoSize || mImg.Height > iPhotoSize)
				{
					// compute new size
					int nWidth, nHeight;
					if (mImg.Width > mImg.Height)
					{
						nWidth = iPhotoSize;
						nHeight = mImg.Height * iPhotoSize / mImg.Width;
					}
					else
					{
						nHeight = iPhotoSize;
						nWidth = mImg.Width * iPhotoSize / mImg.Height;
					}

					// resize
					Image resizedImage = new Bitmap(mImg, new Size(nWidth, nHeight));

					// copy all property
					foreach (int idx in mImg.PropertyIdList)
						resizedImage.SetPropertyItem(mImg.GetPropertyItem(idx));

					// save to stream
					System.IO.MemoryStream ms = new System.IO.MemoryStream();
					resizedImage.Save(ms, ImageFormat.Jpeg);
					stream = ms;
				}
				stream.Seek(0, System.IO.SeekOrigin.Begin);
			}

			//get our filename out of the sessionXml
			XmlNode itemPublishing = sessionXml.SelectSingleNode(string.Format("//PhotoGalleryPublishSession/ItemSet/Item[@id=\"{0}\"]",mediaObjectId));

			string filename = itemPublishing["OriginalFileName"].InnerText;

			//get our information out of session:
			XmlElement googleSettings = sessionXml.DocumentElement[GOOGLE_SETTINGS_NODE_NAME];
			string googleAuthKey = googleSettings[AUTH_KEY_NODE_NAME].InnerText;
			string albumName = googleSettings[SELECTED_ALBUM_NODE_NAME].InnerText;
			XmlElement albumUrlNode = googleSettings[RESULT_URL_NODE_NAME];

			try
			{
				GoogleApi.Picasa picasa = new Picasa(SelectAlbum.APP_NAME, googleAuthKey);
				PicasaEntry newPic = picasa.PostPhoto(albumName, stream, filename);

				//album feed is:
				if (string.IsNullOrEmpty(albumUrlNode.InnerText))
				{
					string albumUrl = newPic.AlternateUri.ToString();
					albumUrl = albumUrl.Substring(0, albumUrl.LastIndexOf('#')+1);
					albumUrlNode.InnerText = albumUrl;
				}

				UpdatePhotoWithSession(itemPublishing, newPic);
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
				string albumId = string.Empty;
				string resultUri = string.Empty;
				if (selectedAlbumRow.UseDefaultAlbum)
				{
					albumId = "default";
				}
				else
				{
					albumId = selectedAlbumRow.SelectedAlbumEntry.Id.AbsoluteUri.Substring(selectedAlbumRow.SelectedAlbumEntry.Id.AbsoluteUri.LastIndexOf('/') + 1);
					resultUri = selectedAlbumRow.SelectedAlbumEntry.AlternateUri.ToString();
				}
				SaveSessionInformation(sessionXml, selectedAlbumRow.AuthenticationToken, albumId, selectedAlbumRow.PhotoSize, resultUri);
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

			XmlElement lastUpdateCheckElement = persistXmlNode[LAST_UPDATE_CHECK_NODE_NAME];
			if (lastUpdateCheckElement != null)
			{
				lastUpdateCheck = DateTime.ParseExact(lastUpdateCheckElement.InnerText, DATE_FORMAT,System.Globalization.CultureInfo.InvariantCulture);
			}

			XmlElement lastUpdateValueElement = persistXmlNode[LAST_UPDATE_VALUE_NODE_NAME];
			if (lastUpdateValueElement != null)
			{
				lastUpdateValue = bool.Parse(lastUpdateValueElement.InnerText);
			}

			XmlElement photoSizeElement = persistXmlNode[PHOTO_SIZE_NODE_NAME];
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
		private static void SaveSessionInformation(System.Xml.XmlDocument sessionXml, 
													string authToken, 
													string selectedAlbumId, 
													int photoSize,
													string resultUrlText)
		{

			XmlNode photoGalleryPublishSession = sessionXml["PhotoGalleryPublishSession"];

			//update maxWidth, maxHeight:
			XmlNode publishParameters = photoGalleryPublishSession["PublishParameters"];
			publishParameters["MaxWidth"].InnerText = photoSize.ToString();
			publishParameters["MaxHeight"].InnerText = photoSize.ToString();


			XmlElement settings = photoGalleryPublishSession[GOOGLE_SETTINGS_NODE_NAME];
			XmlElement authKey = null;
			XmlElement albumId = null;
			XmlElement resultUrl = null;
			if (settings == null)
			{
				settings = sessionXml.CreateElement(GOOGLE_SETTINGS_NODE_NAME);
				authKey = sessionXml.CreateElement(AUTH_KEY_NODE_NAME);
				albumId = sessionXml.CreateElement(SELECTED_ALBUM_NODE_NAME);
				resultUrl = sessionXml.CreateElement(RESULT_URL_NODE_NAME);
				photoGalleryPublishSession.AppendChild(settings);
				settings.AppendChild(authKey);
				settings.AppendChild(albumId);
				settings.AppendChild(resultUrl);
			}
			else
			{
				authKey = settings[AUTH_KEY_NODE_NAME];
				albumId = settings[SELECTED_ALBUM_NODE_NAME];
				resultUrl = settings[RESULT_URL_NODE_NAME];
			}

			authKey.InnerText = authToken;
			albumId.InnerText = selectedAlbumId;
			resultUrl.InnerText = resultUrlText;
		}
		/// <summary>
		/// This function will take a new picasaEntry, and update any applicable fields from the sessionXml:
		/// </summary>
		/// <param name="sessionXml"></param>
		/// <param name="newPic"></param>
		private static void UpdatePhotoWithSession(System.Xml.XmlNode itemNode, PicasaEntry newPic)
		{
			/* session looks like so:
			 * <?xml version=\"1.0\"?>
			 * <PhotoGalleryPublishSession versionMajor=\"1\" versionMinor=\"0\">
			 *  <PublishParameters>
			 *      <MaxWidth>1600</MaxWidth>
			 *      <MaxHeight>1600</MaxHeight>
			 *  </PublishParameters>
			 *  <ItemSet>
			 *      <Item id=\"18811\">
			 *          <FullFilePath>D:\\house\\Pictures\\2009-05-09 - April &amp; May Misc\\100_2766.JPG</FullFilePath>
			 *          <OriginalFileName>100_2766.JPG</OriginalFileName>
			 *          <OriginalFileExtension>.JPG</OriginalFileExtension>
			 *          <PerceivedType>image</PerceivedType>
			 *          <Title>That's close!</Title>
			 *          <OriginalWidth>2832</OriginalWidth>
			 *          <OriginalHeight>1888</OriginalHeight>
			 *          <LengthMS>0</LengthMS>
			 *          <FileSize>1043545</FileSize>
			 *          <KeywordSet>
			 *              <Keyword>People/Jaxen</Keyword>
			 *              <Keyword>Pets/Toffee</Keyword>
			 *              <Keyword>Jaxen-April 2009</Keyword>
			 *          </KeywordSet>
			 *          <PeopleRegionSet>
			 *              <PersonRegion left=\"0.565345080763583\" top=\"3.52422907488987E-02\" width=\"0.244493392070485\" height=\"0.366740088105727\">Jaxen</PersonRegion>
			 *          </PeopleRegionSet>
			 *      </Item>
			 *  </ItemSet>
			 *  <GoogleSettings>
			 *      <AuthKey>Google auth key</AuthKey>
			 *      <AlbumId>album id</AlbumId>
			 *  </GoogleSettings>
			 * </PhotoGalleryPublishSession>
			 */

			/* mappings:
			 * Title = summary
			 * KeywordSEt = Tags
			 * PeopleRegionSet = People Tags  //picasa doesn't support this yet!
			 */
			bool changedPic = false;

			//title -> summary:
			string summary = string.Empty;
			XmlNode titleNode = itemNode["Title"];
			if (titleNode != null)
			{
				summary = titleNode.InnerText;

				if (!string.IsNullOrEmpty(summary))
				{
					newPic.Summary.Text = summary;
					changedPic = true;
				}
			}

			//keywordset -> keywords
			XmlNode keywordSet = itemNode["KeywordSet"];
			if (keywordSet != null)
			{
				StringBuilder tags = new StringBuilder();
				foreach (XmlNode child in keywordSet.ChildNodes)
				{
					tags.Append(child.InnerText);
					tags.Append(",");
				}

				if (tags.Length > 0)
				{
					//remove the last comma:
					tags.Length = tags.Length - 1;

					newPic.Media.Keywords.Value = tags.ToString();
					changedPic = true;
				}
			}

			//if the picture changed, then update it:
			if (changedPic)
			{
				newPic.Update();
			}

		}

	}
}
