using System;
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
    public partial class LoginWindow : Window
    {
		private Picasa _picasaApi;
		private bool _loginClicked = false;
		public bool LoginClicked { get { return _loginClicked; } }
        public string Username { get { return _txtUsername.Text; } set { _txtUsername.Text = value; } }
        public string Password { get { return _txtPassword.Password; } set { _txtPassword.Password = value; } }
        public bool RememberUsername { get { return _cboRememberUsername.IsChecked.Value; } set { _cboRememberUsername.IsChecked = value; } }
		private DateTime _lastCheckForUpdate = DateTime.MinValue;
		public DateTime LastCheckForUpdate { get { return _lastCheckForUpdate; } set { _lastCheckForUpdate = value; } }
		private bool _updateAtLastCheck = true;
		public bool UpdateAtLastCheck { get { return _updateAtLastCheck; } set { _updateAtLastCheck = value; } }
        public bool QuickUpload { get; set; }

        public LoginWindow(Picasa picasaApi)
        {
            InitializeComponent();
			_picasaApi = picasaApi;


			_updateAtLastCheck = SelectAlbum.IsLatestVersion(ref _lastCheckForUpdate, _updateAtLastCheck);
			if (_updateAtLastCheck)
			{
				_cmdUpgrade.Content = Properties.Resources.LATEST_VERSION_TEXT;
				_cmdUpgrade.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.LATEST_VERSION_COLOR));
			}
			else
			{
				_cmdUpgrade.Content = Properties.Resources.NEED_TO_UPGRADE_TEXT;
				_cmdUpgrade.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Properties.Resources.NEED_TO_UPGRADE_COLOR));
			}
			
        }

		private void _cmdLogin_Click(object sender, RoutedEventArgs e)
		{
            Login();
            QuickUpload = false;

        }

        private void Login()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                _picasaApi.Login(Username, Password);
                this.Cursor = Cursors.Arrow;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
        }

        private void _cmdCancel_Click(object sender, RoutedEventArgs e)
        {
        }

		private void _cmdFeedback_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(Properties.Resources.BASE_URL + Properties.Resources.FEEDBACK_URL);
		}

		private void _cmdDonate_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(Properties.Resources.BASE_URL + Properties.Resources.DONATE_URL);
        }
		private void _cmdUpgrade_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(Properties.Resources.BASE_URL + Properties.Resources.UPGRADE_URL);

		}


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(_txtUsername.Text))
            {
                _txtUsername.Focus();
            }
            else
            {
                _txtPassword.Focus();
            }

        }

        /// <summary>
        /// When the user clicks this button, we want to log them in, and automatically post to the dropbox!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _cmdQuickUpload_Click(object sender, RoutedEventArgs e)
        {
            Login();
            QuickUpload = true;



        }



    }
}
