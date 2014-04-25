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
// Original file : IYouTrackProject.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Интерфейс проекта YouTrack
	/// </summary>
	public interface IYouTrackProject : IYouTrackModelElement {
		/// <summary>
		/// 	ID проекта
		/// </summary>
		string Id { get; set; }

		/// <summary>
		/// 	Ссылка на проект
		/// </summary>
		string Url { get; set; }

		/// <summary>
		/// 	Логин главного учсника проекта
		/// </summary>
		string Lead { get; set; }

		/// <summary>
		/// 	Описание проекта
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// 	Имя проекта
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// 	Ссылка на страницу настройки исполнителей
		/// </summary>
		string AssigneesUrl { get; set; }

		/// <summary>
		/// 	Ссылка на страницу настройки подсистем
		/// </summary>
		string SubsystemsUrl { get; set; }

		/// <summary>
		/// 	Страница с перечнем сборок TeamCity
		/// </summary>
		string BuildsUrl { get; set; }

		/// <summary>
		/// 	Страница с перечнем версий проекта
		/// </summary>
		string VersionsUrl { get; set; }


		//ИТД
	}
}