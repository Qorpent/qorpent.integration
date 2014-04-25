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
// Original file : YouTrackGeneralIssueApi.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using Qorpent.IoC;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Стандартная реализация <see cref="IYouTrackGeneralIssueApi" />
	/// </summary>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackGeneralIssueApi : YouTrackApiBase, IYouTrackGeneralIssueApi {
		/// <summary>
		/// 	Экземпляр API
		/// </summary>
		/// <param name="connectionDescriptor"> Объект испулоьзуемый для настройки соединения </param>
		public YouTrackGeneralIssueApi(object connectionDescriptor = null)
			: base(connectionDescriptor) {}


		/// <summary>
		/// 	Интерфейс API инцидента YouTrack
		/// </summary>
		public IYouTrackGeneralIssueApi Issue { get; set; }

		/// <summary>
		/// 	Интерфейс создания нового инцидента YouTrack
		/// </summary>
		/// <param name="project"> ID проекта </param>
		/// <param name="summary"> Описание инцидента </param>
		/// <param name="description"> Более подробное описание </param>
		/// <param name="permittedGroup"> Группы пользователей, которым будет виден инцидент </param>
		/// <remarks>
		/// 	Из API создания интерфейса убран параметр attachments (явно не к месту + экономия трафика)
		/// </remarks>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Create+New+Issue" />
		/// </remarks>
		public IssueRef Create(string project, string summary, string description, string permittedGroup) {
			return Execute<IssueRef>(
				"PUT",
				"issue",
				new {project, summary, description, permittedGroup},
				"Попытка создать новый инцидент");
		}

		/// <summary>
		/// 	Получение инцидента по его ID
		/// </summary>
		/// <param name="issueId"> ID инцидента </param>
		/// <returns> Инцидент YouTrack </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Get+an+Issue" />
		/// </remarks>
		public IYouTrackIssue Get(string issueId) {
			return
				Execute<IYouTrackIssue>("GET",
				                        "issue/" + issueId, null,
				                        "Попытка найти инцидент по его ID");
		}

		/// <summary>
		/// 	Выполняет комманду над указанным инцидентом
		/// </summary>
		/// <param name="issueId"> ID инцидента </param>
		/// <param name="command"> Текст команды </param>
		/// <param name="comment"> Комментарий (если команда пустая, комментарий просто применяется к инциденту) </param>
		/// <param name="group"> Группа пользователей, которым виден комментарий </param>
		/// <param name="runAs"> Имя пользователя, от которого выполнить команду </param>
		/// <param name="disableNotifications"> Если true, изменения не вызывают оповещений </param>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Apply+Command+to+an+Issue" />
		/// </remarks>
		public void ApplyCommand(string issueId, string command, string comment, string group, string runAs,
		                         bool disableNotifications = false) {
			Execute<IgnoreResponse>(
				"POST",
				"issue/" + issueId + "/execute",
				new {command, comment, group, runAs, disableNotifications},
				"Попытка выполнить команду");
		}

		/// <summary>
		/// 	Добавляет комментарий к инциденту по его ID
		/// </summary>
		/// <param name="issueId"> ID инцидента </param>
		/// <param name="comment"> Тект комментария </param>
		public void ApplyComment(string issueId, string comment) {
			Execute<IgnoreResponse>(
				"POST",
				"issue/" + issueId + "/execute",
				new {comment, disableNotifications = true},
				"Попытка добавить комментарий к инциденту");
		}
	}
}