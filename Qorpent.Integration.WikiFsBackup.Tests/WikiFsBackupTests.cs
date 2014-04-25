using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Qorpent.Integration.WikiFsBackup.Tests {
    public class WikiFsBackupTests {
        [Explicit]
        [Test]
        public void CanSave() {
            var wikiFsBackupMaster = new WikiFsBackupMaster {
                CollectionName = "main",
                DatabaseName = "wiki",
                ConnectionString = "mongodb://ugmk-as2:28018,ugmk-econ:28018",
                WorkingDirectory = "D:\\Work\\WikiBackup"
            };

            wikiFsBackupMaster.Backup();
        }
        [Test]
        public void CanGeneratePath() {
            var g = ("/test/ok".Replace("/", "\\") + ".xml").Substring(1);
            Console.Write(g);
            var path = Path.Combine("D:\\Work\\WikiBackup", g);
            Assert.AreEqual("D:\\Work\\WikiBackup\\test\\ok.xml", path);
        }
    }
}
