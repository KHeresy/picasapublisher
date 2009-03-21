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
	public partial class PicasaPubliserForm : Form
	{
		public PicasaPubliserForm()
		{
			InitializeComponent();
		}

		private GoogleAuthorizationToken _googleAuthKey = null;
		public GoogleAuthorizationToken GoogleAuthKey { get { return _googleAuthKey; } set { _googleAuthKey = value; } }

		private string _selectedAlbumId = string.Empty;
		public string SelectedAlbumId { get { return _selectedAlbumId; } }


		/// <summary>
		/// Login is clicked:
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _cmdTestLogin_Click(object sender, EventArgs e)
		{
			Login();

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void _cmdOk_Click(object sender, EventArgs e)
		{
			if (_googleAuthKey == null)
			{
				Login();
				DialogResult = DialogResult.None;
				return;
			}

			if (_cboAlbumSelect.SelectedValue == null)
			{
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

		private void Login()
		{
			string email = _txtEmail.Text;
			string password = _txtPassword.Text;

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

			this.Cursor = Cursors.WaitCursor;
			GoogleApi.Authentication.AuthenticationResult result = GoogleApi.Authentication.Authenticate(GoogleApi.Authentication.AccountTypes.GOOGLE, email, password, GoogleApi.Authentication.Services.PICASA_WEB_ALBUMS, "MLS-PicassaLivePublisher-1");

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

			this.Cursor = Cursors.Default;
		}
	}
}
