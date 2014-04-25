using System;
using System.Globalization;
using System.Net;
using NUnit.Framework;
using MongoDB.Driver;
using MongoDB.Bson;
using Qorpent.Integration.Feed.Mailman;
using System.Net.Mail;

namespace Qorpent.Integration.Feed.Tests.Mailman
{
    [TestFixture]
    internal class MailmanSendFromBsonDocTests
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("LOCALMONGOCS", EnvironmentVariableTarget.Machine) ?? "mongodb://localhost";
        private const string BaseName = "QorpentIntegrationTests";
        private const string CollectionName = "MailmanSendFromBsonDocTestsTestCollection";
        private DateTime _dateTime = DateTime.Now;
        private ObjectId id = ObjectId.GenerateNewId();

        public void CleanBase()
        {
            var client = new MongoClient(_connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(BaseName);
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
            document.Set("_id", id);
            document.Set("form", "2");
            document.Set("year", 2013);
            document.Set("period", 3);
            document.Set("obj", 123);
            document.Set("user", "TestUser");
            document.Set("text", "TestText");
            document.Set("time", _dateTime);
            document.Set("type", "admin");
            collection.Insert(document);
            MailmanMongoDbSerializer.BsonDocumentToMailItem(document);
            
        }


        [Test]
        public void CanSend()
        {
            WriteToBsonDocument();
            MailmanSender.SendMessage();
        }
    }
}

