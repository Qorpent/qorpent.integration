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
// Original file : YouTrackRequestFactory.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System.Collections.Generic;
using Qorpent.IoC;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Стандартная реализация <see cref="IYouTrackRequestFactory" />
	/// </summary>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackRequestFactory : ServiceBase, IYouTrackRequestFactory {
		/// <summary>
		/// 	Возвращает запрос к API YouTrack
		/// </summary>
		/// <param name="method"> </param>
		/// <param name="commandName"> Комманда </param>
		/// <param name="parameters"> Параметры </param>
		/// <returns> </returns>
		public IYouTrackRequest Get(string method, string commandName, object parameters) {
			IYouTrackRequest result = ResolveService<IYouTrackRequest>() ?? new YouTrackRequest();
			result.Method = method.ToUpper();
			result.Command = commandName;
			if (parameters is IDictionary<string, string>) {
				var dict = (IDictionary<string, string>) parameters;
				foreach (var p in dict) {
					result.Parameters[p.Key] = p.Value;
				}
			}
			else if (parameters is IDictionary<string, object>) {
				var dict = (IDictionary<string, object>) parameters;
				foreach (var p in dict) {
					result.Parameters[p.Key] = p.Value.ToStr();
				}
			}
			else if (null != parameters) {
				foreach (var property in parameters.GetType().GetProperties()) {
					result.Parameters[property.Name] = property.GetValue(parameters, null).ToStr();
				}
			}
			return result;
		}
	}
}