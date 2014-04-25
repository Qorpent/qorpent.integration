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
	/// 	��������� ��������� YouTrack
	/// </summary>
	public interface IYouTrackIssue : IYouTrackModelElement {
		/// <summary>
		/// 	ID ���������
		/// </summary>
		string Id { get; set; }

		/// <summary>
		/// 	ID ��������� � Jira (���� �������� ������������ �� Jira)
		/// </summary>
		string JiraId { get; set; }

		/// <summary>
		/// 	�������� ��� ������� � ������� ������ ��������
		/// </summary>
		string ProjectShortName { get; set; }

		/// <summary>
		/// 	���������� ����� ��������� � �������
		/// </summary>
		string NumberInProject { get; set; }

		/// <summary>
		/// 	������� �������� ���������
		/// </summary>
		string Summary { get; set; }

		/// <summary>
		/// 	������ �������� ���������
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// 	����� �������� ��������� (���-�� ����������� � 1 ������ 1970 ����)
		/// </summary>
		long Created { get; set; }

		/// <summary>
		/// 	����� ���������� ���������� ��������� (���-�� ����������� � 1 ������ 1970 ����)
		/// </summary>
		long Updated { get; set; }

		/// <summary>
		/// 	����� ������������ ������� ���� ��������� ��������� � ��������
		/// </summary>
		string UpdaterName { get; set; }

		/// <summary>
		/// 	����� �������� ��������� (���� �� ������ ������)
		/// </summary>
		long Resolved { get; set; }

		/// <summary>
		/// 	����� ������������, ���������� ��������
		/// </summary>
		string ReporterName { get; set; }

		/// <summary>
		/// 	����������� ������������ � ���������
		/// </summary>
		int CommentsCount { get; set; }

		/// <summary>
		/// 	���������� ������� � ����������
		/// </summary>
		int Votes { get; set; }

		/// <summary>
		/// 	������ �������������, ������� ����� ��������
		/// </summary>
		string PermittedGroup { get; set; }

		/// <summary>
		/// 	����������� � ���������
		/// </summary>
		IYouTrackComment[] Comment { get; set; }

		/// <summary>
		/// 	���� ���������
		/// </summary>
		IYouTrackField[] Field { get; set; }

		/// <summary>
		/// 	���� ���������
		/// </summary>
		IYouTrackTag[] Tag { get; set; }
	}
}