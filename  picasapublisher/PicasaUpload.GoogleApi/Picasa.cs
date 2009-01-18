using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Photos;
using System.IO;


namespace PicasaUpload.GoogleApi
{
    /// <summary>
    /// This class communicates through the Google GData api to retrieve information 
    /// from the google app server.
    /// </summary>
    public class Picasa
    {
        private const string DEFAULT_USER_ID = "default";

        private PicasaService _picasaService;

        /// <summary>
        /// Logs the user into Google
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns the AuthenticationToken.</returns>
        public string Login(string username, string password)
        {
            _picasaService.setUserCredentials(username, password);

            return _picasaService.QueryAuthenticationToken();
        }

        /// <summary>
        /// Returns a PicasaFeed of the albums for the logged in user:
        /// </summary>
        /// <returns>The PicasaFeed of albums.</returns>
        public PicasaFeed GetAlbums()
        {
            AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri(DEFAULT_USER_ID));

            PicasaFeed feed = _picasaService.Query(query);

            return feed;
        }

        /// <summary>
        /// Creates an album
        /// </summary>
        /// <param name="albumName"></param>
        /// <param name="albumSummary"></param>
        /// <param name="albumRights"></param>
        /// <returns></returns>
        public PicasaEntry CreateAlbum(string albumName, string albumSummary, string albumRights)
        {
            AlbumEntry newAlbum = new AlbumEntry();
            newAlbum.Title.Text = albumName;
            newAlbum.Summary.Text = albumSummary;

            AlbumAccessor ac = new AlbumAccessor(newAlbum);
            //set to "private" for a private album
            ac.Access = albumRights;

            Uri feedUri = new Uri(PicasaQuery.CreatePicasaUri(DEFAULT_USER_ID));

            PicasaEntry createdEntry = (PicasaEntry)_picasaService.Insert(feedUri, newAlbum);

            return createdEntry;            
        }

        /// <summary>
        /// Posts a photo:
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="photoStream"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public PicasaEntry PostPhoto(PicasaEntry albumEntry, Stream photoStream, string filename)
        {
            //it is album name, not ID
            Uri postUri = new Uri(PicasaQuery.CreatePicasaUri(DEFAULT_USER_ID, albumEntry.Title.Text));

            PicasaEntry entry = (PicasaEntry)_picasaService.Insert(postUri, photoStream, "image/jpeg", filename);

            return entry;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appName"></param>
        public Picasa( string appName)
        {
            _picasaService = new PicasaService(appName);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="authenticationToken"></param>
        public Picasa(string appName, string authenticationToken)
        {
            _picasaService = new PicasaService(appName);
            _picasaService.SetAuthenticationToken(authenticationToken);
        }


        #region static methods

        /// <summary>
        /// ID's are sometimes hidden at the end of a long url, returns only the ID;
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetIdFromUrl(string url)
        {
            int lastSlash = url.LastIndexOf('/');
            if (lastSlash < 0)
            {
                return url;
            }

            return url.Substring(lastSlash + 1);
        }

        #endregion

    }
}
