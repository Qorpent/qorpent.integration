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
// Original file : YouTrackAdministrationApi.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using Qorpent.IoC;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Стандартная реализация <see cref="IYouTrackAdministrationApi" />
	/// </summary>
	[ContainerComponent]
	public class YouTrackAdministrationApi : YouTrackApiBase, IYouTrackAdministrationApi {
		/// <summary>
		/// </summary>
		/// <param name="connectionDescriptor"> </param>
		public YouTrackAdministrationApi(object connectionDescriptor = null) : base(connectionDescriptor) {}


		/// <summary>
		/// 	Проект YouTrack
		/// </summary>
		public IYouTrackAdministrationProjectApi Project {
			get {
				return _project ?? (
					                   _project = (
						                              ResolveService<IYouTrackAdministrationProjectApi>(null, this)
						                              ?? new YouTrackAdministrationProjectApi(this)));
			}
			set { _project = value; }
		}

		private IYouTrackAdministrationProjectApi _project;
	}
}