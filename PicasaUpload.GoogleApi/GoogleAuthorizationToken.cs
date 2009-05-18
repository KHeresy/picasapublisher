using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicasaUpload.GoogleApi
{
	/// <summary>
	/// Used to identify a logged in user.
	/// </summary>
	internal class GoogleAuthorizationToken
	{
		private string _token;
		public string AuthorizationToken { get { return _token; } }

		private GoogleAuthorizationToken()
		{
		}

		public GoogleAuthorizationToken(string token)
		{
			_token = token;
		}
	}
}