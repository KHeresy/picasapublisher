using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;
using System.IO;
using GoogleApi;


namespace PicasaUpload
{
	/// <summary>
	/// This form is the main login form for picasa publisher.  It also allows the user to select the album to upload to.
	/// </summary>
	public partial class LoginForm : Form
	{
		private const string APPLICATION_NAME = "MLS-PicassaLivePublisher-2";


		/// <summary>
		/// Constructor
		/// </summary>
		public LoginForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The GoogleAuthorizationToken that is used to communicate with google:
		/// </summary>
		private GoogleAuthorizationToken _googleAuthKey = null;
		public GoogleAuthorizationToken GoogleAuthKey { get { return _googleAuthKey; } set { _googleAuthKey = value; } }

		/// <summary>
		/// The ID of the album that the user has selected.
		/// </summary>
		private string _selectedAlbumId = string.Empty;
		public string SelectedAlbumId { get { return _selectedAlbumId; } }

		/// <summary>
		/// Allows the user of the form to specify a remembered email address:
		/// </summary>
		public string UserEmail { get { return _txtEmail.Text; } set { _txtEmail.Text = value; } }

		/// <summary>
		/// Allows the parent of the form to know if the user wants to remember their email between sessions:
		/// </summary>
		public bool RememberUserEmail { get { return _cboRememberEmail.Checked; } set { _cboRememberEmail.Checked = value; } }



		/// <summary>
		/// Login is clicked:
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _cmdLogin_Click(object sender, EventArgs e)
		{
			Login();
		}

		/// <summary>
		/// User press the OK button:
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _cmdOk_Click(object sender, EventArgs e)
		{
			//If the user has not logged in yet, then log them in, so they can select an album.
			if (_googleAuthKey == null)
			{
				Login();
				DialogResult = DialogResult.None;
				return;
			}

			if (_cboAlbumSelect.SelectedValue == null)
			{
				//If the user has not selected an album, and they haven't entered a useful name.
				if (string.IsNullOrEmpty(_cboAlbumSelect.Text))
				{
					MessageBox.Show("Select an album, or enter the name of a new album in the Album combobox", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					DialogResult = DialogResult.None;
					return;
				}
				else
				{
					_selectedAlbumId = GoogleApi.PicasaWebAlbums.PicasaWebAlbumsRequest.CreateAlbum(_cboAlbumSelect.Text, _googleAuthKey);
				}
			}
			else
			{
				_selectedAlbumId = _cboAlbumSelect.SelectedValue.ToString();
			}
		}

		/// <summary>
		/// This function will log the user into Google.
		/// </summary>
		private void Login()
		{
			string email = _txtEmail.Text;
			string password = _txtPassword.Text;

			//Validate user input:
			if (string.IsNullOrEmpty(email))
			{
				MessageBox.Show("Please enter your Picasa Web Albums email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Please enter your Picasa Web Albums password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//Set the cursor to wait so user knows something is happenning:
			this.Cursor = Cursors.WaitCursor;



			/*
			 * http://code.google.com/apis/picasaweb/developers_guide_dotnet.html#ClientLogin
			 * 
			 * 
			 */


			#region Issue 10 - Move to the Google Data API already built.
			/*
			 * There seems to be problems with databinding because you have to bind to a subproperty, which
			 * doesn't seem to be supported by combo box:
			 * 
			 */
			//PicasaService service = new PicasaService(APPLICATION_NAME);
			//service.setUserCredentials(email, password);


			//AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri("default"));

			//PicasaFeed feed = service.Query(query);
			//entriesBindingSource.DataSource = feed.Entries;







			//int i = 0;

			#endregion


			//Actually try logging in:
			GoogleApi.Authentication.AuthenticationResult result = GoogleApi.Authentication.Authenticate(GoogleApi.Authentication.AccountTypes.GOOGLE, email, password, GoogleApi.Authentication.Services.PICASA_WEB_ALBUMS, "MLS-PicassaLivePublisher-1");

			//Check for error, and display the error:
			if (result.IsSuccessful)
			{
				_googleAuthKey = result.AuthKey;

				GoogleApi.PicasaWebAlbums.AlbumList albumList = GoogleApi.PicasaWebAlbums.PicasaWebAlbumsRequest.GetAlbumList(_googleAuthKey);
				albumList.Album.DefaultView.Sort = "Name";
				this.albumDataTableBindingSource.DataSource = albumList.Album;
				_cboAlbumSelect.Enabled = true;

			}
			else
			{
				MessageBox.Show(result.GetErrorMessage());
			}

			//Reset our cursor:
			this.Cursor = Cursors.Default;
		}

	}
}
