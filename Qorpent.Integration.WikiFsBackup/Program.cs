using System;

namespace Qorpent.Integration.WikiFsBackup {
    /// <summary>
    /// 
    /// </summary>
    public static class Program {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            if (args.Length != 5) {
                Console.WriteLine(String.Format("Use:  DB_NAME COLLECTION_NAME CONNECTION_STRING SAVE_DIRECTORY WITH_GIT_CONTROL[0/1]"));
            } else {
                var bm = new WikiFsBackupMaster {
                    CollectionName = args[1],
                    DatabaseName = args[0],
                    ConnectionString = args[2],
                    WorkingDirectory = args[3],
                    WithGitControl = args[4].Contains("1")
                };
                try {
                    bm.Backup();
                    Console.WriteLine("Успешно завершено");
                } catch (Exception) {
                    Console.WriteLine("Завершено с ошибками");
                }

            }
        }
    }
}
