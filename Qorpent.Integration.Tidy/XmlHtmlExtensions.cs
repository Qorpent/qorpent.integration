using System.Xml.Linq;
using Qorpent.Utils.Extensions;

namespace Qorpent.Integration.Tidy {
	/// <summary>
	/// Классы для работы с Xml в "манере" HTML
	/// </summary>
	public static class XmlHtmlExtensions {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public static XElement SetClassName(this XElement e, string className) {
			if (!e.HasClassName(className)) {
				var currentClasses = e.Attr("class").SmartSplit(false, true, ' ');
				currentClasses.Add(className);
				var currentClass = string.Join(" ", currentClasses);
				e.SetAttributeValue("class",currentClass);
			}
			return e;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public static XElement RemoveClassName(this XElement e, string className)
		{
			if (e.HasClassName(className))
			{
				var currentClasses = e.Attr("class").SmartSplit(false,true,' ');
				currentClasses.Remove(className);
				var currentClass = string.Join(" ", currentClasses);			
				e.SetAttributeValue("class", currentClass);
			}
			return e;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public static XElement ToggleClassName(this XElement e, string className)
		{
			if (e.HasClassName(className)) {
				e.RemoveClassName(className);
			}
			else {
				e.SetClassName(className);
			}
			return e;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public static bool HasClassName(this XElement e, string className)
		{
			return e.Attr("class").SmartSplit(false, true, ' ').Contains(className);
		}
	}
}