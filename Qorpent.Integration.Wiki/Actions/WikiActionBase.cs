using Qorpent.IoC;
using Qorpent.Mvc;
using Qorpent.Wiki;

namespace Qorpent.Integration.Wiki.Actions {
    /// <summary>
    /// 
    /// </summary>
    public class WikiActionBase : ActionBase {
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        public IWikiSource WikiGeneralProvider { get; set; }
    }
}
