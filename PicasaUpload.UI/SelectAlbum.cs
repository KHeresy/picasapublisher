﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicasaUpload.GoogleApi;
using System.Data;
using System.Reflection;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Google.GData.Photos;

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
		public static SelectAlbumDataSet SelectAlbumUI(bool rememberUserEmail, string userEmail, DateTime lastUpdateCheck, bool updateAtLastCheck, int photoSize)
		{
			Picasa picasa = new Picasa(APP_NAME);

			Cursor.Current = Cursors.WaitCursor;

			//LoginForm login = new LoginForm();
			LoginWindow login = new LoginWindow(picasa);
			//login.PicasaPublisherApi = picasa;
			login.RememberUsername = rememberUserEmail;
			login.Username = userEmail;
			login.LastCheckForUpdate = lastUpdateCheck;
			login.UpdateAtLastCheck = updateAtLastCheck;
			if (login.ShowDialog() == false)
			{
				return null;
			}

			string username = login.Username;
			bool rememberUsername = login.RememberUsername;
			lastUpdateCheck = login.LastCheckForUpdate;
			updateAtLastCheck = login.UpdateAtLastCheck;

			bool quickUpload = login.QuickUpload;
						
			//Display UI for selecting album:
			PicasaEntry selectedAlbumEntry = null;
			SelectAlbumUserControl selectAlbum = new SelectAlbumUserControl(picasa, photoSize);

			if (!quickUpload)
			{
				if (selectAlbum.ShowDialog() == false)
				{
					return null;
				}

				//Create new album if necessary
				selectedAlbumEntry = selectAlbum.SelectedAlbumEntry;
				if (selectedAlbumEntry == null)
				{
					//we need to create a new album
					PicasaEntry newAlbum = picasa.CreateAlbum(selectAlbum.AlbumName, selectAlbum.AlbumSummary, selectAlbum.AlbumRights);
					selectedAlbumEntry = newAlbum;
				}
			}

			//Gather up what needs to be returned to the user:
			return BuildSelectAlbumUIDatatable(rememberUsername, username, picasa.AuthenticationToken, selectedAlbumEntry, lastUpdateCheck, updateAtLastCheck, selectAlbum.PhotoSize, quickUpload);
		}


		public static bool IsLatestVersion(ref DateTime lastCheckForUpdate, bool updateAtLastCheck)
		{
			//Need to do this everytime, because when the user first upgrades, we don't want to wait for 7 days to 
			//tell them that they have an old version.

			//get current version
			string currentVersionString = Assembly.GetAssembly(typeof(LoginWindow)).GetName().Version.ToString();

			//Latest version string from website:
			string latestVersionString = string.Empty;
			try
			{
				HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Properties.Resources.BASE_URL + 
																		Properties.Resources.CURRENT_VERSION_URL +
																		currentVersionString);
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

		private static SelectAlbumDataSet BuildSelectAlbumUIDatatable(bool rememberUsername, string username, string authenticationToken, PicasaEntry selectedAlbumEntry, DateTime lastUpdateCheck, bool updateAtLastCheck, int photoSize, bool quickUpload)
		{
			//this should be a strongly typed dataset:
			SelectAlbumDataSet ret = new SelectAlbumDataSet();
			SelectAlbumDataSet.SelectAlbumTableRow row = ret.SelectAlbumTable.NewSelectAlbumTableRow();
			row.Username = username;
			row.RememberUsername = rememberUsername;
			row.AuthenticationToken = authenticationToken;
			row.LastCheckForUpdate = lastUpdateCheck;
			row.LastUpdateValue = updateAtLastCheck;
			row.SelectedAlbumEntry = selectedAlbumEntry;
			row.PhotoSize = photoSize;
			row.UseDefaultAlbum = quickUpload;

			ret.SelectAlbumTable.Rows.Add(row);

			return ret;           
		}

	}
}
