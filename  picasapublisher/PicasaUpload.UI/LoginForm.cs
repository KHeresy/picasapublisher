using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicasaUpload.UI
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
		}

        private LoginWindow _loginWpf = null;

        private PicasaUpload.GoogleApi.Picasa _picasaPublisherApi;
        public PicasaUpload.GoogleApi.Picasa PicasaPublisherApi
        {
            get { return _picasaPublisherApi; }
            set
            {
                _picasaPublisherApi = value;
            }
        }

		private void LoginForm_Load(object sender, EventArgs e)
		{
            _loginWpf = new LoginWindow(_picasaPublisherApi);
            _loginWpf.CloseLoginEvent += new LoginWindow.CloseLogin(_loginWpf_CloseLoginEvent);
            _wpfHost.Child = _loginWpf;
            DialogResult = DialogResult.None;
            
		}

        void _loginWpf_CloseLoginEvent(object sender, EventArgs e)
        {
            if (_loginWpf.LoginClicked == true)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }
	}
}
