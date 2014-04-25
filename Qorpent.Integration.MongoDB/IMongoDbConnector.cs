using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Qorpent.Integration.MongoDB {
    /// <summary>
    /// 
    /// </summary>
    public interface IMongoDbConnector {
        /// <summary>
        ///     The database name you want to use to store attachements
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        ///     connection string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        ///     Collection name
        /// </summary>
        string CollectionName { get; set; }

        /// <summary>
        ///     MongoDB database setting
        /// </summary>
        MongoDatabaseSettings DatabaseSettings { get; }

        /// <summary>
        ///     MongoDB GridFS settings
        /// </summary>
        MongoGridFSSettings GridFsSettings { get; }

        /// <summary>
        ///     MongoDB Client
        /// </summary>
        MongoClient Client { get; }

        /// <summary>
        ///     MongoDB Server
        /// </summary>
        MongoServer Server { get; }

        /// <summary>
        ///     MongoDB Database connection link
        /// </summary>
        MongoDatabase Database { get; }

        /// <summary>
        ///     MongoDB Collection link
        /// </summary>
        MongoCollection<BsonDocument> Collection { get; }

        /// <summary>
        ///     MongoDB GridFS connection link
        /// </summary>
        MongoGridFS GridFs { get; }


	    
    }
}