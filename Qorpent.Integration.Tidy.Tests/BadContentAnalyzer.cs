using System;
using System.Reflection;
using NUnit.Framework;
using Qorpent.Serialization;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.Tidy.Tests
{
	/// <summary>
	/// Инфраструктурный тест с методом автоматического нахождения "зла"
	/// </summary>
	[TestFixture]
	public class BadContentAnalyzer {
		/// <summary>
		/// Этот тест вызывает автоматический поиск "зла" в большом контенте
		/// </summary>
		/// <param name="filename"></param>
		[TestCase("nr2-1.htm","")]
		[TestCase("jm-1.htm","")]
		[TestCase("bf-1.htm","")]
		[TestCase("gz-1.htm", "http://www.gazeta.ru/politics/2013/10/15_a_5709357.shtml")]
		[TestCase("vz-1.htm","")]
        [TestCase("e1-2.htm", "")]
		[TestCase("ved-1.htm", "http://www.vedomosti.ru/opinion/news/14357781/shkola-liderstva")]
		[TestCase("e1-1.htm", "http://www.e1.ru/talk/forum/list.php?f=152")]
        [TestCase("ved-ur.htm", "")]
		public void TestBadFile(string filename,string baseurl) {
			var text = Assembly.GetExecutingAssembly().ReadManifestResource(filename);
			Console.WriteLine("srclength: " + text.Length);
			var badcontent = GetBadContent(text,baseurl);
			if (!string.IsNullOrWhiteSpace(badcontent)) {
				Console.WriteLine("badcontent length: " + badcontent.Length);
				Console.WriteLine(badcontent);
			}
			
			if (null != lasterror) {
				Console.WriteLine(lasterror);
			}
			
			Assert.IsNull(badcontent);
		}


		[Test]
		public void DoubledAttributesTest_MI143Minimal() {
			const string src = @"<html><body><p class='x' class='y'/></body></html>";
			Assert.True(IsValid(src));
		}
		[Test]
		public void DoubledAttributesTest_MI143Minimal2() {
			const string src = @"<div style='1' class="""" ><div class=""main_block"">";
			Assert.True(IsValid(src));
		}
        [Test]
        public void CanParseHtmlWithXmlHeadDeclaration() {
            var src = Assembly.GetExecutingAssembly().ReadManifestResource("ved-ur.htm"); ;
            Assert.True(IsValid(src));
        }
		/// <summary>
		/// Этот тест уже является коллекцией плохих кусков на проверку,дополнительно минимизированы вручную
		/// </summary>
		/// <param name="content"></param>
        [Explicit]
		[TestCase(@"<script type=""text/JavaScript"" encoding=""", "", Description = "DETECCODE:2 NR2")]
		[TestCase(@"<bf:tag>FFF</bf:tag>", "", Description = "DETECCODE:1 E1")]
		[TestCase(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""><body></body>", "", Description = "DETECTCODE:3 JM")]
		[TestCase(@"<![endif]><div>", "", Description = "DETECTCODE:4 VZ")]
		[TestCase(@"<![if gte IE 5.5000]><div>","", Description = "DETECTCODE:5 VZ")]
		[TestCase(@"<div class=""autors""><p><span itemprop=""author""><a href=""&#77;&#65;&#105;&#76;&#116;&#79;&#58; %74%72%75%64%40%76%65%64%6f%6d%6f%73%74%69%2e%72%75"">������ ����������", "http://www.vedomosti.ru/opinion/news/14357781/shkola-liderstva")]
		[TestCase(@"<noindex><TD><form><TABLE></TABLE></FORM>", "http://www.e1.ru/talk/forum/list.php?f=152")]
		public void DetectedBadContents(string content, string baseurl) {
			Assert.True(IsValid(content,baseurl));
		}

		public string GetBadContent(string content,string baseurl) {
			//если сходу все нормально значит нет плохого контента
			if (IsValid(content,baseurl)) return null;
			//если кусок маленький, и он плохой, возвращаем его.
			if (content.Length < 20) return content;
			
			//теперь пытаемся адаптивно найти точку разрыва, отбавляя от середины по проценту
			var firsttag = FindPointToSplit(content);
			//если не нашли, значит этот контент и есть минимальное зло!!!
			if (5>=firsttag) return content;

			//иначе ищем дальше
			var firstpartofsplit = content.Substring(0, firsttag);
			var secondpartofsplit = content.Substring(firsttag);
			var firstbad = GetBadContent(firstpartofsplit,baseurl);
			var secondbad = GetBadContent(secondpartofsplit, baseurl);
			//если у нас было плохо, а дальше нет - плох весь блок
			if (null == firstbad && null == secondbad) {
				if (content.Length > 600) {
					// try recheck from middle
					secondpartofsplit = content.Substring(content.Length/2 - 200, 400);
					secondbad = GetBadContent(secondpartofsplit, baseurl);
					if (null == secondbad) {
						return content;
					}
				}
				else {
					return content;
				}
			}
			
			if (null == firstbad) return secondbad;
			if (null == secondbad) return firstbad;
			if (firstbad.Length < secondbad.Length) return firstbad;
			return secondbad;

		}

		private static int FindPointToSplit(string content) {
			var middle = content.Length/2;
			var fst = content.Substring(0, middle);
			var sec = content.Substring(middle);
			var fstcandidat = fst.LastIndexOf('<');
			var seccandidat = sec.IndexOf('<') + middle;
			var firsttag = -1;
			if (fstcandidat == -1 || seccandidat == -1) {
				firsttag = Math.Max(fstcandidat, seccandidat);
			}
			else {
				var delt1 = middle-fstcandidat;
				var delt2 = seccandidat - middle;
				if (delt1 < delt2)
				{
					firsttag = fstcandidat;
				}
				else
				{
					firsttag = seccandidat;
				}
			}

	
			return firsttag;
		}

		Exception lasterror = null;
		public bool IsValid(string content,string baseurl=null) {
			try {
				var contentCleaner = new TidyContentCleaner();
				var xml = contentCleaner.CleanContent(content, new ContentCleanerOptions {BaseUrl = baseurl});
				return true;
			}
			catch(Exception ex) {
				lasterror = ex;
				Console.WriteLine(lasterror);
				return false;
			}
		}

	}

	
}
