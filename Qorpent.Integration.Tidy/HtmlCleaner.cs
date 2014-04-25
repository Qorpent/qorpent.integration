using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Qorpent.Utils.Extensions;
using TidyManaged;

namespace Qorpent.Integration.Tidy
{
	/// <summary>
	/// Оболочка над Tidy, обеспечивающая реальную очистку
	/// </summary>
	public class HtmlCleaner {
	    /// <summary>
	    /// Вызов очистки контента
	    /// </summary>
	    /// <param name="html"></param>
	    /// <param name="removeScripts"></param>
	    /// <returns></returns>
	    public XElement Clean(string html, bool removeScripts = true) {
            if (removeScripts) {
                html = RemoveScripts(html);
            }
			var cleanedHtml = RepairHtml(html);
	        return XElement.Parse(cleanedHtml);
	    }

        private string RemoveScripts(string html) {
            return Regex.Replace(html, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
		

		private void SetUpDefaultTidy(Document document) {
            document.Quiet = true;
		    document.ShowWarnings = false;
			document.CharacterEncoding = EncodingType.Utf8;
			document.NewInlineTags = GetNewInlineElements();
			document.NewBlockLevelTags = GetNewBlockTags();
			document.OutputXml = true;
			document.OutputNumericEntities = true;
			document.DocType = DocTypeMode.Omit;
			document.DropEmptyParagraphs = true;
			document.DropFontTags = true;
			document.DropProprietaryAttributes = true;
			document.MergeDivs = AutoBool.No;
			document.MergeSpans = AutoBool.No;
			document.EscapeCdata = true;
		    document.RemoveComments = true;
		    document.MaximumErrors = 0;
		}
		private string TidyWithPreprocessingAndSafeMode(string html)
		{
			var list = GetNewBlockTags().SmartSplit();


			html = Regex.Replace(html, @"(?ix)(?<=</?)form+", @"qform");

			MatchCollection matches = Regex.Matches(html, @"(?<=</?)[\w\d_\-:\.]+");
			var tags = matches.OfType<Match>().Select(_ => _.Value.ToLower()).Distinct();

			foreach (var tag in tags)
			{
				if (-1 != Array.IndexOf(_classicHtmlTags, tag))
				{
					continue;
				}

				if (-1 != Array.IndexOf(_html5BlockElements, tag))
				{
					continue;
				}

				if (-1 != Array.IndexOf(_html5InlineElements, tag))
				{
					continue;
				}

				list.Add(tag);
			}

			var nbe = string.Join(",", list);
			var document = Document.FromString(html);

			SetUpDefaultTidy(document);
			document.NewBlockLevelTags = nbe;
			document.CleanAndRepair();

			return document.Save();
		}

		private string GetNewInlineElements()
		{
			return "mark,time,meter,progress,command,qform";
		}

		private string GetNewBlockTags() {
			return _html5BlockElements.Aggregate((_, __) => string.Format("{0},{1}", _, __));
		}

		private readonly string[] _classicHtmlTags = new[] {
            "a", "abbr", "acronym", "address", "applet", "area", "b", "base", "basefont", "bdo",
            "big", "blockquote", "body", "br", "button", "caption", "center", "cite", "code",
            "col", "colgroup", "dd", "del", "dfn", "dir", "div", "dl", "dt", "em", "fieldset", "font",
            "form", "frame", "frameset", "h1", "h2", "h3", "h4", "h5", "h6", "head", "hr", "html", "i",
            "iframe", "img", "input", "ins", "isindex", "kbd", "label", "legend", "li", "link", "map",
            "menu", "meta", "noframes", "noscript", "object", "ol", "optgroup", "option", "p", "param",
            "pre", "q", "s", "samp", "script", "select", "small", "span", "strike", "strong", "style",
            "sub", "sup", "table", "tbody", "td", "textarea", "tfoot", "th", "thead", "title", "tr", "tt",
            "u", "ul", "var", "qform"
        };

		private readonly string[] _html5BlockElements = new[] {
            "article", "aside", "audio", "canvas", "figcaption", "figure", "footer", "header", "hgroup",
            "output", "section", "video", "details", "datagrid", "menu", "nav"
        };

		private readonly string[] _html5InlineElements = new[] {
            "mark", "time", "meter", "progress", "command", "form"
        };
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string RepairHtml(string html)
		{
			html = html.Trim();
			//ANSWERTO: DETECTED 01
			html = Regex.Replace(html, @"<(\w+):", "<$1.");
			//ANSWERTO: DETECTED 02
			html = html.Replace("encoding=", "failsafe_encoding=");
            if (html.StartsWith("<?")) {
                var end = html.IndexOf("?>", StringComparison.InvariantCulture);
                if (end > -1) {
                    html = html.Substring(end + 2);
                }
            }
			//ANSWERTO: DETECTED 03
			if (html.StartsWith("<!DOCTYPE")) {
				var fstclose = html.IndexOf('>');
				html = html.Substring(fstclose);
			}
			//ANSWERTO: DETECTED 04
			if (html.IndexOf("<![") != -1) {
				html = Regex.Replace(html, @"<!\[((endif)|(if\s[^\]]+?))\]>", "");

			}


		    try {
		        return TidyWithoutPreprocessing(html);
		    } catch (Exception) {
		        return TidyWithPreprocessingAndSafeMode(html);
		    }
		}
	
		private string TidyWithoutPreprocessing(string html) {
			var document = Document.FromString(html);
			SetUpDefaultTidy(document);
			document.CleanAndRepair();
			return document.Save();
		}
	}


	
}
