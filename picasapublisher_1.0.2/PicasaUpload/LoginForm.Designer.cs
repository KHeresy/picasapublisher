namespace PicasaUpload
{
	partial class LoginForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this._cmdOk = new System.Windows.Forms.Button();
            this._cmdCancel = new System.Windows.Forms.Button();
            this.albumDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._ttAlbum = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._cboRememberEmail = new System.Windows.Forms.CheckBox();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this._txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._cmdLogin = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._cboAlbumSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._cboPictureSize = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.albumDataTableBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cmdOk
            // 
            this._cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdOk.Location = new System.Drawing.Point(207, 255);
            this._cmdOk.Name = "_cmdOk";
            this._cmdOk.Size = new System.Drawing.Size(75, 23);
            this._cmdOk.TabIndex = 8;
            this._cmdOk.Text = "OK";
            this._cmdOk.UseVisualStyleBackColor = true;
            this._cmdOk.Click += new System.EventHandler(this._cmdOk_Click);
            // 
            // _cmdCancel
            // 
            this._cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cmdCancel.Location = new System.Drawing.Point(288, 255);
            this._cmdCancel.Name = "_cmdCancel";
            this._cmdCancel.Size = new System.Drawing.Size(75, 23);
            this._cmdCancel.TabIndex = 9;
            this._cmdCancel.Text = "Cancel";
            this._cmdCancel.UseVisualStyleBackColor = true;
            // 
            // albumDataTableBindingSource
            // 
            this.albumDataTableBindingSource.DataSource = typeof(GoogleApi.PicasaWebAlbums.AlbumList.AlbumDataTable);
            // 
            // _ttAlbum
            // 
            this._ttAlbum.IsBalloon = true;
            this._ttAlbum.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this._ttAlbum.ToolTipTitle = "Album Tip";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._cboRememberEmail);
            this.groupBox1.Controls.Add(this._txtPassword);
            this.groupBox1.Controls.Add(this._txtEmail);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._cmdLogin);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 103);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Google Account Information";
            // 
            // _cboRememberEmail
            // 
            this._cboRememberEmail.AutoSize = true;
            this._cboRememberEmail.Location = new System.Drawing.Point(68, 78);
            this._cboRememberEmail.Name = "_cboRememberEmail";
            this._cboRememberEmail.Size = new System.Drawing.Size(105, 17);
            this._cboRememberEmail.TabIndex = 9;
            this._cboRememberEmail.Text = "Remember Email";
            this._cboRememberEmail.UseVisualStyleBackColor = true;
            // 
            // _txtPassword
            // 
            this._txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._txtPassword.Location = new System.Drawing.Point(68, 48);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.PasswordChar = '*';
            this._txtPassword.Size = new System.Drawing.Size(277, 20);
            this._txtPassword.TabIndex = 7;
            // 
            // _txtEmail
            // 
            this._txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._txtEmail.Location = new System.Drawing.Point(68, 22);
            this._txtEmail.Name = "_txtEmail";
            this._txtEmail.Size = new System.Drawing.Size(277, 20);
            this._txtEmail.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Email:";
            // 
            // _cmdLogin
            // 
            this._cmdLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdLogin.Location = new System.Drawing.Point(270, 74);
            this._cmdLogin.Name = "_cmdLogin";
            this._cmdLogin.Size = new System.Drawing.Size(75, 23);
            this._cmdLogin.TabIndex = 10;
            this._cmdLogin.Text = "Login";
            this._cmdLogin.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._cboPictureSize);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this._cboAlbumSelect);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 82);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Publish Settings";
            // 
            // _cboAlbumSelect
            // 
            this._cboAlbumSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._cboAlbumSelect.DataSource = this.albumDataTableBindingSource;
            this._cboAlbumSelect.DisplayMember = "Name";
            this._cboAlbumSelect.Enabled = false;
            this._cboAlbumSelect.FormattingEnabled = true;
            this._cboAlbumSelect.Location = new System.Drawing.Point(78, 19);
            this._cboAlbumSelect.Name = "_cboAlbumSelect";
            this._cboAlbumSelect.Size = new System.Drawing.Size(267, 21);
            this._cboAlbumSelect.TabIndex = 8;
            this._ttAlbum.SetToolTip(this._cboAlbumSelect, "After logging in, select the album you wish to \r\nPublish to, or type the name of " +
                    "a new album.");
            this._cboAlbumSelect.ValueMember = "Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Album:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Picture Size:";
            // 
            // _cboPictureSize
            // 
            this._cboPictureSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._cboPictureSize.FormattingEnabled = true;
            this._cboPictureSize.Location = new System.Drawing.Point(78, 46);
            this._cboPictureSize.Name = "_cboPictureSize";
            this._cboPictureSize.Size = new System.Drawing.Size(267, 21);
            this._cboPictureSize.TabIndex = 11;
            // 
            // LoginForm
            // 
            this.AcceptButton = this._cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdCancel;
            this.ClientSize = new System.Drawing.Size(375, 290);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._cmdCancel);
            this.Controls.Add(this._cmdOk);
            this.MinimumSize = new System.Drawing.Size(303, 185);
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Picasa Web Album Publisher";
            ((System.ComponentModel.ISupportInitialize)(this.albumDataTableBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button _cmdOk;
        private System.Windows.Forms.Button _cmdCancel;
		private System.Windows.Forms.BindingSource albumDataTableBindingSource;
        private System.Windows.Forms.ToolTip _ttAlbum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox _cboRememberEmail;
        private System.Windows.Forms.TextBox _txtPassword;
        private System.Windows.Forms.TextBox _txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _cmdLogin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox _cboPictureSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _cboAlbumSelect;
        private System.Windows.Forms.Label label3;

	}
}