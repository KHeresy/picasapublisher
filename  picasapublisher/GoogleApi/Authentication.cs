using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace GoogleApi
{
	/// <summary>
	/// Allows authentication against a google service.  Followed documentation located at:
	/// http://code.google.com/apis/accounts/docs/AuthForInstalledApps.html
	/// </summary>
	public class Authentication
	{
		private const string AUTHENTICATE_URL = "https://www.google.com/accounts/ClientLogin";
		/// <summary>
		/// This is the class that gets returned after a call to Authenticate:
		/// 
		/// </summary>
		public class AuthenticationResult
		{
			/// <summary>
			/// Possible values for ErrorCodes:  
			/// </summary>
			public enum ErrorCodes
			{
				BadAuthentication,
				NotVerified,
				TermsNotAgreed,
				CaptchaRequired,
				Unknown,
				AccountDeleted,
				AccountDisabled,
				ServiceDisabled,
				ServiceUnavailable,
				None
				
			}

			/// <summary>
			/// Get the error message associated with the ErrorCode:
			/// </summary>
			/// <returns></returns>
			public  string GetErrorMessage()
			{
				switch (_errorCode)
				{
					case ErrorCodes.AccountDeleted:
						return "The user account has been deleted.";
					case ErrorCodes.AccountDisabled:
						return "The user account has been disabled.";
					case ErrorCodes.BadAuthentication:
						return "The login request used a username or password that is not recognized.";
					case ErrorCodes.CaptchaRequired:
						return "A CAPTCHA is required.";
					case ErrorCodes.NotVerified:
						return "The account email address has not been verified. The user will need to access their Google account directly to resolve the issue before logging in using a non-Google application.";
					case ErrorCodes.ServiceDisabled:
						return "The user's access to the specified service has been disabled.";
					case ErrorCodes.ServiceUnavailable:
						return "The service is not available; try again later.";
					case ErrorCodes.TermsNotAgreed:
						return "The user has not agreed to terms. The user will need to access their Google account directly to resolve the issue before logging in using a non-Google application.";
					case ErrorCodes.None:
						return "No Errors Reported.";
					default:
						return "The error is unknown or unspecified; the request contained invalid input or was malformed.";

				}
			}


			private const string RESPONSE_OK = "OK";

			private bool _isSuccessful = false;
			public bool IsSuccessful { get { return _isSuccessful; } }

			private GoogleAuthorizationToken _authKey = null;
			public GoogleAuthorizationToken AuthKey { get { return _authKey; } }

			private string _response = string.Empty;
			public string Response { get { return _response; } }

			private ErrorCodes _errorCode = ErrorCodes.None;
			public ErrorCodes ErrorCode { get { return _errorCode; } }

			private string _captchaToken = string.Empty;
			public string CaptchaToken { get { return _captchaToken; } }

			private string _captchaUrl = string.Empty;
			public string CaptchaUrl { get { return _captchaUrl; } }

			private AuthenticationResult()
			{
			}

			internal AuthenticationResult(HttpWebResponse response)
			{
				string responseStatus = response.StatusDescription;
				_isSuccessful = (responseStatus == RESPONSE_OK);

				StreamReader sr = new StreamReader(response.GetResponseStream());
				_response = sr.ReadToEnd();

				if (_isSuccessful)
				{
					_authKey = new GoogleAuthorizationToken(ParseResponseForKey(_response, "Auth"));
				}
				else
				{
					_errorCode = (ErrorCodes)Enum.Parse(typeof(ErrorCodes), ParseResponseForKey(_response, "Error"));
					_captchaToken = ParseResponseForKey(_response, "CaptchaToken");
					_captchaUrl = ParseResponseForKey(_response, "CaptchaUrl");
				}
			}

			private string ParseResponseForKey(string response, string key)
			{
				string findKey = key + "=";

				int start = response.IndexOf(findKey);
				if (start < 0)
				{
					return string.Empty;
				} 
				start += findKey.Length;

				int end = response.IndexOf("\n", start);
				if (end < 0)
				{
					return string.Empty;
				}
				int count =  end - start;

				return response.Substring(start, count);
			}
		}

		/// <summary>
		/// The account types:
		/// </summary>
		public enum AccountTypes
		{
			GOOGLE,
			HOSTED,
			HOSTED_OR_GOOGLE
		}

		/// <summary>
		/// The Google Services supported:
		/// </summary>
		public enum Services
		{
			CALENDAR,
			GOOGLE_BASE,
			BLOGGER,
			CONTACTS,
			DOCUMENTS_LIST,
			PICASA_WEB_ALBUMS,
			GOOGLE_APPS_PROVISIONING,
			SPREADSHEETS,
			YOUTUBE
		}

		/// <summary>
		/// Authenticate with the following parameters.
		/// </summary>
		/// <param name="accountType"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <param name="service"></param>
		/// <param name="applicationSource"></param>
		/// <returns>An AuthenticationResult object with the results of the Authenticate call.</returns>
		public static AuthenticationResult Authenticate(AccountTypes accountType, string email, string password, Services service, string applicationSource)
		{
			return Authenticate(accountType, email, password, service, applicationSource, null, null);
		}

		/// <summary>
		/// Authenticate with the following parameters.
		/// </summary>
		/// <param name="accountType"></param>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <param name="service"></param>
		/// <param name="applicationSource"></param>
		/// <param name="loginToken"></param>
		/// <param name="logincaptcha"></param>
		/// <returns>An AuthenticationResult object with the results of the Authenticate call.</returns>
		public static AuthenticationResult Authenticate(AccountTypes accountType, string email, string password, Services service, string applicationSource, string loginToken, string logincaptcha)
		{
			StringBuilder postData = new StringBuilder();

			string postDataFormat = "{0}={1}&";
			postData.AppendFormat(postDataFormat, "accountType", accountType.ToString());
			postData.AppendFormat(postDataFormat, "Email", email);
			postData.AppendFormat(postDataFormat, "Passwd", password);
			postData.AppendFormat(postDataFormat, "service", GetServiceName(service));
			postData.AppendFormat(postDataFormat, "source", applicationSource);

			//If these are included, add them:
			if (!string.IsNullOrEmpty(loginToken))
			{
				postData.AppendFormat(postDataFormat, "logintoken", loginToken);
			}
			if (!string.IsNullOrEmpty(logincaptcha))
			{
				postData.AppendFormat(postDataFormat, "logincaptcha", logincaptcha);
			}

			UTF8Encoding encoding = new UTF8Encoding();
			byte[] byteData = encoding.GetBytes(postData.ToString());

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(AUTHENTICATE_URL);
			req.Method = "POST";
			req.ContentType = "application/x-www-form-urlencoded";
			req.ContentLength = byteData.Length;

			Stream requestStream = req.GetRequestStream();
			requestStream.Write(byteData, 0, byteData.Length);
			requestStream.Close();

			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)req.GetResponse();
			}
			catch (WebException we)
			{
				response = (HttpWebResponse)we.Response;
			}

			AuthenticationResult result = new AuthenticationResult(response);
			response.Close();
			

			return result;
		}

		/// <summary>
		/// Gets the google service name:
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		private static string GetServiceName(Services service)
		{
			switch (service)
			{
				case Services.BLOGGER:
					return "blogger";
				case Services.CALENDAR:
					return "cl";
				case Services.CONTACTS:
					return "cp";
				case Services.DOCUMENTS_LIST:
					return "writely";
				case Services.GOOGLE_APPS_PROVISIONING:
					return "apps";
				case Services.GOOGLE_BASE:
					return "gbase";
				case Services.PICASA_WEB_ALBUMS:
					return "lh2";
				case Services.SPREADSHEETS:
					return "wise";
				case Services.YOUTUBE:
					return "youtube";	
			}

			return string.Empty;
		}
		
	}
}
