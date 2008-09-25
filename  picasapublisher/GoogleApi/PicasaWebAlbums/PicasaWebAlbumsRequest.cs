using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;

namespace GoogleApi.PicasaWebAlbums
{
	public class PicasaWebAlbumsRequest
	{
		private const string DEFAULT_USER_ID = "default";

		#region Album List
		public static AlbumList GetAlbumList( GoogleAuthorizationToken auth )
		{
			return GetAlbumList(DEFAULT_USER_ID, auth);
		}
		public static AlbumList GetAlbumList(string userId)
		{
			return GetAlbumList(userId, null);
		}

		private static AlbumList GetAlbumList(string userId, GoogleAuthorizationToken auth)
		{
			//Build up our request:
			string requestUri = string.Format("http://picasaweb.google.com/data/feed/api/user/{0}", userId);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

			AddAuthorizationRequestHeader(request, auth);

			request.Method = "GET";

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			StreamReader responseReader = new StreamReader(response.GetResponseStream());
			string albumListXml = responseReader.ReadToEnd();

			AlbumList al = new AlbumList();

			XmlDocument xd = new XmlDocument();
			xd.LoadXml(albumListXml);
			XmlNamespaceManager xnm = new XmlNamespaceManager(xd.NameTable);
			xnm.AddNamespace("gphoto", "http://schemas.google.com/photos/2007");

			XmlNodeList xmlEntries = xd.GetElementsByTagName("entry");
			foreach (XmlNode node in xmlEntries)
			{
				AlbumList.AlbumRow row = al.Album.NewAlbumRow();
				row.Id = node["gphoto:id"].InnerText;
				row.Name = node["gphoto:name"].InnerText;

				al.Album.Rows.Add(row);
			}
			return al;
		}

		private static void AddAuthorizationRequestHeader(HttpWebRequest request, GoogleAuthorizationToken auth)
		{
			if (auth == null)
			{
				return;
			}

			request.Headers.Add(string.Format("Authorization: GoogleLogin auth={0}", auth.AuthorizationToken));
		}
		#endregion



		#region CreateAlbum

		/// <summary>
		/// Takes the name of the album, and returns the id:
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static string CreateAlbum(string albumName, GoogleAuthorizationToken auth)
		{
			//Build up our request:
			string requestUri = string.Format("http://picasaweb.google.com/data/feed/api/user/default");
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

			AddAuthorizationRequestHeader(request, auth);

			request.Method = "POST";

			string newAlbumFormatString = @"<entry xmlns='http://www.w3.org/2005/Atom' " +
											@"xmlns:media='http://search.yahoo.com/mrss/' " +
												@"xmlns:gphoto='http://schemas.google.com/photos/2007'>" +
											  @"<title type='text'>{0}</title>" +
											  @"<summary type='text'></summary>" +
											  @"<gphoto:location></gphoto:location>" +
											  @"<gphoto:access>public</gphoto:access>" +
											  @"<gphoto:commentingEnabled>true</gphoto:commentingEnabled>" +
											  @"<gphoto:timestamp>{1:d}</gphoto:timestamp>" +
											  @"<category scheme='http://schemas.google.com/g/2005#kind' " +
												@"term='http://schemas.google.com/photos/2007#album'></category>" +
											@"</entry>";
			string newAlbumText = string.Format(newAlbumFormatString, albumName, (long)(DateTime.Now - new DateTime(1970,1,1)).TotalMilliseconds);

			UTF8Encoding encoding = new UTF8Encoding();
			byte[] byteData = encoding.GetBytes(newAlbumText);

			request.ContentLength = byteData.Length;
			request.ContentType = "application/atom+xml";

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(byteData, 0, byteData.Length);
			requestStream.Close();


			//Make the request:
			try
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader responseReader = new StreamReader(response.GetResponseStream());
				string albumListXml = responseReader.ReadToEnd();
				response.Close();

				XmlDocument xd = new XmlDocument();
				xd.LoadXml(albumListXml);
				XmlNamespaceManager xnm = new XmlNamespaceManager(xd.NameTable);
				xnm.AddNamespace("gphoto", "http://schemas.google.com/photos/2007");

				return xd.GetElementsByTagName("entry")[0]["gphoto:id"].InnerText;
			}
			catch( WebException wx )
			{
				HttpWebResponse response = (HttpWebResponse)wx.Response;
				StreamReader responseReader = new StreamReader(response.GetResponseStream());
				
				string error = responseReader.ReadToEnd();

				throw new Exception(error);
			}
		}

		#endregion

		#region UploadPhoto

		/// <summary>
		/// Upload a photot without any MedtaData.
		/// </summary>
		/// <param name="albumId"></param>
		/// <param name="imageStream"></param>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static bool PostPhotoWithoutMetadata(string albumId, Stream imageStream, string filename, GoogleAuthorizationToken auth)
		{
			//If the albumid is not specified, use default, and the image will be placed to a Drop Box:
			if (string.IsNullOrEmpty(albumId))
			{
				albumId = "default";
			}

			string albumFormatString = "http://picasaweb.google.com/data/feed/api/user/{0}/albumid/{1}";

			string requestUri = string.Format(albumFormatString, DEFAULT_USER_ID, albumId);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);

			AddAuthorizationRequestHeader(request, auth);

			request.Method = "POST";
			request.ContentType = "image/jpeg";
			request.ContentLength = imageStream.Length;

			if (!string.IsNullOrEmpty(filename))
			{
				request.Headers.Add(string.Format("Slug: {0}", filename));
			}

			Stream requestStream = request.GetRequestStream();
			byte[] buffer = new byte[imageStream.Length];
			imageStream.Read(buffer, 0, buffer.Length);
			requestStream.Write(buffer, 0, buffer.Length);
			requestStream.Close();


			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException wx)
			{
				response = (HttpWebResponse)wx.Response;
				StreamReader sr = new StreamReader(response.GetResponseStream());
				string error = sr.ReadToEnd();
				sr.Close();
				throw new Exception(error);
			}
			finally
			{
				response.Close();
			}
			return true;

		}

		#endregion
	}
}
