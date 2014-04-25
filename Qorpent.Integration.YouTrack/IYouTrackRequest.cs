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
// Original file : IYouTrackRequest.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System.Collections.Generic;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	��������� ������� REST API YouTrack (������� ������� YouTrack RestApi)
	/// </summary>
	public interface IYouTrackRequest {
		/// <summary>
		/// 	HTTP ����� �������
		/// </summary>
		string Method { get; set; }

		/// <summary>
		/// 	������� YouTrack (����� ���� ����� /rest � �� ������ ?)
		/// </summary>
		string Command { get; set; }

		/// <summary>
		/// 	��������� �������
		/// </summary>
		IDictionary<string, string> Parameters { get; }
	}
}