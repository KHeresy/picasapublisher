using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PicasaUpload
{
	public partial class AlbumSelectionControl : Control
	{
		public AlbumSelectionControl()
		{
			InitializeComponent();
		}

		//private PicasaFeed _albums;
		//public PicasaFeed Albums 
		//{ 
		//    get { return _albums; } 
		//    set { _albums = value; } 
		//}

		/// <summary>
		/// This function is where we layout all of the controls that are used to display the list of albums:
		/// </summary>
		protected override void OnCreateControl()
		{
			//call the base for this function. 
			base.OnCreateControl();

			//Get the default sizes for the albumUserControl
			AlbumUserControl aucDefaultSize = new AlbumUserControl();
			int width = aucDefaultSize.Width;
			int height = aucDefaultSize.Height;

			//Get the number of rows and columns based on the data being displayed:
			_pnlAlbumLayout.RowCount = 5;
			_pnlAlbumLayout.ColumnCount = 2;	//the third column is for the vertical scroll bar:

			//setup our row and column styles.
			_pnlAlbumLayout.RowStyles.Clear();
			_pnlAlbumLayout.ColumnStyles.Clear();

			for (int r = 0; r < _pnlAlbumLayout.RowCount; r++)
			{
				_pnlAlbumLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, height + 2));
			}
			for (int c = 0; c < _pnlAlbumLayout.ColumnCount; c++)
			{
				_pnlAlbumLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, width + 2));
			}



			//set the total height and width of the container of albums.
			_pnlAlbumLayout.Width = (_pnlAlbumLayout.ColumnCount) * width + 5;
			_pnlAlbumLayout.Height = _pnlAlbumLayout.RowCount * height + 10;

	
			//add event handlers:
			_pnlAlbumLayout.MouseMove += new MouseEventHandler(_pnlAlbumLayout_MouseMove);
			_pnlAlbumLayout.MouseEnter += new EventHandler(_pnlAlbumLayout_MouseEnter);
			_pnlAlbumLayout.MouseLeave += new EventHandler(_pnlAlbumLayout_MouseLeave);

			//Add all of our albums:
			for( int r=0; r<_pnlAlbumLayout.RowCount; r++ )
			{
				for (int c = 0; c < _pnlAlbumLayout.ColumnCount; c++)
				{
					AlbumUserControl auc = new AlbumUserControl();
					auc.Dock = DockStyle.Fill;
					_pnlAlbumLayout.Controls.Add(auc, c, r);
				}
			}

			//_pnlAlbumLayout.Dock = DockStyle.Left;
			_pnlMainContainer.Controls.Add(_pnlAlbumLayout);

			//We need to setup the vertical scroll bar, and add it to our list of controls:
			_vsbVerticalScrollBar.Dock = DockStyle.Right;
			InitializeVerticalScrollBar();
			_vsbVerticalScrollBar.Value = _vsbVerticalScrollBar.Minimum;
			_vsbVerticalScrollBar.Scroll += new ScrollEventHandler(_vsbVerticalScrollBar_Scroll);
			_pnlMainContainer.Controls.Add(_vsbVerticalScrollBar);

			_pnlMainContainer.Width = _pnlAlbumLayout.Width + _vsbVerticalScrollBar.Width;
			_pnlMainContainer.Dock = DockStyle.Left;

			//Add our album container to our list of controls.
			this.Controls.Add(_pnlMainContainer);

		}



		/// <summary>
		/// This function is used to change the size of the vertical scroll bar.  This should be called when a resize is detected.
		/// </summary>
		private void InitializeVerticalScrollBar()
		{
			int amountHidden = _pnlAlbumLayout.Height - _pnlMainContainer.Height;

			if (amountHidden <= 0)
			{
				//Reset the main panel to be visible:
				_pnlAlbumLayout.Top = 0;
				_vsbVerticalScrollBar.Visible = false;
			}
			else
			{
				_vsbVerticalScrollBar.Visible = true;
			}

			//Setup the vertical scrollbar:
			_vsbVerticalScrollBar.Minimum = 0;
			_vsbVerticalScrollBar.Maximum = amountHidden;

			//calculate some reasonable large and small steps:
			_vsbVerticalScrollBar.LargeChange = amountHidden /4 ;
			_vsbVerticalScrollBar.SmallChange = amountHidden / 10;

		}

		/// <summary>
		/// This is fired when the vertical scroll bar is moved:
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _vsbVerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			//adjust our UI for the scroll:
			_pnlAlbumLayout.Top = -e.NewValue;
		}


		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}


		#region Selecting albums and Mouse Over

		private Color _highlightColor = Color.LightBlue;
		private Color _selectColor = Color.Blue;
		public Color HighlightColor { get { return _highlightColor; } set { _highlightColor = value; } }
		public Color SelectColor { get { return _selectColor; } set { _selectColor = value; } }

		private AlbumUserControl _albumSelected = null;
		private AlbumUserControl _albumHighlighted = null;
		private Color _albumHighilghtedOldColor;

		void _pnlAlbumLayout_MouseLeave(object sender, EventArgs e)
		{
		}

		void _pnlAlbumLayout_MouseEnter(object sender, EventArgs e)
		{
		}


		private void _pnlAlbumLayout_MouseMove(object sender, MouseEventArgs e)
		{
			AlbumUserControl albumAtPoint = GetAlbumAtLocation(e.Location);
			HighlightAlbum(albumAtPoint);
		}

		private void HighlightAlbum(AlbumUserControl albumToHightlight)
		{
			//if we have the same album, then there is nothing to do.
			if (albumToHightlight == _albumHighlighted)
			{
				return;
			}

			//Remove the highlighting:
			if (_albumHighlighted != null)
			{
				_albumHighlighted.BackColor = _albumHighilghtedOldColor;
				_albumHighlighted = null;
			}

			//If nothing is selected, then we are done:
			if (albumToHightlight == null)
			{
				return;
			}

			//Add highlighting:
			_albumHighlighted = albumToHightlight;
			_albumHighilghtedOldColor = _albumHighlighted.BackColor;
			_albumHighlighted.BackColor = HighlightColor;
		}


		/// <summary>
		/// Get the Album at the point 
		/// </summary>
		/// <param name="location">Client points to the _pnlAlbumLayout</param>
		/// <returns>The AlbumUserControl, or null if there was none there.</returns>
		private AlbumUserControl GetAlbumAtLocation(Point location)
		{
			AlbumUserControl control = _pnlAlbumLayout.GetChildAtPoint(location) as AlbumUserControl;

			return control;
		}

	
		#endregion
	}
}
