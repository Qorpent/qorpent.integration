using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Qorpent.Integration.Feed.Tests.Mailman
{
    [TestFixture]
    class MailmanMessageTest
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("LOCALMONGOCS", EnvironmentVariableTarget.Machine) ?? "mongodb://localhost";
        private const string BaseName = "QorpentIntegrationTests";
        private const string CollectionName = "MailmanMessageTestTestCollection";

        public void CleanBase() {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(BaseName);
            var collection = database.GetCollection(CollectionName);
            var document = new BsonDocument();
            database.DropCollection(CollectionName);
        }

        public void WriteToBsonDocument()
        {
            CleanBase();
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(BaseName);
            var collection = database.GetCollection(CollectionName);
            var document = new BsonDocument();
            document.Set("form", "2");
            document.Set("year", 2013);
            document.Set("period", 3);
            document.Set("obj", 123);
            document.Set("user", "TestUser");
            document.Set("text", "TestText");
            document.Set("time", "2013-08-02T09:10:34.842Z");
            document.Set("type", "admin");
            collection.Insert(document);
        }

        [Test]
        public void ReadFromMongoTest()
        {
            WriteToBsonDocument();
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(BaseName);
            var collection = database.GetCollection(CollectionName);
            var document = collection.FindAll().FirstOrDefault();
            Assert.AreEqual(2013, document["year"].AsInt32);
            Assert.AreEqual(3, document["period"].AsInt32);
            Assert.AreEqual(123, document["obj"].AsInt32);
            Assert.AreEqual("2", document["form"].AsString);
            Assert.AreEqual("TestUser", document["user"].AsString);
            Assert.AreEqual("2013-08-02T09:10:34.842Z", document["time"].AsString);
            Assert.AreEqual("admin", document["type"].AsString);
        }

    }
}
