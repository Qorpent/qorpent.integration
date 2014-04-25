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
// Original file : YouTrackApiBase.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Базовый интерфейс YouTrack
	/// </summary>
	public abstract class YouTrackApiBase : ServiceBase, IYouTrackConnectedBase, IYouTrackProtocolClient {
		///<summary>
		///	Создает экземпляр API используя переданный объект для настройки соединения
		///</summary>
		///<param name="connectionDescriptor"> <list type="number">
		///	                                    <item>NULL - будет использована из контейнера дефолтное соединение</item>
		///	                                    <item>строка - из контейнера будет взята
		///		                                    <see cref="IYouTrackConnectionFactory" />
		///		                                    и использован с передачей строки</item>
		///	                                    <item>
		///		                                    <see cref="IYouTrackConnection" />
		///		                                    - прямая передача соединения</item>
		///	                                    <item>
		///		                                    <see cref="IYouTrackConnectedBase" />
		///		                                    - будет использовано родительское соединение</item>
		///	                                    <item>Иначе - исключение
		///		                                    <see cref="ArgumentException" />
		///	                                    </item>
		///                                    </list> </param>
		public YouTrackApiBase(object connectionDescriptor = null) {
			_connectionDescriptor = connectionDescriptor;
			if (_connectionDescriptor is IYouTrackProtocolClient) {
				var parent = (IYouTrackProtocolClient) _connectionDescriptor;
				Connection = parent.Connection;
				RequestFactory = parent.RequestFactory;
				ResponseAdapter = parent.ResponseAdapter;
				if (parent is ServiceBase) {
					SourceContainer = ((ServiceBase) parent).Container;
				}
			}
		}


		/// <summary>
		/// 	Соединение с сервером YouTrack
		/// </summary>
		public IYouTrackConnection Connection {
			get {
				if (null == _connection) {
					if (null == _connectionDescriptor) {
						_connection = ResolveService<IYouTrackConnectionFactory>().Get("Default");
					}
					else if (_connectionDescriptor is string) {
						_connection = ResolveService<IYouTrackConnectionFactory>().Get((string) _connectionDescriptor);
					}
					else if (_connectionDescriptor is IYouTrackConnection) {
						_connection = (IYouTrackConnection) _connectionDescriptor;
					}
					else if (_connectionDescriptor is IYouTrackConnectedBase) {
						_connection = ((IYouTrackConnectedBase) _connectionDescriptor).Connection;
					}
					else {
						throw new YouTrackApiException("Неверный дескриптор соединения " + _connectionDescriptor);
					}
				}
				return _connection;
			}
			set { _connection = value; }
		}


		/// <summary>
		/// </summary>
		public IYouTrackRequestFactory RequestFactory {
			get {
				return _requestFactory ?? (_requestFactory =
				                           (ResolveService<IYouTrackRequestFactory>() ?? new YouTrackRequestFactory()));
			}
			set { _requestFactory = value; }
		}

		/// <summary>
		/// 	Обертка над интерфейсом ответа сервера YouTrack
		/// </summary>
		public IYouTrackResponseAdapter ResponseAdapter {
			get {
				return _responseAdapter ?? (_responseAdapter = (
					                                               ResolveService<IYouTrackResponseAdapter>() ??
					                                               new YouTrackResponseAdapter()
				                                               ));
			}
			set { _responseAdapter = value; }
		}


		/// <summary>
		/// 	Выполняет запрос к серверу YouTrack
		/// </summary>
		/// <typeparam name="T"> </typeparam>
		/// <param name="method"> HTTP метод для вызова </param>
		/// <param name="commandName"> </param>
		/// <param name="parameters"> </param>
		/// <param name="errorInfo"> </param>
		/// <returns> </returns>
		protected virtual T Execute<T>(string method, string commandName, object parameters, string errorInfo) where T : class {
			var request = RequestFactory.Get(method, commandName, parameters);
			IYouTrackResponse response = null;
			try {
				response = Connection.Execute(request);
			}
			catch (YouTrackConnectionException ex) {
				throw new YouTrackApiException(errorInfo, request, ex);
			}
			if (typeof (IgnoreResponse) == typeof (T)) {
				return default(T);
			}
			var result = ResponseAdapter.Get<T>(response);
			return result;
		}

		private readonly object _connectionDescriptor;
		private IYouTrackConnection _connection;
		private IYouTrackRequestFactory _requestFactory;
		private IYouTrackResponseAdapter _responseAdapter;
	}
}