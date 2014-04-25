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
// Original file : IYouTrackAdministrationProjectApi.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System.Collections.Generic;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Интерфейс API проекта YouTrack
	/// </summary>
	/// <remarks>
	/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Project" />
	/// </remarks>
	public interface IYouTrackAdministrationProjectApi : IYouTrackApiBase {
		/// <summary>
		/// 	Создает новый проект
		/// </summary>
		/// <param name="projectId"> Уникальный ID нового проекта </param>
		/// <param name="projectName"> Имя проекта </param>
		/// <param name="startingNumber"> Номер с которого начинается нумерация инцидентов </param>
		/// <param name="projectLeadLogin"> Логин пользователя, который будет использоваться в качестве главного участника проекта </param>
		/// <param name="description"> Описания проекта </param>
		void Create(string projectId, string projectName, int startingNumber, string projectLeadLogin, string description = "");

		/// <summary>
		/// 	Возвращает проект по его ID
		/// </summary>
		/// <param name="projectId"> ID проекта </param>
		/// <returns> </returns>
		IYouTrackProject Get(string projectId);

		/// <summary>
		/// 	Возвращает коллекцию всех проектов YouTrack
		/// </summary>
		/// <returns> </returns>
		IEnumerable<IYouTrackProject> Get();

		/// <summary>
		/// 	Возвращает коллекцию всех исполнителей проекта
		/// </summary>
		/// <param name="projectId"> </param>
		/// <returns> </returns>
		IEnumerable<IYouTrackAssignee> GetAssignees(string projectId);

		/// <summary>
		/// 	Возвращает коллекцию групп проекта
		/// </summary>
		/// <param name="projectId"> ID проекта </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Assignee+Groups" />
		/// </remarks>
		IEnumerable<IYouTrackGroup> GetAssigneesGroups(string projectId);
	}
}