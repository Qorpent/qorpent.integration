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
// Original file : YouTrackProject.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using Qorpent.IoC;
using Qorpent.Serialization;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	����������� ���������� ������� <see cref="IYouTrackProject" />
	/// </summary>
	[ContainerComponent(Lifestyle.Transient)]
	[Serialize]
	public class YouTrackProject : IYouTrackProject {
		/// <summary>
		/// 	ID �������
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// 	������ �� ������
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 	����� �������� ������� �������
		/// </summary>
		public string Lead { get; set; }

		/// <summary>
		/// 	�������� �������
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 	��� �������
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 	������ �� �������� ��������� ������������
		/// </summary>
		public string AssigneesUrl { get; set; }

		/// <summary>
		/// 	������ �� �������� ��������� ���������
		/// </summary>
		public string SubsystemsUrl { get; set; }

		/// <summary>
		/// 	�������� � �������� ������ TeamCity
		/// </summary>
		public string BuildsUrl { get; set; }

		/// <summary>
		/// 	�������� � �������� ������ �������
		/// </summary>
		public string VersionsUrl { get; set; }
	}
}