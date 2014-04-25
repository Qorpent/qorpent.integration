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
	/// 	����������� ���������� <see cref="IYouTrackGeneralIssueApi" />
	/// </summary>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackGeneralIssueApi : YouTrackApiBase, IYouTrackGeneralIssueApi {
		/// <summary>
		/// 	��������� API
		/// </summary>
		/// <param name="connectionDescriptor"> ������ ������������� ��� ��������� ���������� </param>
		public YouTrackGeneralIssueApi(object connectionDescriptor = null)
			: base(connectionDescriptor) {}


		/// <summary>
		/// 	��������� API ��������� YouTrack
		/// </summary>
		public IYouTrackGeneralIssueApi Issue { get; set; }

		/// <summary>
		/// 	��������� �������� ������ ��������� YouTrack
		/// </summary>
		/// <param name="project"> ID ������� </param>
		/// <param name="summary"> �������� ��������� </param>
		/// <param name="description"> ����� ��������� �������� </param>
		/// <param name="permittedGroup"> ������ �������������, ������� ����� ����� �������� </param>
		/// <remarks>
		/// 	�� API �������� ���������� ����� �������� attachments (���� �� � ����� + �������� �������)
		/// </remarks>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Create+New+Issue" />
		/// </remarks>
		public IssueRef Create(string project, string summary, string description, string permittedGroup) {
			return Execute<IssueRef>(
				"PUT",
				"issue",
				new {project, summary, description, permittedGroup},
				"������� ������� ����� ��������");
		}

		/// <summary>
		/// 	��������� ��������� �� ��� ID
		/// </summary>
		/// <param name="issueId"> ID ��������� </param>
		/// <returns> �������� YouTrack </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Get+an+Issue" />
		/// </remarks>
		public IYouTrackIssue Get(string issueId) {
			return
				Execute<IYouTrackIssue>("GET",
				                        "issue/" + issueId, null,
				                        "������� ����� �������� �� ��� ID");
		}

		/// <summary>
		/// 	��������� �������� ��� ��������� ����������
		/// </summary>
		/// <param name="issueId"> ID ��������� </param>
		/// <param name="command"> ����� ������� </param>
		/// <param name="comment"> ����������� (���� ������� ������, ����������� ������ ����������� � ���������) </param>
		/// <param name="group"> ������ �������������, ������� ����� ����������� </param>
		/// <param name="runAs"> ��� ������������, �� �������� ��������� ������� </param>
		/// <param name="disableNotifications"> ���� true, ��������� �� �������� ���������� </param>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Apply+Command+to+an+Issue" />
		/// </remarks>
		public void ApplyCommand(string issueId, string command, string comment, string group, string runAs,
		                         bool disableNotifications = false) {
			Execute<IgnoreResponse>(
				"POST",
				"issue/" + issueId + "/execute",
				new {command, comment, group, runAs, disableNotifications},
				"������� ��������� �������");
		}

		/// <summary>
		/// 	��������� ����������� � ��������� �� ��� ID
		/// </summary>
		/// <param name="issueId"> ID ��������� </param>
		/// <param name="comment"> ���� ����������� </param>
		public void ApplyComment(string issueId, string comment) {
			Execute<IgnoreResponse>(
				"POST",
				"issue/" + issueId + "/execute",
				new {comment, disableNotifications = true},
				"������� �������� ����������� � ���������");
		}
	}
}