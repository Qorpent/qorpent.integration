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
// Original file : YouTrackAdministrationProjectApi.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System;
using System.Collections.Generic;
using Qorpent.IoC;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	API проекта
	/// </summary>
	/// <remarks>
	/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Project" />
	/// </remarks>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackAdministrationProjectApi : YouTrackApiBase, IYouTrackAdministrationProjectApi {
		/// <summary>
		/// 	Экземпляр API
		/// </summary>
		/// <param name="connectionDescriptor"> Объект испулоьзуемый для настройки соединения </param>
		public YouTrackAdministrationProjectApi(object connectionDescriptor = null) : base(connectionDescriptor) {}


		/// <summary>
		/// 	Создает новый проект
		/// </summary>
		/// <param name="projectId"> Уникальный ID нового проекта </param>
		/// <param name="projectName"> Имя проекта </param>
		/// <param name="startingNumber"> Номер с которого начинается нумерация инцидентов </param>
		/// <param name="projectLeadLogin"> Логин пользователя, который будет использоваться в качестве главного участника проекта </param>
		/// <param name="description"> Описания проекта </param>
		public void Create(string projectId, string projectName, int startingNumber, string projectLeadLogin,
		                   string description = "") {
			Execute<IgnoreResponse>(
				"PUT",
				"admin/project",
				new {projectId, projectName, startingNumber, projectLeadLogin, description},
				"Попытка создать новый проект");
		}

		/// <summary>
		/// 	Возвращает проект по его ID
		/// </summary>
		/// <param name="projectId"> ID проекта </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Project">Ссылка на API</see>
		/// </remarks>
		public IYouTrackProject Get(string projectId) {
			return
				Execute<IYouTrackProject>("GET",
				                          "admin/project/" + projectId, null,
				                          "Поиск объекта по его ID");
		}

		/// <summary>
		/// 	Возвращает коллекцию всех проектов YouTrack
		/// </summary>
		/// <returns> </returns>
		public IEnumerable<IYouTrackProject> Get() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// 	Возвращает коллекцию всех исполнителей проекта
		/// </summary>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Assignee" />
		/// </remarks>
		public IEnumerable<IYouTrackAssignee> GetAssignees(string projectId) {
			return Execute<IEnumerable<IYouTrackAssignee>>("GET",
			                                               "admin/project/" + projectId + "/assignee", null,
			                                               "Попытка получить список исполнителей проекта");
		}

		/// <summary>
		/// 	Возвращает коллекцию групп проекта
		/// </summary>
		/// <param name="projectId"> ID проекта </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Assignee+Groups" />
		/// </remarks>
		public IEnumerable<IYouTrackGroup> GetAssigneesGroups(string projectId) {
			return Execute<IEnumerable<IYouTrackGroup>>("GET",
			                                            "admin/project/" + projectId + "/assignee/group", null,
			                                            "Попытка получить список групп проекта");
		}


		/// <summary>
		/// 	Удаляет проект по его ID
		/// </summary>
		/// <param name="projectId"> </param>
		public void Delete(string projectId) {
			Execute<IYouTrackProject>("DELETE",
			                          "admin/project/" + projectId, null,
			                          "Поиск объекта по его ID");
		}
	}
}