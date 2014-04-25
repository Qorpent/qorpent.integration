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
	/// 	��������� API ��������� YouTrack
	/// </summary>
	public interface IYouTrackGeneralIssueApi : IYouTrackApiBase, IYouTrackGeneralApi {
		/// <summary>
		/// 	��������� �������� ������ ��������� YouTrack
		/// </summary>
		/// <param name="project"> ID ������� </param>
		/// <param name="summary"> �������� ��������� </param>
		/// <param name="description"> ����� ��������� �������� </param>
		/// <param name="permittedGroup"> ������ �������������, ������� ����� ����� �������� </param>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Create+New+Issue" />
		/// </remarks>
		IssueRef Create(string project, string summary, string description, string permittedGroup);

		/// <summary>
		/// 	��������� ��������� �� ��� ID
		/// </summary>
		/// <param name="issueId"> ID ��������� </param>
		/// <returns> </returns>
		/// <remarks>
		/// 	<see href="http://confluence.jetbrains.net/display/YTD4/Get+an+Issue" />
		/// </remarks>
		IYouTrackIssue Get(string issueId);

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
		void ApplyCommand(string issueId, string command, string comment="", string group="", string runAs="",
		                  bool disableNotifications = false);

		/// <summary>
		/// 	��������� ����������� � ��������� �� ��� ID
		/// </summary>
		/// <param name="issueId"> ID ��������� </param>
		/// <param name="comment"> ���� ����������� </param>
		void ApplyComment(string issueId, string comment);
	}
}