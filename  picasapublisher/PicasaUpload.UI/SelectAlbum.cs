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
			LoginForm login = new LoginForm();
            login.PicasaPublisherApi = picasa;
            if (login.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return null;
            }
//            LoginWindow login = new LoginWindow(picasa);

			////Login
			//if (login.ShowDialog() == true)
			//{
			//    if (!login.LoginClicked)
			//    {
			//        return null;
			//    }
			//}

			string username = "";
			bool rememberUsername = false;
			string authenticationToken = "";
            
            
            //Display UI for selecting album:
            string selectedAlbumId = string.Empty;
			SelectAlbumForm selectAlbum = new SelectAlbumForm();
			selectAlbum.PicasaPublisherApi = picasa;
			if (selectAlbum.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return null;
			}


            
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
