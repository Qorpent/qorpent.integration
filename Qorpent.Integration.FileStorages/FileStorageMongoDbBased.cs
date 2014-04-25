using System.Collections.Generic;
using Qorpent.Integration.FileStorages.FileDescriptors;
using Qorpent.IO;
using Qorpent.Integration.MongoDB;
using Qorpent.MongoDBIntegration;
using System.IO;

namespace Qorpent.Integration.FileStorages {
    /// <summary>
    ///     Хранилище файлов, базированное на MongoDB
    /// </summary>
    public class FileStorageMongoDbBased : IFileStorage {
        /// <summary>
        ///     MongoDB Connector
        /// </summary>
        public IMongoDbConnector MongoDbConnector { get; private set; }
        /// <summary>
        ///     Поддерживаемый функционал хранилища
        /// </summary>
        public FileStorageAbilities Abilities { get; private set; }
        /// <summary>
        ///     Хранилище файлов, базированное на MongoDB
        /// </summary>
        public FileStorageMongoDbBased(IMongoDbConnector mongoDbConnector) {
            Abilities = FileStorageAbilities.Persist;
            MongoDbConnector = mongoDbConnector;
        }
        /// <summary>
        ///     Сохранение файла в хранилище
        /// </summary>
        /// <param name="file">Представление файла</param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public IFile Set(IFileDescriptor file, Stream stream) {
            MongoDbConnector.GridFs.Upload(stream, file.Path);
            return new FileDescriptorMongoDbBased(file, MongoDbConnector);
        }
        /// <summary>
        ///     Получение файла из хранилища
        /// </summary>
        /// <param name="file">Представление файла</param>
        /// <returns>Дескриптор файла</returns>
        public IFile Get(IFileDescriptor file) {
            return new FileDescriptorMongoDbBased(file, MongoDbConnector);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IEnumerable<IFile> EnumerateFiles(FileSearchOptions options = null) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Удаление (реальное) файла из хранилища
        /// </summary>
        /// <param name="file">Представление файла</param>
        public void Del(IFileDescriptor file) {
            MongoDbConnector.GridFs.Delete(file.Path);
        }
        /// <summary>
        ///     Получение низкоуровневого хранилища
        /// </summary>
        /// <returns>Экземпляр класса низкоуровневого хранилища</returns>
        public object GetUnderlinedStorage() {
            return MongoDbConnector;
        }
    }
}
