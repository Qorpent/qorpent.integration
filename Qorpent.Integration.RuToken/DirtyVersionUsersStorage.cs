using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using Qorpent.IO;
using Qorpent.IO.DirtyVersion;
using Qorpent.IoC;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.RuToken {
    /// <summary>
    /// 
    /// </summary>
    public class DirtyVersionUsersStorage : IRuTokenUsersStorage {
        private IDirtyVersionStorage _dirtyVersionStorage;

        /// <summary>
        /// 
        /// </summary>
        public string TargetDirectory = "~/.TokenUsers";
        /// <summary>
        /// 
        /// </summary>
        public IDirtyVersionStorage DirtyVersionStorage {
            get {
                return _dirtyVersionStorage ??
                       (_dirtyVersionStorage = new DirtyVersionStorage(Path.Combine(new FileNameResolver().Root, ".TokenStorage")));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsActivated(string tokenid = null, string username = null) {
            var config = GetConfig(tokenid, username);
            return config != null && config.ToDict()["Activated"].To<bool>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenid"></param>
        /// <param name="config"></param>
        public void UpdateConfig(string tokenid, object config) {
            var dict = config.ToDict();
            var map = GetMap() ?? new XElement("Map");
            var itm = map.XPathSelectElement("//i[@TokenId='" + tokenid + "']");

            if (itm == null) {
                itm = new XElement("i");
                map.Add(itm);
            }

            itm.SetAttributeValue("TokenId", tokenid);

            if (dict.ContainsKey("Activated")) itm.SetAttributeValue("Activated", dict["Activated"]);
            if (dict.ContainsKey("Comment")) itm.SetAttributeValue("Comment", dict["Comment"]);
            if (dict.ContainsKey("Username")) itm.SetAttributeValue("Username", dict["Username"]);
            if (dict.ContainsKey("FakeUsername")) itm.SetAttributeValue("FakeUsername", dict["FakeUsername"]);

            SetMap(map);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public object GetConfig(string tokenid = null, string username = null) {
            var xml = GetMap();
            var config = xml.XPathSelectElement("//i[@" + (tokenid != null ? "TokenId='" + tokenid + "'" : "Username='" + username + "'") + "]");

            if (config != null) {
                return new {
                    Activated = config.Attribute("Activated").TryGetValue().To<bool>(),
                    Comment = config.Attribute("Comment").TryGetValue().To<string>(),
                    TokenId = config.Attribute("TokenId").TryGetValue().To<string>(),
                    Username = config.Attribute("Username").TryGetValue().To<string>(),
                    FakeUsername = config.Attribute("FakeUsername").TryGetValue().To<string>()
                };
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private XElement GetMap() {
            try {
                return XElement.Parse(new StreamReader(DirtyVersionStorage.Open("mapper.xml")).ReadToEnd());
            } catch {
                return new XElement("Map");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        private void SetMap(XElement map) {
            DirtyVersionStorage.Save("mapper.xml", map.ToString());
        }
    }
}
