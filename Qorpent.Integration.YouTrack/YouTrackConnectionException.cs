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
// Original file : YouTrackConnectionException.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Ошибка при работе соединения с YouTrack
	/// </summary>
	[Serializable]
	public class YouTrackConnectionException : Exception {
		/// <summary>
		/// 	Создает новое исключение при работе YouTrack соединения
		/// </summary>
		/// <param name="message"> Пользовательское сообшение </param>
		/// <param name="server"> Адрес сервера </param>
		/// <param name="user"> Пользователь (login) </param>
		/// <param name="request"> Запрос </param>
		/// <param name="innerException"> Внутреннее необработанное исключение </param>
		public YouTrackConnectionException(string message, string server, string user, IYouTrackRequest request,
		                                   Exception innerException) : base(message, innerException) {
			_server = server;
			_user = user;
			_request = request;
		}

		/// <summary>
		/// 	Адрес сервера YouTrack
		/// </summary>
		public string Server {
			get { return _server; }
		}

		/// <summary>
		/// 	Пользователь сервера YouTrack
		/// </summary>
		public string User {
			get { return _user; }
		}

		/// <summary>
		/// 	Запрос, при выполнении которого возникла ошибка
		/// </summary>
		public IYouTrackRequest Request {
			get { return _request; }
		}

		private readonly IYouTrackRequest _request;
		private readonly string _server;
		private readonly string _user;
	}
}