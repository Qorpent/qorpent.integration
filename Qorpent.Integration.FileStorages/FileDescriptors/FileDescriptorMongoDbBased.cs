using Qorpent.Integration.MongoDB;
using Qorpent.IO;
using System.IO;

namespace Qorpent.Integration.FileStorages.FileDescriptors {
    /// <summary>
    ///     Дескриптор файла, базированный на MongoDB
    /// </summary>
    public class FileDescriptorMongoDbBased : IFile {
        /// <summary>
        ///     Внутренний экземпляр коннектора
        /// </summary>
        private readonly IMongoDbConnector _mongoDbConnector;
        /// <summary>
        ///     Сущость файла
        /// </summary>
        public IFileDescriptor Descriptor { get; private set; }
        /// <summary>
        ///     Дескриптор файла, базированный на MongoDB
        /// </summary>
        /// <param name="fileDescriptor">Представление файла</param>
        /// <param name="mongoDbConnector">Настроенный коннектор к базе</param>
        public FileDescriptorMongoDbBased(IFileDescriptor fileDescriptor, IMongoDbConnector mongoDbConnector) {
            Descriptor = fileDescriptor;
            _mongoDbConnector = mongoDbConnector;
        }
        /// <summary>
        ///     Получение потока до файла
        /// </summary>
        /// <param name="access">Параметер доступа</param>
        /// <returns></returns>
        public Stream GetStream(FileAccess access) {
            return _mongoDbConnector.GridFs.Exists(Descriptor.Path) ? _mongoDbConnector.GridFs.OpenRead(Descriptor.Path) : null;
        }
    }
}
