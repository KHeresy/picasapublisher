namespace PicasaUpload
{
	partial class AlbumUserControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._pnlMain = new System.Windows.Forms.TableLayoutPanel();
			this._picAlbumCover = new System.Windows.Forms.PictureBox();
			this._txtAlbumName = new System.Windows.Forms.TextBox();
			this._txtAlbumDescription = new System.Windows.Forms.TextBox();
			this._cboAlbumPermission = new System.Windows.Forms.ComboBox();
			this._ttInformation = new System.Windows.Forms.ToolTip(this.components);
			this._pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._picAlbumCover)).BeginInit();
			this.SuspendLayout();
			// 
			// _pnlMain
			// 
			this._pnlMain.ColumnCount = 2;
			this._pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this._pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this._pnlMain.Controls.Add(this._picAlbumCover, 0, 0);
			this._pnlMain.Controls.Add(this._txtAlbumName, 1, 0);
			this._pnlMain.Controls.Add(this._txtAlbumDescription, 1, 1);
			this._pnlMain.Controls.Add(this._cboAlbumPermission, 1, 2);
			this._pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pnlMain.Location = new System.Drawing.Point(0, 0);
			this._pnlMain.Name = "_pnlMain";
			this._pnlMain.RowCount = 3;
			this._pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this._pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._pnlMain.Size = new System.Drawing.Size(300, 150);
			this._pnlMain.TabIndex = 0;
			// 
			// _picAlbumCover
			// 
			this._picAlbumCover.Dock = System.Windows.Forms.DockStyle.Fill;
			this._picAlbumCover.Location = new System.Drawing.Point(3, 3);
			this._picAlbumCover.Name = "_picAlbumCover";
			this._pnlMain.SetRowSpan(this._picAlbumCover, 3);
			this._picAlbumCover.Size = new System.Drawing.Size(144, 144);
			this._picAlbumCover.TabIndex = 0;
			this._picAlbumCover.TabStop = false;
			// 
			// _txtAlbumName
			// 
			this._txtAlbumName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._txtAlbumName.Location = new System.Drawing.Point(153, 3);
			this._txtAlbumName.Name = "_txtAlbumName";
			this._txtAlbumName.Size = new System.Drawing.Size(144, 20);
			this._txtAlbumName.TabIndex = 1;
			this._ttInformation.SetToolTip(this._txtAlbumName, "The Album Name");
			// 
			// _txtAlbumDescription
			// 
			this._txtAlbumDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this._txtAlbumDescription.Location = new System.Drawing.Point(153, 29);
			this._txtAlbumDescription.Multiline = true;
			this._txtAlbumDescription.Name = "_txtAlbumDescription";
			this._txtAlbumDescription.Size = new System.Drawing.Size(144, 91);
			this._txtAlbumDescription.TabIndex = 2;
			this._ttInformation.SetToolTip(this._txtAlbumDescription, "The description of the Album");
			// 
			// _cboAlbumPermission
			// 
			this._cboAlbumPermission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._cboAlbumPermission.FormattingEnabled = true;
			this._cboAlbumPermission.Items.AddRange(new object[] {
            "Public",
            "Unlisted"});
			this._cboAlbumPermission.Location = new System.Drawing.Point(153, 126);
			this._cboAlbumPermission.Name = "_cboAlbumPermission";
			this._cboAlbumPermission.Size = new System.Drawing.Size(144, 21);
			this._cboAlbumPermission.TabIndex = 3;
			this._ttInformation.SetToolTip(this._cboAlbumPermission, "Permissions for this album:\r\nPublic - For albums you want to show publicly.\r\nUnli" +
					"sted - For albums that you only want to share with select people.");
			// 
			// _ttInformation
			// 
			this._ttInformation.IsBalloon = true;
			this._ttInformation.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// AlbumUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._pnlMain);
			this.MaximumSize = new System.Drawing.Size(300, 150);
			this.MinimumSize = new System.Drawing.Size(300, 150);
			this.Name = "AlbumUserControl";
			this.Size = new System.Drawing.Size(300, 150);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AlbumUserControl_MouseMove);
			this.Leave += new System.EventHandler(this.AlbumUserControl_Leave);
			this.Enter += new System.EventHandler(this.AlbumUserControl_Enter);
			this.MouseEnter += new System.EventHandler(this.AlbumUserControl_MouseEnter);
			this._pnlMain.ResumeLayout(false);
			this._pnlMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._picAlbumCover)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel _pnlMain;
		private System.Windows.Forms.PictureBox _picAlbumCover;
		private System.Windows.Forms.TextBox _txtAlbumName;
		private System.Windows.Forms.TextBox _txtAlbumDescription;
		private System.Windows.Forms.ComboBox _cboAlbumPermission;
		private System.Windows.Forms.ToolTip _ttInformation;
	}
}
