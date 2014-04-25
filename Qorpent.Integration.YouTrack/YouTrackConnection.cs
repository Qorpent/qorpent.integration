#region LICENSE

// Copyright 2007-2012 Comdiv (F. Sadykov) - http://code.google.com/u/fagim.sadykov/
// Supported by Media Technology LTD 
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
// http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// Solution: Qorpent
// Original file : YouTrackConnection.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using Qorpent.IoC;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Соединение с сервером YouTrack, обслуживает запросы
	/// </summary>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackConnection : IYouTrackConnection {
		/// <summary>
		/// 	Конструктор соединения с сервером YouTrack
		/// </summary>
		public YouTrackConnection() {}

		/// <summary>
		/// 	Конструктор соединения с сервером YouTrack
		/// </summary>
		/// <param name="server"> Сервер YouTrack </param>
		/// <param name="user"> Имя пользователя </param>
		/// <param name="password"> Пароль </param>
		public YouTrackConnection(string server, string user, string password) {
			_server = server;
			_user = user;
			_password = password;
		}

		/// <summary>
		/// Доступ к свойству адреса сервера
		/// </summary>
		public string Server {
			get { return _server; }
			set {  _server = value; }
		}

		/// <summary>
		/// 	Имя текущего пользователя
		/// </summary>
		public string User {
			get { return _user; }
			set { _user = value; }
		}

		/// <summary>
		/// 	Пароль текущего пользователя
		/// </summary>
		public string Password {
			get { return "IT_IS_NOT_PASSWORD_PASSWORD_HIDDEN"; }
			set { _password = value; }
		}


		/// <summary>
		/// </summary>
		public void Dispose() {
			
		}

		/// <summary>
		/// 	Выполняет запрос к серверу YouTrack
		/// </summary>
		/// <param name="request"> </param>
		/// <returns> </returns>
		public IYouTrackResponse Execute(IYouTrackRequest request) {
			lock(this) {
				if (_cookies.Count == 0) {
					TestConnection();
				}
				try {
					return DirectExecute(request);
				}
				catch (YouTrackConnectionException) {
					TestConnection();
					return DirectExecute(request);
				}
			}
		}

		private IYouTrackResponse DirectExecute(IYouTrackRequest request) {
			var result = new YouTrackResponse();
			using (
				var webresult = GetResponse(_server + ("/rest/" + request.Command).Replace("//", "/"), request.Method,
				                            request.Parameters)) {
				var content = string.Empty;
				using (var sr = new StreamReader(webresult.GetResponseStream(), Encoding.UTF8)) {
					content = sr.ReadToEnd();
				}
				if (content.IsEmpty()) {
					content = "<location>" + webresult.Headers["Location"] + "</location>";
				}
				result.Content = XElement.Parse(content);
			}
			return result;
		}

		/// <summary>
		/// 	Проверяет, что соединение живое
		/// </summary>
		/// <param name="throwError"> </param>
		/// <returns> </returns>
		public bool TestConnection(bool throwError = true) {
			try {
				Login();
				return true;
			}
			catch (YouTrackConnectionException) {
				if (throwError) {
					throw;
				}
			}
			catch (Exception e) {
				if (throwError) {
					throw new YouTrackConnectionException("Необработанное исключение при подключении", _server, _user, null, e);
				}
			}

			return false;
		}


		private void Login() {
			try {
				var loginresponse = GetResponse(
					string.Format("{0}/rest/user/login", _server), "POST",
					new Dictionary<string, string> {{"login", _user}, {"password", _password}});
				var sessioncookie = loginresponse.Cookies["YTJSESSIONID"];
				var principalcookie = loginresponse.Cookies["jetbrains.charisma.main.security.PRINCIPAL"];
				if (null != sessioncookie) {
					_cookies.Add(sessioncookie);
				}
				if (null != principalcookie) {
					_cookies.Add(principalcookie);
				}
			}
			catch (YouTrackConnectionException e) {
				if (e.InnerException != null && e.InnerException.Message.Contains("403")) {
					throw new YouTrackConnectionException("Неверное имя или пароль", _server, _user, null, e.InnerException);
				}
				throw;
			}
		}



		private HttpWebResponse GetResponse(string url, string method, IDictionary<string, string> options) {
			var postdata = string.Empty;

			if (null != options) {
				foreach (var o in options) {
					if(o.Value.IsNotEmpty()) {
						postdata += o.Key + "=" + o.Value.Replace("&", "__AMP__") + "&";
					}
				}
				postdata = postdata.Substring(0, postdata.Length - 1);
			}
			if (method != "POST" && postdata != string.Empty) {
				url = url + "?" + postdata;
			}
			var request = GetRequest(url);
			request.Method = method;
			request.KeepAlive = true;
			request.Host = request.RequestUri.Host;
			request.CookieContainer = _cookies;
			ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
			try {
				if (method == "POST") {
					var safepostdata = Uri.EscapeUriString(postdata);
					safepostdata = safepostdata.Replace("__AMP__", "%26");

					request.ContentLength = safepostdata.Length;
					request.ContentType = "application/x-www-form-urlencoded";

					var sw = new StreamWriter(request.GetRequestStream());
					sw.Write(safepostdata);
					sw.Flush();
					sw.Close();
				}
				var response = (HttpWebResponse) request.GetResponse();
				return response;
			}
			catch (WebException e) {
				if (e.Message.Contains("Unable to connect")) {
					throw new YouTrackConnectionException("Сервер не найден", _server, _user, null, e);
				}
				else if (e.Message.Contains("403")) {
					throw new YouTrackConnectionException("Недостаточно полномочий", _server, _user, null, e);
				}
				throw new YouTrackConnectionException("Ошибка соединения", _server, _user, null, e);
			}
			finally {
				ServicePointManager.ServerCertificateValidationCallback -= ServerCertificateValidationCallback;
			}
		}

		private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			return true;
		}

		private HttpWebRequest GetRequest(string url) {
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.CookieContainer = _cookies;

			return request;
		}

		/// <summary>
		/// 	Кукисы
		/// </summary>
		private readonly CookieContainer _cookies = new CookieContainer();

		/// <summary>
		/// 	Пароль текущего пользователя
		/// </summary>
		private string _password = string.Empty;

		/// <summary>
		/// 	Текущий сервер YouTrack
		/// </summary>
		public string _server = string.Empty;

		/// <summary>
		/// 	Имя текущего пользователя
		/// </summary>
		private string _user = string.Empty;
	}
}