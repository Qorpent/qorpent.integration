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
// Original file : YouTrackConnectionFactory.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using Qorpent.IoC;

namespace Qorpent.Integration.YouTrack {
    /// <summary>
    /// </summary>
    [ContainerComponent(Lifestyle.Singleton)]
    public class YouTrackConnectionFactory : ServiceBase, IYouTrackConnectionFactory {
        /// <summary>
        ///   Возвращает соединение с YouTrack по имени соединения
        /// </summary>
        /// <param name="connectionName"> Имя соединения </param>
        /// <returns> Соединение с YouTrack </returns>
        public IYouTrackConnection Get(string connectionName) {
            lock (this) {
                var name = connectionName + ".youtrack.connection";
                var connection = ResolveService<IYouTrackConnection>(name);
                if (null == connection) {
                    var descriptor = ResolveService<IYouTrackConnectionDescriptor>();
                    if (null != descriptor) {
                        connection = ResolveService<IYouTrackConnection>(null, descriptor.Server, descriptor.User,
                                                                         descriptor.Password);
                    }
                }
                if (null == connection && connectionName.ToLower() == "default") {
                    connection = ResolveService<IYouTrackConnection>();
                }
                if (null == connection) {
                    throw new YouTrackConnectionException(
                        "Не удалось найти подключение к YouTrack с именем " + connectionName, null,
                        null, null, null);
                }
                connection.TestConnection();
                return connection;
            }
        }

        /// <summary>
        ///   Возвращает соединение с YouTrack
        /// </summary>
        /// <param name="serverName"> Адрес сервера </param>
        /// <param name="user"> Имя пользователя </param>
        /// <param name="password"> Пароль </param>
        /// <returns> Соединение с YouTrack </returns>
        public IYouTrackConnection Get(string serverName, string user, string password) {
            var connection = ResolveService<IYouTrackConnection>(null, serverName, user, password);
            connection.TestConnection();
            return connection;
        }
    }
}