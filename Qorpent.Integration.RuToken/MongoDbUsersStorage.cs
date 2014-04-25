using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Qorpent.Integration.MongoDB;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.RuToken {
    /// <summary>
    /// 
    /// </summary>
    public class MongoDbUsersStorage : MongoDbConnector, IRuTokenUsersStorage {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsActivated(string username) {
            var selected = GetConfig(username);

            return (selected != null) && (selected.ToDict()["Activated"].ToBool());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="config"></param>
        public void UpdateConfig(string username, object config) {
            var dict = config.ToDict();

            Collection.Update(
                new QueryDocument(BsonDocument.Parse("{'Username' : '" + username + "'}")),
                new UpdateDocument(BsonDocument.Parse("{$set : {'Activated' : " + ((dict["Activated"].ToString() == "True") ? "true" : "false") + ", 'Comment' : '" + dict["Comment"] + "'}}")),
                UpdateFlags.Multi | UpdateFlags.Upsert
            );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        public object GetConfig(string username) {
            var config = Collection.Find(
                new QueryDocument(
                    BsonDocument.Parse("{'Username' : '" + username + "'}")
                )
            ).FirstOrDefault();

            return config != null ? config.ToDictionary() : null;
        }
    }
}
