using Qorpent.Mvc;

namespace Qorpent.Integration.RuToken.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("rutoken.builtin", Role = "ADMIN", Arm = "TOKEN")]
    public class RuTokenBuiltInAction : ActionBase {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            return BuiltInTokens.Logins;
        }
    }
}
