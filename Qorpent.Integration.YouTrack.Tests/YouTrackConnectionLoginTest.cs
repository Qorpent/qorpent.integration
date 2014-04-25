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
// Original file : YouTrackConnectionLoginTest.cs
// Project: Qorpent.Integration.YouTrack.Tests
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using NUnit.Framework;

namespace Qorpent.Integration.YouTrack.Tests {
	[TestFixture]
	public class YouTrackConnectionLoginTest : YouTrackTestBase {
		[Test]
		public void InvalidPasswordTest() {
			password = "321";
			var connection = GetDefaultConnection();
			Assert.False(connection.TestConnection(false));
			var e = Assert.Throws<YouTrackConnectionException>(() => connection.TestConnection());
			StringAssert.Contains("пароль", e.Message);
		}

		[Test]
		public void InvalidServerTest() {
			server = "http://localhost:5555";
			var connection = GetDefaultConnection();
			Assert.False(connection.TestConnection(false));
			var e = Assert.Throws<YouTrackConnectionException>(() => connection.TestConnection());
			StringAssert.Contains("Сервер", e.Message);
		}

		[Test]
		public void IsConnectionSuccess() {
			var connection = GetDefaultConnection();
			Assert.True(connection.TestConnection());
		}
	}
}