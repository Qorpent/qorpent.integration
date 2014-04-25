using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using MongoDB.Bson;
using Qorpent.Integration.MongoDB;

namespace Qorpent.Integration.WikiFsBackup {
    /// <summary>
    /// 
    /// </summary>
    public class WikiFsBackupMaster : MongoDbConnector {
        /// <summary>
        /// 
        /// </summary>
        public string WorkingDirectory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool WithGitControl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Backup() {
            var updateLog = LoadBackupLog();
            var ul = new List<WikiPageIndexElement>();

            foreach (var _ in Collection.FindAll()) {
                ul.Add(new WikiPageIndexElement {
                    Code = _["_id"].ToString(),
                    Ver = _["ver"].ToString(),
                    Editor = _["editor"].ToString()
                });

                if (_.Contains("ver")) {
                    var existing = updateLog.FirstOrDefault(e => e.Code == _["_id"].ToString());

                    if (existing != null) {
                        if (existing.Ver == _["ver"].ToString()) {
                            Debug.Print("Hit");
                            continue;
                        }
                    }
                }

                SaveSingleWikiPage(_);
            }

            SaveBackupLog(ul.OrderBy(_ => _.Code));

            if (WithGitControl) {
                Process.Start(new ProcessStartInfo { FileName = "git", Arguments = "init", WorkingDirectory = WorkingDirectory, CreateNoWindow = true }).WaitForExit();
                Process.Start(new ProcessStartInfo { FileName = "git", Arguments = "add --a", WorkingDirectory = WorkingDirectory, CreateNoWindow = true }).WaitForExit();
                Process.Start(new ProcessStartInfo { FileName = "git", Arguments = "commit -m \"AutoSync\"", WorkingDirectory = WorkingDirectory, CreateNoWindow = true }).WaitForExit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        private void SaveSingleWikiPage(BsonDocument document) {
            var savePath = GenerateSavePath(document);
            Directory.CreateDirectory(savePath.Directory.FullName);
            File.WriteAllText(savePath.FullName, SerializeXml(document).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        private XElement SaveBackupLog(IEnumerable<WikiPageIndexElement> u) {
            var x = new XElement("BackupLog");
            foreach (var i in u) {
                var el = new XElement("item");
                el.SetAttributeValue("code", i.Code);
                el.SetAttributeValue("ver", i.Ver);
                el.SetAttributeValue("editor", i.Editor);
                x.Add(el);
            }

            File.WriteAllText(GenerateBackupLogPath(), x.ToString());

            return x;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<WikiPageIndexElement> LoadBackupLog() {
            var ul = new List<WikiPageIndexElement>();
            if (File.Exists(GenerateBackupLogPath())) {
                foreach (var i in XElement.Parse(File.ReadAllText(GenerateBackupLogPath())).XPathSelectElements("//item")) {
                    ul.Add(new WikiPageIndexElement {
                        Code = i.Attribute("code").Value,
                        Editor = i.Attribute("editor").Value,
                        Ver = i.Attribute("ver").Value
                    });
                }
            }

            return ul;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wikiDocument"></param>
        /// <returns></returns>
        private XElement SerializeXml(BsonDocument wikiDocument) {
            var xml = new XElement("WikiPage");

            foreach (var el in wikiDocument.Elements) {
                xml.SetElementValue(el.Name, el.Value.ToString());
            }

            return xml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateBackupLogPath() {
            return Path.Combine(WorkingDirectory, "Index.Log.Xml");
        }
        /// <summary>
        ///     Собирает путь для сохранения файла 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private FileInfo GenerateSavePath(BsonDocument document) {
            var relativePath = Guid.NewGuid().ToString();

            if (document.Contains("_id")) {
                relativePath = document["_id"].ToString().Replace("/", "\\").Substring(1);
            }

            return new FileInfo(Path.Combine(WorkingDirectory, relativePath + ".xml"));
        }
    }

    internal class WikiPageIndexElement {
        /// <summary>
        ///     Код
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///     Редактор
        /// </summary>
        public string Editor {get; set; }
        /// <summary>
        ///     Версия
        /// </summary>
        public string Ver { get; set; }
    }
}
