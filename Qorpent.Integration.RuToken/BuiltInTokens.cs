using System.Collections.Generic;
using System.Linq;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.RuToken {
    /// <summary>
    /// 
    /// </summary>
    public class BuiltInTokens {
        /// <summary>
        /// 
        /// </summary>
        public static readonly IDictionary<string, object> Logins = new Dictionary<string, object> {
            {"MASTERZeus01", new {DefaultOn = true, Role = "ADMIN", Comment = "Владелец системы", TokenId = "0781737361"}},
            {"DEVELZeus01", new {DefaultOn = true, Role = "DEV", Comment = "DEVELOPER", TokenId = "0781737461"}},
            {"u0781737415", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737415"}},
            {"u0781737416", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737416"}},
            {"u0781737418", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737418"}},
            {"u0781737423", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737423"}},
            {"u0781737612", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737612"}},
            {"u0781737683", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737683"}},
            {"u0781737720", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737720"}},
            {"u0781737724", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781737724"}},
            {"u0781738085", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781738085"}},
            {"u0781738205", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781738205"}},
            {"u0781739057", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781739057"}},
            {"u0781739188", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781739188"}},
            {"u0781739444", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781739444"}},
            {"u0781739892", new {DefaultOn = false, Role = "USER", Comment = "", TokenId = "0781739892"}}
        };
        /// <summary>
        ///     Проверяет наличие токена
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public static bool TokenExists(string tokenId) {
            return Logins.Any(_ => _.Value.ToDict()["TokenId"].ToString() == tokenId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetConfig(string tokenId) {
            if (!TokenExists(tokenId)) {
                return null;
            }

            return Logins.FirstOrDefault(_ => _.Value.ToDict()["TokenId"].ToString() == tokenId).Value.ToDict();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public static string InternalUsername(string tokenId) {
            return Logins.FirstOrDefault(_ => _.Value.ToDict()["TokenId"].ToString() == tokenId).Key;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public static string GetRole(string tokenId) {
            return Logins.FirstOrDefault(_ => _.Value.ToDict()["TokenId"].ToString() == tokenId).Value.ToDict()["Role"].ToString();
        }

    }
}
