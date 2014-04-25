using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Qorpent.MongoDBIntegration.DirectQueries;

namespace Qorpent.Integration.MongoDB.DirectQueries {
    /// <summary>
    /// 
    /// </summary>
    public class DirectQuery : MongoDbConnector, IDirectQuery {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string Query(string query) {
            var parsed = ParseQuery(query);

            switch (CheckQueryMethod(query)) {
                case "update":
                    Collection.Update(
                        new QueryDocument(
                            (!(parsed.Count < 1)) ? (parsed[0].ToBsonDocument()) : (new BsonDocument())
                        ),

                        new UpdateDocument(
                            (!(parsed.Count < 2)) ? (parsed[1].ToBsonDocument()) : (new BsonDocument())
                        ),

                        GetUpdateFlags((!(parsed.Count < 3)) ? (parsed[2].ToBsonDocument()) : (new BsonDocument()))
                    );

                    return QueryDone();
                case "insert":
                    Collection.Insert(
                        new QueryDocument(
                            parsed[0].AsBsonDocument
                        )
                    );

                    return QueryDone();
                case "remove":
                    Collection.Remove(
                        new QueryDocument(
                            parsed[0].AsBsonDocument
                        )
                    );

                    return QueryDone();
                case "find":
                    return Collection.Find(
                        new QueryDocument(
                            parsed[0].AsBsonDocument
                        )
                    ).ToJson();
            }

            return "{exception : \"Can not resolve the method of using the database\"}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private UpdateFlags GetUpdateFlags(BsonDocument document) {
            var flags = UpdateFlags.None;

            if (document.Contains("upsert")) {
                flags = flags | ((document["upsert"].ToBoolean()) ? (UpdateFlags.Upsert) : (UpdateFlags.None));
            }

            if (document.Contains("multi")) {
                flags = flags | ((document["multi"].ToBoolean()) ? (UpdateFlags.Upsert) : (UpdateFlags.None));
            }

            return flags;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string CheckQueryMethod(string query) {
            return query.Substring(0, query.IndexOf("({", System.StringComparison.Ordinal));
        }

        private string QueryDone() {
            return "{ok : true}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private BsonArray ParseQuery(string query) {
            var document = BsonDocument.Parse(
                "{query : [" +
                query.Substring(
                    query.IndexOf("({", System.StringComparison.Ordinal) + 1,
                    query.Length - 2 - query.IndexOf("({", System.StringComparison.Ordinal)
                ) +
                "]}"
            );

            return document["query"].AsBsonArray;
        }
    }
}
