using System;
using Qorpent.Mvc;

namespace Qorpent.Integration.RuToken.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("rutoken.salt", Role = "DEFAULT", Help = "Возвращает GUID в качестве сессионной соли для работы с сервером")]
    public class RuTokenSaltAction : ActionBase {
        /// <summary>
        ///     Main process
        /// </summary>
        /// <returns>Salt as a GUID string</returns>
        protected override object MainProcess() {
            return Guid.NewGuid().ToString();
        }
    }
}
