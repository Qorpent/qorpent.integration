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
// Original file : IYouTrackIssue.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Интерфейс инцидента YouTrack
	/// </summary>
	public interface IYouTrackIssue : IYouTrackModelElement {
		/// <summary>
		/// 	ID инцидента
		/// </summary>
		string Id { get; set; }

		/// <summary>
		/// 	ID инцидента в Jira (если инцидент импортирован из Jira)
		/// </summary>
		string JiraId { get; set; }

		/// <summary>
		/// 	Короткое имя проекта в который входит инцидент
		/// </summary>
		string ProjectShortName { get; set; }

		/// <summary>
		/// 	Порядковый номер инцидента в проекте
		/// </summary>
		string NumberInProject { get; set; }

		/// <summary>
		/// 	Краткое описание инцидента
		/// </summary>
		string Summary { get; set; }

		/// <summary>
		/// 	Полное описание инцидента
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// 	Время создания инцидента (кол-во миллисекунд с 1 Января 1970 года)
		/// </summary>
		long Created { get; set; }

		/// <summary>
		/// 	Время последнего обновления инцидента (кол-во миллисекунд с 1 Января 1970 года)
		/// </summary>
		long Updated { get; set; }

		/// <summary>
		/// 	Логин пользователя который внес последние изменения в инцидент
		/// </summary>
		string UpdaterName { get; set; }

		/// <summary>
		/// 	Время закрытия инцидента (если он вообще закрыт)
		/// </summary>
		long Resolved { get; set; }

		/// <summary>
		/// 	Логин пользователя, создавшего инцидент
		/// </summary>
		string ReporterName { get; set; }

		/// <summary>
		/// 	Колличество комментариев к инциденту
		/// </summary>
		int CommentsCount { get; set; }

		/// <summary>
		/// 	Количество голосов у инцидентаы
		/// </summary>
		int Votes { get; set; }

		/// <summary>
		/// 	Группы пользователей, которые видят инцидент
		/// </summary>
		string PermittedGroup { get; set; }

		/// <summary>
		/// 	Комментарии к инциденту
		/// </summary>
		IYouTrackComment[] Comment { get; set; }

		/// <summary>
		/// 	Поля инцидента
		/// </summary>
		IYouTrackField[] Field { get; set; }

		/// <summary>
		/// 	Поля инцидента
		/// </summary>
		IYouTrackTag[] Tag { get; set; }
	}
}