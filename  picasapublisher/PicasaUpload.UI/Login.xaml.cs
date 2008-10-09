﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using PicasaUpload.GoogleApi;

namespace PicasaUpload.UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : UserControl
    {
		private Picasa _picasaApi;
		private bool _loginClicked = false;
		public bool LoginClicked { get { return _loginClicked; } }
        public string Username { get { return _txtUsername.Text; } set { _txtUsername.Text = value; } }
        public string Password { get { return _txtPassword.Password; } set { _txtPassword.Password = value; } }
        public bool RememberUsername { get { return _cboRememberUsername.IsChecked.Value; } set { _cboRememberUsername.IsChecked = value; } }
		private string _authenticationToken;
		public string AuthenticationToken { get { return _authenticationToken; } set { _authenticationToken = value; } }

        public LoginWindow(Picasa picasaApi)
        {
            InitializeComponent();
			_picasaApi = picasaApi;
			
        }

		private void _cmdLogin_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.Cursor = Cursors.Wait;
				_authenticationToken = _picasaApi.Login(Username, Password);
				this.Cursor = Cursors.Arrow;
			}
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			_loginClicked = true;
		}

		private void _cmdFeedback_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(Properties.Resources.BASE_URL + Properties.Resources.FEEDBACK_URL);
		}

		private void _cmdDonate_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(Properties.Resources.BASE_URL + Properties.Resources.DONATE_URL);
		}
    }
}
