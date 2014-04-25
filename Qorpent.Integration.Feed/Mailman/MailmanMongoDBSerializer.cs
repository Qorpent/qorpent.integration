using System;
using MongoDB.Bson;

namespace Qorpent.Integration.Feed.Mailman
{
    /// <summary>
    /// 
    /// </summary>
    public static class MailmanMongoDbSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        public static MailItem TestItem = new MailItem();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        static public MailItem BsonDocumentToMailItem(BsonDocument document)
        {
            var mailitem = new MailItem();
            if (document.Contains("_id"))mailitem.Id = document["_id"].ToString();
            if (document.Contains("text"))mailitem.Text = document["text"].AsString;
            if (document.Contains("time"))mailitem.Time = document["time"].ToLocalTime();
            if (document.Contains("form"))mailitem.FormCode = document["form"].AsString;
            if (document.Contains("year"))mailitem.Year = document["year"].AsInt32;
            if (document.Contains("period"))mailitem.Period = document["period"].AsInt32;
            if (document.Contains("obj"))mailitem.ObjId = document["obj"].AsInt32;
            if (document.Contains("user"))mailitem.User = document["user"].AsString;
            if (document.Contains("type"))mailitem.Type = document["type"].AsString;
            TestItem = mailitem;
            return mailitem;
        }
    }
}
