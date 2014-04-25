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
	/// 	API �������
	/// </summary>
	/// <remarks>
	/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Project" />
	/// </remarks>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackAdministrationProjectApi : YouTrackApiBase, IYouTrackAdministrationProjectApi {
		/// <summary>
		/// 	��������� API
		/// </summary>
		/// <param name="connectionDescriptor"> ������ ������������� ��� ��������� ���������� </param>
		public YouTrackAdministrationProjectApi(object connectionDescriptor = null) : base(connectionDescriptor) {}


		/// <summary>
		/// 	������� ����� ������
		/// </summary>
		/// <param name="projectId"> ���������� ID ������ ������� </param>
		/// <param name="projectName"> ��� ������� </param>
		/// <param name="startingNumber"> ����� � �������� ���������� ��������� ���������� </param>
		/// <param name="projectLeadLogin"> ����� ������������, ������� ����� �������������� � �������� �������� ��������� ������� </param>
		/// <param name="description"> �������� ������� </param>
		public void Create(string projectId, string projectName, int startingNumber, string projectLeadLogin,
		                   string description = "") {
			Execute<IgnoreResponse>(
				"PUT",
				"admin/project",
				new {projectId, projectName, startingNumber, projectLeadLogin, description},
				"������� ������� ����� ������");
		}

		/// <summary>
		/// 	���������� ������ �� ��� ID
		/// </summary>
		/// <param name="projectId"> ID ������� </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Project">������ �� API</see>
		/// </remarks>
		public IYouTrackProject Get(string projectId) {
			return
				Execute<IYouTrackProject>("GET",
				                          "admin/project/" + projectId, null,
				                          "����� ������� �� ��� ID");
		}

		/// <summary>
		/// 	���������� ��������� ���� �������� YouTrack
		/// </summary>
		/// <returns> </returns>
		public IEnumerable<IYouTrackProject> Get() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// 	���������� ��������� ���� ������������ �������
		/// </summary>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Assignee" />
		/// </remarks>
		public IEnumerable<IYouTrackAssignee> GetAssignees(string projectId) {
			return Execute<IEnumerable<IYouTrackAssignee>>("GET",
			                                               "admin/project/" + projectId + "/assignee", null,
			                                               "������� �������� ������ ������������ �������");
		}

		/// <summary>
		/// 	���������� ��������� ����� �������
		/// </summary>
		/// <param name="projectId"> ID ������� </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Assignee+Groups" />
		/// </remarks>
		public IEnumerable<IYouTrackGroup> GetAssigneesGroups(string projectId) {
			return Execute<IEnumerable<IYouTrackGroup>>("GET",
			                                            "admin/project/" + projectId + "/assignee/group", null,
			                                            "������� �������� ������ ����� �������");
		}


		/// <summary>
		/// 	������� ������ �� ��� ID
		/// </summary>
		/// <param name="projectId"> </param>
		public void Delete(string projectId) {
			Execute<IYouTrackProject>("DELETE",
			                          "admin/project/" + projectId, null,
			                          "����� ������� �� ��� ID");
		}
	}
}