using Qorpent;
using Qorpent.Mvc;
using Qorpent.Mvc.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Qorpent.Mvc.Renders;
using Qorpent.Utils.Extensions;
using Qorpent.Wiki;

namespace Qorpent.Integration.Wiki.Actions {
    /// <summary>
    /// 
    /// </summary>
    [Action("wikigp.get", Role = "DEFAULT")]
    public class GetWikiPageAction : WikiActionBase {
        /// <summary>
        /// 
        /// </summary>
        [Bind(Required = false)]
        protected string PageVersion;
        /// <summary>
        /// Код или коды страниц, которые требуется получить
        /// </summary>
        [Bind(Required = true)]
        public string Code;
        /// <summary>
        /// Вариант использования
        /// </summary>
        [Bind(Default = "default")]
        public string Usage;
        /// <summary>
        /// Возвращает страницы Wiki по запросу
        /// </summary>
        /// <returns></returns>
        protected override object MainProcess() {
            if (PageVersion != null) {
                return WikiGeneralProvider.GetWikiPageByVersion(Code, PageVersion);
            }

            return WikiGeneralProvider.Get(Usage, Code.SmartSplit(false, true, ',').ToArray()).ToArray();
        }
        /// <summary>
        /// Поддерживает возврат статуса неизменности
        /// </summary>
        /// <returns></returns>
        protected override bool GetSupportNotModified() {
            return true;
        }

        /// <summary>
        /// Возвращает последнюю версию страницы
        /// </summary>
        /// <returns></returns>
        protected override System.DateTime EvalLastModified() {
            return WikiGeneralProvider.GetVersion(Code, WikiObjectType.Page);
        }

        /// <summary>
        /// 	override if Yr action provides 304 state and return ETag header
        /// </summary>
        /// <returns> </returns>
        protected override string EvalEtag() {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Code + "_" + Usage + "_" + WikiRender.WikiRenderVersion)));

        }
    }
}
