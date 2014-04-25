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
// Original file : YouTrackAdministrationProjectApiTest.cs
// Project: Qorpent.Integration.YouTrack.Tests
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using System.Linq;
using NUnit.Framework;

namespace Qorpent.Integration.YouTrack.Tests {
	[TestFixture]
	public class YouTrackAdministrationProjectApiTest : CertainApiTestBase<IYouTrackAdministrationProjectApi> {
		[Test]
		public void Can_Get_Project() {
			var result = me.Get("T1");
			Assert.AreEqual("T1", result.Id);
			Assert.AreEqual("TEST", result.Name);
			Assert.AreEqual("root", result.Lead);
			Assert.AreEqual("http://localhost:7777/rest/admin/project/T1/assignee", result.AssigneesUrl);
			Assert.AreEqual("http://localhost:7777/rest/admin/project/T1/subsystem", result.SubsystemsUrl);
			Assert.AreEqual("http://localhost:7777/rest/admin/project/T1/build", result.BuildsUrl);
			Assert.AreEqual("http://localhost:7777/rest/admin/project/T1/version", result.VersionsUrl);
			Assert.AreEqual("Project for c# API developing", result.Description);
		}

		[Test]
		public void Can_Get_Project_Assignees() {
			var result = me.GetAssignees("T1");
			Assert.IsNotNull(result);
			Assert.AreEqual("root", result.ElementAt(0).Login);
		}

		[Test]
		public void Can_Get_Project_Assignees_Groups() {
			var result = me.GetAssigneesGroups("T1");
			Assert.IsNotNull(result);
			Assert.AreEqual("All Users", result.ElementAt(0).Name);
		}

		[Test]
		public void Can_Get_Project_From_AdministrationApi() {
			var admapi = Container.Get<IYouTrackAdministrationApi>(null, GetDefaultConnection());
			var result = admapi.Project.Get("T1");
			Assert.AreSame(((YouTrackAdministrationApi) admapi).RequestFactory,
			               ((YouTrackAdministrationProjectApi) admapi.Project).RequestFactory);
			Assert.AreEqual("T1", result.Id);
		}
	}
}