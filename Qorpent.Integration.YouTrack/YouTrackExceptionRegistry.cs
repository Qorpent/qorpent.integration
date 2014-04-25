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
// Original file : YouTrackExceptionRegistry.cs
// Project: Qorpent.Integration.YouTrack
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System;
using System.Text.RegularExpressions;
using Qorpent.IoC;
using Qorpent.Log;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.YouTrack {
	/// <summary>
	/// 	Регистратор ошибок YouTrack
	/// </summary>
	[ContainerComponent(Lifestyle.Transient, Name = "youtrack.exception.registry")]
	public class YouTrackExceptionRegistry : ServiceBase, IExceptionRegistry {
		/// <summary>
		/// 	Конструктор
		/// </summary>
		public YouTrackExceptionRegistry() {
			ConnectionName = "Default";
			ProjectName = "Exreg";
		}

		/// <summary>
		/// 	Имя соединения (если null, то берется имя по-умолчанию "default")
		/// </summary>
		public string ConnectionName { get; set; }

		/// <summary>
		/// 	Имя проекта (если null, то берется имя по-умолчанию "Exreg")
		/// </summary>
		public string ProjectName { get; set; }

		/// <summary>
		/// 	Отпавляет сообщение с ошибкой в YouTrack
		/// </summary>
		/// <param name="error"> Ошибка </param>
		/// <param name="severity"> Уровень ошибки </param>
		/// <param name="title"> Заголовок ошибки </param>
		/// <param name="advancedParameters"> Дополнительные параметры </param>
		/// <returns> Уникальный ключ сообщения </returns>
		public string Send(Exception error, ErrorLevel severity, object advancedParameters, string title = "") {
			var myapi = ResolveService<IYouTrackGeneralIssueApi>(null, ConnectionName);
			var message = Regex.Replace(error.ToString(), @"(([\s\S]{60}[\s\S]*?(?<!line)\s)|(\sin\s))",
			                            "$0" + Environment.NewLine,
			                            RegexOptions.Compiled);
			if(message.Length > 2300 ) {
				message = message.Substring(0, 2300);
			}

			var desc = "{code}" + message+ "{code}";
			var result = myapi.Create(ProjectName, title + ": " + error.Message, desc, "");
			var priority = "Normal";
			if (severity.Equals(ErrorLevel.Error)) {
				priority = "Major";
			}
			else if (severity.Equals(ErrorLevel.Fatal)) {
				priority = "Critical";
			}
			myapi.ApplyCommand(result.Id, "Priority " + priority, disableNotifications: true);
			var comment = "";
			foreach (var p in advancedParameters.ToDict()) {
				var val = p.Value.ToStr();
				if (val.IsNotEmpty()) {
					bool trycomand = true;
					if(p.Key=="fullurl") {
						if(val.Length<=50)continue;
						trycomand = false;
					}
					try {
						if(trycomand) {
							myapi.ApplyCommand(result.Id, p.Key + " " + p.Value, "", "All Users");
						}else {
							comment += p.Key + ": " + p.Value + "\n\n";
						}
					}
					catch (YouTrackApiException) {
						comment += p.Key + ": " + p.Value + "\n\n";
					}
				}
			}
			myapi.ApplyComment(result.Id, comment);
			return result.Url;
		}
	}
}