using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Qorpent.IoC;
using Qorpent.Json;
using Qorpent.Mvc;
using Qorpent.Mvc.Binding;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.RuToken.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("rutoken.edit", Role = "ADMIN", Arm = "TOKEN")]
    public class RuTokenEditAction : RuTokenAuthAction {
        /// <summary>
        /// 
        /// </summary>
        [Bind(Required = true)]public string Config { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            var auth = base.MainProcess().ToDict();


            var salt = auth["Salt"].ToString();

            if (!RuTokenAuthAction.Sessions.Any(_ => _.ToDict()["Salt"].ToString() == salt)) {
                throw new Exception("Salt misatch");
            }

            var user = RuTokenAuthAction.Sessions.FirstOrDefault(_ => _.ToDict()["Salt"].ToString() == salt);
            if (
                user.ToDict()["Role"].ToString() != "ADMIN"
            ) {
                throw new Exception("permission denied");
            }
            
            var config = new JsonParser().ParseXml(Config);

            foreach (var el in config.XPathSelectElements("//item")) {
                var tokenId = el.Attribute("TokenId").TryGetValue();

                if (!BuiltInTokens.TokenExists(tokenId)) {
                    continue;
                }

                var tconfig = BuiltInTokens.GetConfig(tokenId);

                if (tconfig["Role"].ToString() == "ADMIN") {
                    continue;
                }

                var clause = new {
                    Activated = (el.Attribute("Activated").TryGetValue() == "true"),
                    Comment = el.Attribute("Comment").TryGetValue(),
                    Username = BuiltInTokens.InternalUsername(tokenId),
                    FakeUsername = el.Attribute("Username").TryGetValue()
                } as object;

                if (tconfig["Role"].ToString() == "DEV") {
                    clause = new {
                        Activated = (el.Attribute("Activated").TryGetValue() == "true"),
                        Comment = tconfig["Comment"].ToString(),
                        Username = BuiltInTokens.InternalUsername(tokenId),
                        FakeUsername = BuiltInTokens.InternalUsername(tokenId)
                    } as object;
                }

                UsersStorage.UpdateConfig(tokenId, clause);
            }

            return new {Success = true};
        }
    }
}
