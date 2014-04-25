using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
﻿using Qorpent.Integration.MongoDB;

namespace Qorpent.Integration.Feed.Mailman {
    /// <summary>
    ///     Источник данных из MongoDB для подсистемы рассылки сообщений
    /// </summary>
    public class MailmanMongoDbSource : MongoDbConnector, IMailManSource {
        /// <summary>
        /// Выбирает сообщения которые не имеют отметки о отправке
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MailItem> GetOutbox()
        {
            var found = Collection.Find(new QueryDocument(
                BsonDocument.Parse("{haveSend : {$not : {$in : [true]}}")
                ));
            foreach (var item in found)
            {
                yield return MailmanMongoDbSerializer.BsonDocumentToMailItem(item);
            }
        }

        /// <summary>
        /// Метод ставит отметку о том, что сообщение было отправлено в саппорт 
        /// </summary>
        /// <param name="mailItem"></param>
        public void MarkAsSend(MailItem mailItem)
        {
            Collection.Update(new QueryDocument(BsonDocument.Parse("{_id : "+mailItem.Id+"}")), new UpdateDocument(BsonDocument.Parse("{$set : { haveSended : true }")));
        }
    }
}
