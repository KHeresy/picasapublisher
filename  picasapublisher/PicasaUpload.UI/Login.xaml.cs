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

namespace PicasaUpload.UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Username { get { return _txtUsername.Text; } set { _txtUsername.Text = value; } }
        public string Password { get { return _txtPassword.Password; } set { _txtPassword.Password = value; } }
        public bool RememberUsername { get { return _cboRememberUsername.IsChecked.Value; } set { _cboRememberUsername.IsChecked = value; } }
        public LoginWindow()
        {
            InitializeComponent();
        }
    }
}
