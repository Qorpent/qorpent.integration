using MongoDB.Bson;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.MongoDB {
	/// <summary>
	/// 
	/// </summary>
	public static class MongoDbConnectorExtension {
		/// <summary>
		/// Выполняет логику сохранения по умолчанию
		/// </summary>
		/// <param name="connector"></param>
		/// <param name="item"></param>
		/// <param name="idname"></param>
		public static void DefaultStoreElement(this IMongoDbConnector connector, object item, string idname="_id") {
			DefaultStoreElement(connector, connector.DatabaseName, item, idname);
		}

		/// <summary>
		/// Выполняет логику сохранения по умолчанию
		/// </summary>
		/// <param name="connector"></param>
		/// <param name="database"></param>
		/// <param name="item"></param>
		/// <param name="idname"></param>
		public static void DefaultStoreElement(this IMongoDbConnector connector, string database, object item, string idname="_id") {
			DefaultStoreElement(connector, database, connector.CollectionName, item, idname);
		}

		/// <summary>
		/// Выполняет логику сохранения по умолчанию
		/// </summary>
		/// <param name="connector"></param>
		/// <param name="database"></param>
		/// <param name="collection"></param>
		/// <param name="item"></param>
		/// <param name="idname"></param>
		public static string DefaultStoreElement(this IMongoDbConnector connector, string database, string collection, object item, string idname = "_id") {
			var db = connector.Client.GetServer().GetDatabase(database);
			var col = db.GetCollection(collection);
			var dict = item.ToDict();
			if (dict.ContainsKey(idname) && idname!="_id") {
				dict["_id"] = dict[idname];
			}
			
			if (string.IsNullOrWhiteSpace(dict["_id"].ToStr())) {
				dict.Remove("_id");
			}

			var doc = new BsonDocument(dict);
			col.Save(doc);
			return doc["_id"].ToString();
		}
	}
}