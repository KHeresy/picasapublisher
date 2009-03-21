using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleApi
{
	/// <summary>
	/// Used to identify a logged in user.
	/// </summary>
	public class GoogleAuthorizationToken
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
