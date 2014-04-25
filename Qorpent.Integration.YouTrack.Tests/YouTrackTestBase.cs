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
// Original file : YouTrackTestBase.cs
// Project: Qorpent.Integration.YouTrack.Tests
// 
// ALL MODIFICATIONS MADE TO FILE MUST BE DOCUMENTED IN SVN

#endregion

using NUnit.Framework;
using Qorpent.IoC;

namespace Qorpent.Integration.YouTrack.Tests {
	public abstract class YouTrackTestBase {
		[SetUp]
		public virtual void Setup() {
			server = "http://localhost:7777";
			user = "root";
			password = "90e9992c";
			Container = ContainerFactory.CreateEmpty();
			Container.RegisterAssembly(typeof (IYouTrackConnection).Assembly);
		}

		public IYouTrackConnection GetDefaultConnection() {
			return Container.Get<IYouTrackConnection>(null, server, user, password);
		}

		public T GetApi<T>() where T : class, IYouTrackApiBase {
			return Container.Get<T>(null, GetDefaultConnection());
		}

		protected IContainer Container;
		public string password;

		public string server;

		public string user;
	}
}