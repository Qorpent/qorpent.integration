using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Qorpent.Data;

namespace Qorpent.Integration.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDbDocumentStorage : ServiceBase,IDocumentStorage
    {
        private IMongoDbConnector _connector;

        /// <summary>
        /// 
        /// </summary>
        public MongoDbDocumentStorage() {
            ConnectorName = "mongo.connector";
        }
        
        /// <summary>
        /// Имя коннектора
        /// </summary>
        public string ConnectorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IMongoDbConnector Connector
        {
            get { return _connector ?? (ResolveService<IMongoDbConnector>(ConnectorName)); }
            set { _connector = value; }
        }
        /// <summary>
        /// Выполнить запрос
        /// </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public XElement ExecuteQuery(string query, DocumentStorageOptions options = null) {
            var cursor = Connector.Collection.Find(new QueryDocument(BsonDocument.Parse(query)));
            if (null != options && null != options.Fields) {
               cursor = cursor.SetFields(options.Fields);
            }
            if (0 != options.Limit) {
                cursor = cursor.SetLimit(options.Limit);
            }
            var result = new XElement("result");
            foreach (var doc in cursor) {
                result.Add(ConvertToXElement(doc,"doc"));
            }
            return result;
        }

        private XElement ConvertToXElement(BsonDocument doc, string name) {
            var result = new XElement(name);
            foreach (var e in doc.Elements) {
                var key = e.Name;
                if (e.Value is BsonDocument) {
                    result.Add(ConvertToXElement(e.Value as BsonDocument, key));
                }
                else {
                    result.SetAttributeValue(key,e.Value.ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// Установить контекст работы
        /// </summary>
        /// <param name="database"></param>
        /// <param name="collection"></param>
        public IDocumentStorage SetContext(string database, string collection)
        {
            Connector = ResolveService<IMongoDbConnector>(ConnectorName);
            Connector.DatabaseName = database;
            Connector.CollectionName = collection;
            return this;
        }
    }
}
