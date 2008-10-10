namespace PicasaUpload.UI
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
			this._wpfHost = new System.Windows.Forms.Integration.ElementHost();
			this.SuspendLayout();
			// 
			// _wpfHost
			// 
			this._wpfHost.Location = new System.Drawing.Point(12, 12);
			this._wpfHost.Name = "_wpfHost";
			this._wpfHost.Size = new System.Drawing.Size(268, 242);
			this._wpfHost.TabIndex = 0;
			this._wpfHost.Text = "elementHost1";
			this._wpfHost.Child = null;
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this._wpfHost);
			this.Name = "LoginForm";
			this.Text = "LoginForm";
			this.Load += new System.EventHandler(this.LoginForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Integration.ElementHost _wpfHost;
	}
}