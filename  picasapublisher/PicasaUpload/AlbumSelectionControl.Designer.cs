namespace PicasaUpload
{
	partial class AlbumSelectionControl
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
			this._pnlAlbumLayout = new System.Windows.Forms.TableLayoutPanel();
			this._vsbVerticalScrollBar = new System.Windows.Forms.VScrollBar();
			this._pnlMainContainer = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// _pnlAlbumLayout
			// 
			this._pnlAlbumLayout.BackColor = System.Drawing.SystemColors.Window;
			this._pnlAlbumLayout.ColumnCount = 1;
			this._pnlAlbumLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this._pnlAlbumLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this._pnlAlbumLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this._pnlAlbumLayout.Location = new System.Drawing.Point(0, 0);
			this._pnlAlbumLayout.Name = "_pnlAlbumLayout";
			this._pnlAlbumLayout.RowCount = 1;
			this._pnlAlbumLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this._pnlAlbumLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this._pnlAlbumLayout.Size = new System.Drawing.Size(200, 100);
			this._pnlAlbumLayout.TabIndex = 0;
			// 
			// _vsbVerticalScrollBar
			// 
			this._vsbVerticalScrollBar.Location = new System.Drawing.Point(0, 0);
			this._vsbVerticalScrollBar.Name = "_vsbVerticalScrollBar";
			this._vsbVerticalScrollBar.Size = new System.Drawing.Size(17, 80);
			this._vsbVerticalScrollBar.TabIndex = 0;
			// 
			// _pnlMainContainer
			// 
			this._pnlMainContainer.BackColor = System.Drawing.SystemColors.Window;
			this._pnlMainContainer.Location = new System.Drawing.Point(0, 0);
			this._pnlMainContainer.Name = "_pnlMainContainer";
			this._pnlMainContainer.Size = new System.Drawing.Size(200, 100);
			this._pnlMainContainer.TabIndex = 0;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel _pnlAlbumLayout;
		private System.Windows.Forms.VScrollBar _vsbVerticalScrollBar;
		private System.Windows.Forms.Panel _pnlMainContainer;
	}
}
