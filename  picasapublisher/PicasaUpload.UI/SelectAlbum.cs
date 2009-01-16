﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicasaUpload.GoogleApi;
using System.Data;
using System.Reflection;
using System.Net;
using System.IO;

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
        public static SelectAlbumDataSet SelectAlbumUI(bool rememberUserEmail, string userEmail, DateTime lastUpdateCheck, bool updateAtLastCheck)
        {
            Picasa picasa = new Picasa(APP_NAME);

			LoginForm login = new LoginForm();
            login.PicasaPublisherApi = picasa;
			login.RememberUsername = rememberUserEmail;
			login.Username = userEmail;
			login.LastCheckForUpdate = lastUpdateCheck;
			login.UpdateAtLastCheck = updateAtLastCheck;
            if (login.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return null;
            }

			string username = login.Username;
			bool rememberUsername = login.RememberUsername;
			string authenticationToken = "";
			lastUpdateCheck = login.LastCheckForUpdate;
			updateAtLastCheck = login.UpdateAtLastCheck;

                        
            //Display UI for selecting album:
            string selectedAlbumId = string.Empty;
			SelectAlbumForm selectAlbum = new SelectAlbumForm();
			selectAlbum.PicasaPublisherApi = picasa;
			if (selectAlbum.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				return null;
			}
            
            //Create new album if necessary
			selectedAlbumId = selectAlbum.SelectedAlbum;
			if (selectedAlbumId == string.Empty)
			{
			}


            //Gather up what needs to be returned to the user:
            return BuildSelectAlbumUIDatatable(rememberUsername, username, authenticationToken, selectedAlbumId, lastUpdateCheck, updateAtLastCheck);
        }


		public static bool IsLatestVersion(ref DateTime lastCheckForUpdate, bool updateAtLastCheck)
		{
			if( (DateTime.Now - lastCheckForUpdate).TotalDays < 7.0 ) 
			{
				return updateAtLastCheck;
			}

			//get current version
			string currentVersionString = Assembly.GetAssembly(typeof(LoginWindow)).GetName().Version.ToString();

			//Latest version string from website:
			string latestVersionString = string.Empty;
			try
			{
				HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Properties.Resources.BASE_URL + Properties.Resources.CURRENT_VERSION_URL);
				webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.Reload);
				HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
				StreamReader sr = new StreamReader(response.GetResponseStream());
				latestVersionString = sr.ReadToEnd();
			}
			catch (Exception)
			{
				return true;
			}

			lastCheckForUpdate = DateTime.Now;

			if (currentVersionString != latestVersionString)
			{
				return false;
			}

			return true;
		}

        private static SelectAlbumDataSet BuildSelectAlbumUIDatatable(bool rememberUsername, string username, string authenticationToken, string selectedAlbumId, DateTime lastUpdateCheck, bool updateAtLastCheck)
        {
            //this should be a strongly typed dataset:
            SelectAlbumDataSet ret = new SelectAlbumDataSet();
            SelectAlbumDataSet.SelectAlbumTableRow row = ret.SelectAlbumTable.NewSelectAlbumTableRow();
            row.SelectedAlbumId = selectedAlbumId;
            row.Username = username;
            row.RememberUsername = rememberUsername;
            row.AuthenticationToken = authenticationToken;
			row.LastCheckForUpdate = lastUpdateCheck;
			row.LastUpdateValue = updateAtLastCheck;

            ret.SelectAlbumTable.Rows.Add(row);

            return ret;           
        }

    }
}
