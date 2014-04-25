using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Qorpent.Integration.MongoDB {
    /// <summary>
    ///     Набор расширений для работы с драйвером MongoDB
    /// </summary>
    public static class MongoDbExtensions {
        /// <summary>
        ///     Исключение полей из выборки
        /// </summary>
        /// <typeparam name="T">Типизация курсора</typeparam>
        /// <param name="mongoCursor">Курсор MongoDB</param>
        /// <param name="fields">Перечисление полей для исключение</param>
        /// <returns>Замыкание на курсор mongo</returns>
        public static MongoCursor<T> ExceptFields<T>(this MongoCursor<T> mongoCursor, params string[] fields) {
            foreach (var field in fields) {
                mongoCursor.SetFields(new FieldsDocument(field, false));
            }
            return mongoCursor;
        }
        /// <summary>
        ///     Обновление записи в MongoDB по её ObjectId
        /// </summary>
        /// <param name="collection">Исходная коллекция MongoDB</param>
        /// <param name="id">_id</param>
        /// <param name="document">Документ для обновления</param>
        /// <param name="flags">Флаги</param>
        public static void UpdateById(this MongoCollection collection, string id, BsonDocument document, UpdateFlags flags = UpdateFlags.None) {
            collection.Update(new QueryDocument("_id", id), new UpdateDocument(document.Merge(new BsonDocument("_id", id))), flags);
        }
        /// <summary>
        ///     Поиск записи в коллекции MongoDB по _id
        /// </summary>
        /// <param name="collection">Исходная коллекция MongoDB</param>
        /// <param name="id">_id</param>
        /// <returns>Документ</returns>
        public static BsonDocument FindById(this MongoCollection collection, string id) {
            return collection.FindOneAs<BsonDocument>(new QueryDocument("_id", id));
        }
        /// <summary>
        ///     $set
        /// </summary>
        /// <param name="updateDocument">Исходный документ обновления</param>
        /// <param name="name">Имя поля для $set</param>
        /// <param name="value">Значение поля для $set</param>
        /// <returns>Замыкание на исходный документ</returns>
        public static UpdateDocument MongoSet(this UpdateDocument updateDocument, string name, BsonValue value) {
            updateDocument.AddRange(new BsonDocument("$set", new BsonDocument(name, value)));
            return updateDocument;
        }
        /// <summary>
        ///     Поиск записей по перечислению <see cref="ObjectId"/>
        /// </summary>
        /// <typeparam name="T">Типизация объекта мапинга</typeparam>
        /// <param name="mongoCollection">Исходная коллекция MongoDB</param>
        /// <param name="objectIds">Перечисление <see cref="ObjectId"/> для поиска</param>
        /// <returns>Перечисление докуменов</returns>
        public static IEnumerable<T> FindByIdAs<T>(this MongoCollection mongoCollection, IEnumerable<ObjectId> objectIds) {
            return objectIds.Select(_ => mongoCollection.FindOneByIdAs<T>(_));
        }
    }
}
