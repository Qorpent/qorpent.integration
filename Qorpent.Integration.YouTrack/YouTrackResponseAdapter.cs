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
// Original file : YouTrackResponseAdapter.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Qorpent.IoC;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Стандартная реализация <see cref="IYouTrackResponseAdapter" />
	/// </summary>
	[ContainerComponent(Lifestyle.Transient)]
	public class YouTrackResponseAdapter : ServiceBase, IYouTrackResponseAdapter {
		/// <summary>
		/// 	Конвертирует ответ сервера в модель
		/// </summary>
		/// <param name="response"> Ответ сервера </param>
		/// <typeparam name="T"> </typeparam>
		/// <returns> </returns>
		public T Get<T>(IYouTrackResponse response) where T : class {
			if (typeof (IssueRef) == typeof (T)) {
				return GetIssueRef(response) as T;
			}
			if (typeof (T).FullName.StartsWith("System.Collections.Generic.IEnumerable")) {
				return
					(T)
					GetArray(typeof (T).GetGenericArguments()[0], response,
					         GetValidElements(typeof (T).GetGenericArguments()[0], response));
			}
			else if (typeof (T).IsArray) {
				return (T) GetArray(typeof (T).GetElementType(), response, GetValidElements(typeof (T).GetElementType(), response));
			}
			else {
				var result = ResolveService<T>();
				return response.Content.Apply(result);
			}
		}


		private IssueRef GetIssueRef(IYouTrackResponse response) {
			var locationelement = response.Content.DescendantsAndSelf("location").FirstOrDefault();
			if (null == locationelement) {
				throw new YouTrackApiException("Не могу извлечь ссылку на инцидент из переданного контента -" + response.Content);
			}
			var url = locationelement.Value;
			if (url.IsEmpty()) {
				throw new YouTrackApiException("Не могу извлечь ссылку на инцидент из переданного контента -" + response.Content);
			}
			var result = new IssueRef
				{Url = url, Id = Regex.Match(url, @"issue/(.+)$", RegexOptions.Compiled).Groups[1].Value};
			return result;
		}

		private static IEnumerable<XElement> GetValidElements(Type type, IYouTrackResponse response) {
			if (type == typeof (IYouTrackAssignee)) {
				return GetAssigners(response.Content);
			}
			else if (type == typeof (IYouTrackGroup)) {
				return GetGroups(response.Content);
			}
			return response.Content.Elements();
		}

		private static IEnumerable<XElement> GetGroups(XElement content) {
			if (content.Name.LocalName == "userGroupRefs") {
				return content.Descendants("userGroup");
			}
			return content.Elements();
		}

		private static IEnumerable<XElement> GetAssigners(XElement content) {
			if (content.Name.LocalName == "assigneeList") {
				return content.Descendants("assignee");
			}
			else if (content.Name.LocalName == "userGroupRefs") {
				return content.Descendants("userGroup");
			}
			else if (content.Name.LocalName == "userRefs") {
				return content.Descendants("user");
			}
			return content.Elements();
		}

		private object GetArray(Type type, IYouTrackResponse response, IEnumerable<XElement> xElements) {
			var array = new ArrayList();
			foreach (var element in xElements) {
				object item = Container.Get(type);
				element.Apply(item);
				array.Add(item);
			}
			var result = Array.CreateInstance(type, array.Count);
			for (var i = 0; i < result.Length; i++) {
				((IList) result)[i] = array[i];
			}
			return result;
		}
	}
}