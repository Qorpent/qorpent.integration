using System.Linq;
using Qorpent.IoC;
using Qorpent.Mvc;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.RuToken.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("rutoken.getusers", Role = "ADMIN", Arm = "TOKEN", Help = "Возвращает актуальные конфиги юзеров")]
    public class RuTokenGetUsers : ActionBase {
        /// <summary>
        /// 
        /// </summary>
        [Inject(Name = "RuToken.Storage")]
        public IRuTokenUsersStorage UsersStorage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            return BuiltInTokens.Logins.Values.Select(
                _ => UsersStorage.GetConfig(tokenid:_.ToDict()["TokenId"].ToString())
            ).Where(
                _ => _ != null
            );
        }
    }
}
