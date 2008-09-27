namespace PicasaUpload
{
	partial class PicasaPubliserForm
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
            this._cmdLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._txtEmail = new System.Windows.Forms.TextBox();
            this._txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._cmdOk = new System.Windows.Forms.Button();
            this._cmdCancel = new System.Windows.Forms.Button();
            this._cboAlbumSelect = new System.Windows.Forms.ComboBox();
            this.albumDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._ttAlbum = new System.Windows.Forms.ToolTip(this.components);
            this._cboRememberEmail = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.albumDataTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // _cmdLogin
            // 
            this._cmdLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdLogin.Location = new System.Drawing.Point(208, 58);
            this._cmdLogin.Name = "_cmdLogin";
            this._cmdLogin.Size = new System.Drawing.Size(75, 23);
            this._cmdLogin.TabIndex = 4;
            this._cmdLogin.Text = "Login";
            this._cmdLogin.UseVisualStyleBackColor = true;
            this._cmdLogin.Click += new System.EventHandler(this._cmdLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // _txtEmail
            // 
            this._txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._txtEmail.Location = new System.Drawing.Point(74, 6);
            this._txtEmail.Name = "_txtEmail";
            this._txtEmail.Size = new System.Drawing.Size(209, 20);
            this._txtEmail.TabIndex = 1;
            // 
            // _txtPassword
            // 
            this._txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._txtPassword.Location = new System.Drawing.Point(74, 32);
            this._txtPassword.Name = "_txtPassword";
            this._txtPassword.PasswordChar = '*';
            this._txtPassword.Size = new System.Drawing.Size(209, 20);
            this._txtPassword.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Album:";
            // 
            // _cmdOk
            // 
            this._cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cmdOk.Location = new System.Drawing.Point(127, 116);
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
            this._cmdCancel.Location = new System.Drawing.Point(208, 116);
            this._cmdCancel.Name = "_cmdCancel";
            this._cmdCancel.Size = new System.Drawing.Size(75, 23);
            this._cmdCancel.TabIndex = 9;
            this._cmdCancel.Text = "Cancel";
            this._cmdCancel.UseVisualStyleBackColor = true;
            // 
            // _cboAlbumSelect
            // 
            this._cboAlbumSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._cboAlbumSelect.DataSource = this.albumDataTableBindingSource;
            this._cboAlbumSelect.DisplayMember = "Name";
            this._cboAlbumSelect.Enabled = false;
            this._cboAlbumSelect.FormattingEnabled = true;
            this._cboAlbumSelect.Location = new System.Drawing.Point(74, 87);
            this._cboAlbumSelect.Name = "_cboAlbumSelect";
            this._cboAlbumSelect.Size = new System.Drawing.Size(209, 21);
            this._cboAlbumSelect.TabIndex = 5;
            this._ttAlbum.SetToolTip(this._cboAlbumSelect, "After logging in, select the album you wish to \r\nPublish to, or type the name of " +
                    "a new album.");
            this._cboAlbumSelect.ValueMember = "Id";
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
            // _cboRememberEmail
            // 
            this._cboRememberEmail.AutoSize = true;
            this._cboRememberEmail.Location = new System.Drawing.Point(74, 58);
            this._cboRememberEmail.Name = "_cboRememberEmail";
            this._cboRememberEmail.Size = new System.Drawing.Size(105, 17);
            this._cboRememberEmail.TabIndex = 3;
            this._cboRememberEmail.Text = "Remember Email";
            this._cboRememberEmail.UseVisualStyleBackColor = true;
            // 
            // PicasaPubliserForm
            // 
            this.AcceptButton = this._cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cmdCancel;
            this.ClientSize = new System.Drawing.Size(295, 151);
            this.Controls.Add(this._cboRememberEmail);
            this.Controls.Add(this._cboAlbumSelect);
            this.Controls.Add(this._cmdCancel);
            this.Controls.Add(this._cmdOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._txtPassword);
            this.Controls.Add(this._txtEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cmdLogin);
            this.MinimumSize = new System.Drawing.Size(303, 185);
            this.Name = "PicasaPubliserForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Picasa Web Album Publisher";
            ((System.ComponentModel.ISupportInitialize)(this.albumDataTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _cmdLogin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox _txtEmail;
		private System.Windows.Forms.TextBox _txtPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button _cmdOk;
		private System.Windows.Forms.Button _cmdCancel;
		private System.Windows.Forms.ComboBox _cboAlbumSelect;
		private System.Windows.Forms.BindingSource albumDataTableBindingSource;
		private System.Windows.Forms.ToolTip _ttAlbum;
		private System.Windows.Forms.CheckBox _cboRememberEmail;

	}
}