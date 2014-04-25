using System;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Qorpent.Serialization;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.Tidy
{
	/// <summary>
	/// Класс очистки XML от лишнего контента
	/// </summary>
	public class XmlCleaner
	{
		/// <summary>
		/// Стандартный конструктор со всеми опциями
		/// </summary>
		public XmlCleaner() {
			Options = ContentCleanerOperations.All;
		}

		/// <summary>
		/// Опции клинера
		/// </summary>
		public ContentCleanerOperations Options { get; set; }

		/// <summary>
		/// Теги для исключения из выдачи
		/// </summary>
		public readonly string[] ExcludeTags = new[] {
            "script", "style", "embed", "object", "qform",
            "noscript","colgroup","iframe"
        };
		/// <summary>
		/// Аттрибуты для сохранения в контенте
		/// </summary>
		public readonly string[] PreserveAttributes = new[] {
           "id","class","name","href","src"
        };

		/// <summary>
		/// Выполнить все очистки
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="baseurl">базовый адрес</param>
		/// <param name="options"></param>
		/// <returns></returns>
		public XElement Clean(XElement xml, string baseurl = null, ContentCleanerOperations options = ContentCleanerOperations.Undefined)
		{
			var _options = PrepareOptions(options);

			if (_options.HasFlag(ContentCleanerOperations.RemoveBadTags))
			{
				RemoveBadTags(xml);
			}
			if (_options.HasFlag(ContentCleanerOperations.RemoveBadAttributes))
			{
				RemoveBadAttributes(xml);
			}
			if (_options.HasFlag(ContentCleanerOperations.RewriteTables))
			{
				RewriteTables(xml);
			}
			if (!string.IsNullOrWhiteSpace(baseurl)) {
				var baseuri = new Uri(baseurl);
				if (_options.HasFlag(ContentCleanerOperations.FixImageUrls))
				{
					FixImageUrls(xml, baseuri);
				}
				if (_options.HasFlag(ContentCleanerOperations.FixHrefUrls))
				{
					FixHrefUrls(xml, baseuri);
				}
			}
			if (_options.HasFlag(ContentCleanerOperations.SetupPositionClasses))
			{
				SetupPositionClasses(xml);
			}
			return xml;
		}

		private ContentCleanerOperations PrepareOptions(ContentCleanerOperations options)
		{
			var _options = options;
			if (_options == ContentCleanerOperations.Undefined) _options = Options;
			if (_options == ContentCleanerOperations.Undefined) _options = ContentCleanerOperations.Default;
			return _options;
		}

		private void SetupPositionClasses(XElement xml) {
			var notidedElements = xml.DescendantsAndSelf();
			foreach (var e in notidedElements) {
				var level = e.Ancestors().Count();
				var position = e.ElementsBeforeSelf().Count();
				var cls = e.Attr("class");
				cls += " qlevel-" + level + " qpos-" + position;
				e.SetAttributeValue("class",cls);
			}
		}

		///
		public readonly string[] TableTags = new[] {"table", "thead", "tbody", "tr", "td","th"};
		private void RewriteTables(XElement xml) {
			foreach (var e in xml.Descendants()) {
				var tag = e.Name.LocalName;
				if (-1 != Array.IndexOf(TableTags, tag)) {
					e.Name = "div";
					e.SetAttributeValue("tag", tag);
					e.SetClassName("qtag-" + tag);
				}
			}
		}

		private void FixImageUrls(XElement xml, Uri baseuri) {
			
			var images = xml.Descendants("img");
			foreach (var i in images) {
				var src = i.Attribute("src");
				if (null != src) {
					if (!(src.Value.StartsWith("http"))) {
						src.Value = new Uri(baseuri, src.Value).ToString();
					}
				}
			}
		}
		private void FixHrefUrls(XElement xml, Uri baseuri)
		{

			var links = xml.Descendants("a");
			foreach (var i in links)
			{
				i.SetAttributeValue("target", "_blank");
				var src = i.Attribute("href");
				if (null != src) {
					var normalizedlink = src.Value;
					//MI-36 надо оставлять mailto: адреса в неприкосновенности
					if (IsRootedLink(normalizedlink)) {
						continue;
					}
					try {
						src.Value = new Uri(baseuri, normalizedlink).ToString();
					}
					catch {
						throw new Exception("cannot convert to valid uri base ='"+baseuri+"' normlink='"+normalizedlink+"'");
					}

				}
			}
		}

		private bool IsRootedLink(string nlink) {
			var normalizedlink = nlink.ToLower();
			if (normalizedlink.StartsWith("mailto:")) return true;
			if (normalizedlink.StartsWith("http://")) return true;
			if (normalizedlink.StartsWith("https://")) return true;
			if (normalizedlink.StartsWith("ftp://")) return true;
			return false;
		}

		private void RemoveBadAttributes(XElement xml) {
			var badattrs = (xml.XPathEvaluate("//@*") as IEnumerable).Cast<XAttribute>()
			                                                         .Where(
				                                                         _ =>
				                                                         -1 == Array.IndexOf(PreserveAttributes, _.Name.LocalName))
			                                                         .ToArray();
			foreach (var attr in badattrs) {
				attr.Remove();
			}
		}

		private void RemoveBadTags(XElement xml) {
			var badtags = xml.DescendantsAndSelf().Where(
				_ =>(_.Name.LocalName!="img" && string.IsNullOrWhiteSpace(_.Value) &&  !_.HasElements)
 					||
					-1 != Array.IndexOf(ExcludeTags, _.Name.LocalName)).ToArray();
			foreach (var tag in badtags) {
				tag.Remove();
			}
		}
	}
}
