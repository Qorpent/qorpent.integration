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
// Original file : IYouTrackConnectionFactory.cs
// Project: Qorpent.Integration.YouTrack
// 
// MODIFICATIONS HAVE BEEN MADE TO THIS FILE

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// Интерфейс фабрики для работы с соединением YouTrack
	/// </summary>
	public interface IYouTrackConnectionFactory {
		/// <summary>
		/// Возвращает соединение с YouTrack по имени соединения
		/// </summary>
		/// <param name="connectionName">Имея соединения</param>
		/// <returns>Соединение с YouTrack</returns>
		IYouTrackConnection Get(string connectionName);

		/// <summary>
		/// Возвращает соединение с YouTrack
		/// </summary>
		/// <param name="serverName">Адрес сервера</param>
		/// <param name="user">Имя пользователя</param>
		/// <param name="password">Пароль</param>
		/// <returns>Соединение с YouTrack</returns>
		IYouTrackConnection Get(string serverName, string user, string password);
	}
}