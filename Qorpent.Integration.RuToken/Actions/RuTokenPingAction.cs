using System.Collections.Generic;
using System.Linq;
using Qorpent.Mvc;

namespace Qorpent.Integration.RuToken.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("rutoken.ping", Role = "DEFAULT", Arm = "TOKEN")]
    public class RuTokenPingAction : ActionBase {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            if (
                RuTokenAuthAction.LastAccess.Any(
                    _ => _.Key == User.Identity.Name
                )
            ) {
                RuTokenAuthAction.LastAccess.Remove(
                    RuTokenAuthAction.LastAccess.FirstOrDefault(
                        _ => _.Key == User.Identity.Name
                    )
                );
            }

            RuTokenAuthAction.LastAccess.Add(
                new KeyValuePair<string, long>(
                    User.Identity.Name,
                    RuTokenAuthAction.CurrentTimestamp()
                )
            );

            return new {Pong = true};
        }
    }
}
