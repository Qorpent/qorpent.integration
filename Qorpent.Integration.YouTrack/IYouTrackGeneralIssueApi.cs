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
// Original file : IYouTrackGeneralIssueApi.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Интерфейс API инцидента YouTrack
	/// </summary>
	public interface IYouTrackGeneralIssueApi : IYouTrackApiBase, IYouTrackGeneralApi {
		/// <summary>
		/// 	Интерфейс создания нового инцидента YouTrack
		/// </summary>
		/// <param name="project"> ID проекта </param>
		/// <param name="summary"> Описание инцидента </param>
		/// <param name="description"> Более подробное описание </param>
		/// <param name="permittedGroup"> Группы пользователей, которым будет виден инцидент </param>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Create+New+Issue" />
		/// </remarks>
		IssueRef Create(string project, string summary, string description, string permittedGroup);

		/// <summary>
		/// 	Получение инцидента по его ID
		/// </summary>
		/// <param name="issueId"> ID инцидента </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Get+an+Issue" />
		/// </remarks>
		IYouTrackIssue Get(string issueId);

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
		void ApplyCommand(string issueId, string command, string comment="", string group="", string runAs="",
		                  bool disableNotifications = false);

		/// <summary>
		/// 	Добавляет комментарий к инциденту по его ID
		/// </summary>
		/// <param name="issueId"> ID инцидента </param>
		/// <param name="comment"> Тект комментария </param>
		void ApplyComment(string issueId, string comment);
	}
}