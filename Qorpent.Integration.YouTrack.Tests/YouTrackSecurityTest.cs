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
// Original file : YouTrackSecurityTest.cs
// Project: Qorpent.Integration.YouTrack.Tests
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using NUnit.Framework;

namespace Qorpent.Integration.YouTrack.Tests {
	[TestFixture]
	public class YouTrackSecurityTest : YouTrackTestBase {
		[Test]
		public void ValidErrorOnDenied() {
			var connection = GetDefaultConnection();
			var api = Container.Get<IYouTrackAdministrationProjectApi>(null, connection);
			api.Get("T1");

			user = "test";
			password = "123";
			connection = GetDefaultConnection();
			api = Container.Get<IYouTrackAdministrationProjectApi>(null, connection);
			Assert.True(connection.TestConnection());

			//Создаем заведомо недоступное API

			var error = Assert.Throws<YouTrackApiException>(() => api.Get("T1"));
			StringAssert.Contains("полномоч", error.InnerException.Message);
		}
	}
}