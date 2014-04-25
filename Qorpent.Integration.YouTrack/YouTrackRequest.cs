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
// Original file : YouTrackRequest.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System.Collections.Generic;
using Qorpent.IoC;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Стандартная реализация <see cref="IYouTrackRequest" />
	/// </summary>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackRequest : IYouTrackRequest {
		/// <summary>
		/// </summary>
		public YouTrackRequest() {
			Parameters = new Dictionary<string, string>();
		}


		/// <summary>
		/// 	HTTP метод запроса
		/// </summary>
		public string Method { get; set; }

		/// <summary>
		/// 	Команда YouTrack (часть пути после /rest и до начала ?)
		/// </summary>
		public string Command { get; set; }

		/// <summary>
		/// 	Параметры команда
		/// </summary>
		public IDictionary<string, string> Parameters { get; private set; }
	}
}