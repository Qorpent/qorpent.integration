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
	/// 	��������� API ������� YouTrack
	/// </summary>
	/// <remarks>
	/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Project" />
	/// </remarks>
	public interface IYouTrackAdministrationProjectApi : IYouTrackApiBase {
		/// <summary>
		/// 	������� ����� ������
		/// </summary>
		/// <param name="projectId"> ���������� ID ������ ������� </param>
		/// <param name="projectName"> ��� ������� </param>
		/// <param name="startingNumber"> ����� � �������� ���������� ��������� ���������� </param>
		/// <param name="projectLeadLogin"> ����� ������������, ������� ����� �������������� � �������� �������� ��������� ������� </param>
		/// <param name="description"> �������� ������� </param>
		void Create(string projectId, string projectName, int startingNumber, string projectLeadLogin, string description = "");

		/// <summary>
		/// 	���������� ������ �� ��� ID
		/// </summary>
		/// <param name="projectId"> ID ������� </param>
		/// <returns> </returns>
		IYouTrackProject Get(string projectId);

		/// <summary>
		/// 	���������� ��������� ���� �������� YouTrack
		/// </summary>
		/// <returns> </returns>
		IEnumerable<IYouTrackProject> Get();

		/// <summary>
		/// 	���������� ��������� ���� ������������ �������
		/// </summary>
		/// <param name="projectId"> </param>
		/// <returns> </returns>
		IEnumerable<IYouTrackAssignee> GetAssignees(string projectId);

		/// <summary>
		/// 	���������� ��������� ����� �������
		/// </summary>
		/// <param name="projectId"> ID ������� </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/GET+Assignee+Groups" />
		/// </remarks>
		IEnumerable<IYouTrackGroup> GetAssigneesGroups(string projectId);
	}
}