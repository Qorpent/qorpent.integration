using System;
using Qorpent.Integration.MongoDB.DirectQueries;
using Qorpent.MongoDBIntegration.DirectQueries;
using Qorpent.Mvc;
using Qorpent.Mvc.Binding;

namespace Qorpent.Integration.MongoDB.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("db.mongo", Role = "DEVELOPER", Arm = "dev")]
    public class MongoDbDirectQueryAction : ActionBase {
        /// <summary>
        ///     Строка подключения к MongoDB
        /// </summary>
        [Bind] public string ConnectionString;

        /// <summary>
        ///     Имя базы данных
        /// </summary>
        [Bind] public string DatabaseName;

        /// <summary>
        ///     Имя коллекции
        /// </summary>
        [Bind] public string CollectionName;

        /// <summary>
        ///     Запрос к базе вида #Q-65
        /// </summary>
        [Bind(Required = true)] public string Query;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            var directQuery = GetDirectQueryInstance();
            if (null == directQuery) {
                throw new Exception("Need an IDirectQuery instance");
            }

            if (!string.IsNullOrWhiteSpace(ConnectionString)) {
                directQuery.ConnectionString = ConnectionString;
            }

            if (!string.IsNullOrWhiteSpace(DatabaseName)) {
                directQuery.DatabaseName = DatabaseName;
            }

            if (!string.IsNullOrWhiteSpace(CollectionName)) {
                directQuery.CollectionName = CollectionName;
            }

            return directQuery.Query(Query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDirectQuery GetDirectQueryInstance() {
            return Container.Get<IDirectQuery>() ?? new DirectQuery();
        }
    }
}