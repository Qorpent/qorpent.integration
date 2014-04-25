using System;
using NUnit.Framework;
using Qorpent.Integration.MongoDB.DirectQueries;

namespace Qorpent.Integration.MongoDB.Tests.DirectQueries {
	[TestFixture]
    public class DirectQueryTests {
        [Test]
        public void CanUseDirectQuery() {
            var dq = new DirectQuery {
                CollectionName = "DirectQueryTests",
                ConnectionString = Environment.GetEnvironmentVariable("LOCALMONGOCS", EnvironmentVariableTarget.Machine) ?? "mongodb://localhost",
                DatabaseName = "QorpentIntegrationTests"
            };

            dq.Database.Drop();

            dq.Query("insert({test : true})");
            var found = dq.Query("find({test : true})");
            Assert.IsTrue(found.Contains("\"test\" : true"));

            dq.Query("update({test : true}, {$set : {h : 1}})");
            found = dq.Query("find({h : 1})");
            Assert.IsTrue(found.Contains("\"test\" : true"));
            Assert.IsTrue(found.Contains("\"h\" : 1"));
        }
    }
}
