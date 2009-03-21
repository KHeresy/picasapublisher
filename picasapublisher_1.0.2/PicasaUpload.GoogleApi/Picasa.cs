using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
            AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri("default"));

            PicasaFeed feed = _picasaService.Query(query);

            return feed;
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
    }
}
