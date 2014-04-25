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
// Original file : YouTrackApiException.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Ошибка при работе с API YouTrack
	/// </summary>
	[Serializable]
	public class YouTrackApiException : Exception {
		/// <summary>
		/// 	Создает новое исключение при работе c API YouTrack
		/// </summary>
		/// <param name="errorInfo"> Информация об ошибке </param>
		/// <param name="request"> Запрос, при выполнении которого произошла ошибка </param>
		/// <param name="innerException"> </param>
		/// <exception cref="NotImplementedException"></exception>
		public YouTrackApiException(string errorInfo, IYouTrackRequest request, Exception innerException)
			: base(errorInfo, innerException) {
			Request = request;
		}

		/// <summary>
		/// </summary>
		/// <param name="errorInfo"> </param>
		public YouTrackApiException(string errorInfo) : base(errorInfo) {}

		/// <summary>
		/// 	Запрос, во время которого возникло исключение
		/// </summary>
		public IYouTrackRequest Request { get; private set; }
	}
}