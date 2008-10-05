using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicasaUpload.GoogleApi;
using System.Data;

namespace PicasaUpload.UI
{
    public static class SelectAlbum
    {
        public static string APP_NAME = "MLS-PicassaLivePublisher-2";
        public static string DT_TAB_SELECT_ALBUM_NAME = "SelectAlbumData";
        public static string DT_COL_REMEMBER_USERNAME = "RememberUsername";
        public static string DT_COL_USERNAME = "Username";
        public static string DT_COL_AUTHENTICATION_TOKEN = "AuthenticationToken";
        public static string DT_COL_SELECTED_ALBUM_ID = "SelectedAlbumId";

        /// <summary>
        /// This function will display all windows necessary for the user to select an album
        /// </summary>
        /// <returns>The ID of the photo album</returns>
        public static SelectAlbumDataSet SelectAlbumUI()
        {
            Picasa picasa = new Picasa(APP_NAME);
            LoginWindow login = new LoginWindow();

            //Login
            if (login.ShowDialog() == true)
            {
                
            }

            string username = login.Username;
            bool rememberUsername = login.RememberUsername;
            string authenticationToken = picasa.Login(username, login.Password);
            
            
            //Display UI for selecting album:
            string selectedAlbumId = string.Empty;
            SelectAlbumWindow selectAlbum = new SelectAlbumWindow();
            //selectAlbum.


            
            //Create new album if necessary


            //Gather up what needs to be returned to the user:
            return BuildSelectAlbumUIDatatable(rememberUsername, username, authenticationToken, selectedAlbumId);
        }

        private static SelectAlbumDataSet BuildSelectAlbumUIDatatable(bool rememberUsername, string username, string authenticationToken, string selectedAlbumId)
        {
            //this should be a strongly typed dataset:
            SelectAlbumDataSet ret = new SelectAlbumDataSet();
            SelectAlbumDataSet.SelectAlbumTableRow row = ret.SelectAlbumTable.NewSelectAlbumTableRow();
            row.SelectedAlbumId = selectedAlbumId;
            row.Username = username;
            row.RememberUsername = rememberUsername;
            row.AuthenticationToken = authenticationToken;

            ret.SelectAlbumTable.Rows.Add(row);

            return ret;           
        }

    }
}
