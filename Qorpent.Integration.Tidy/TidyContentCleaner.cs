using System.Xml.Linq;
using Qorpent.IoC;
using Qorpent.Serialization;

namespace Qorpent.Integration.Tidy {
	/// <summary>
	/// Общая сервисная обертка
	/// </summary>
	[ContainerComponent(Lifestyle=Lifestyle.Transient,Name="tidy.cleaner",ServiceType = typeof(IContentCleaner))]
	public class TidyContentCleaner:IContentCleaner {
		private HtmlCleaner hc = new HtmlCleaner();
		private XmlCleaner xc = new XmlCleaner();

		/// <summary>
		/// Очищает переданный контент до совместимости с XML
		/// </summary>
		/// <param name="content"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public XElement CleanContent(string content,  ContentCleanerOptions options = null) {
			options = options ?? ContentCleanerOptions.Default;
			var cleanedHtml = hc.Clean(content);
			var cleanedXml = xc.Clean(cleanedHtml, options.BaseUrl, options.Operations);
			return cleanedXml;
		}
	}
}