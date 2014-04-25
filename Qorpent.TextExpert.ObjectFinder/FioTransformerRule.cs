#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTransformerRule.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System.Text.RegularExpressions;

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Правило транслитерации ФИО
	/// </summary>
	public sealed class FioTransformerRule {
		/// <summary>
		/// 	Создает правило
		/// </summary>
		/// <param name="sex"> </param>
		/// <param name="targetPart"> </param>
		/// <param name="suffix"> </param>
		/// <param name="padezh"> </param>
		/// <param name="type"> </param>
		/// <param name="changer"> </param>
		public FioTransformerRule(FioSex sex, FioStructPart targetPart, string suffix, Padezh padezh,
		                          FioTrasformerRuleType type, string changer) {
			Part = targetPart;
			Suffix = suffix;
			Type = type;
			Padezh = padezh;
			Sex = sex;
			Changer = changer;
		}

		/// <summary>
		/// 	Целевая часть имени
		/// </summary>
		public FioStructPart Part { get; set; }

		/// <summary>
		/// 	Суффикс для поиска соответствия
		/// </summary>
		public string Suffix { get; set; }

		/// <summary>
		/// 	Тип преобразования
		/// </summary>
		public FioTrasformerRuleType Type { get; set; }

		/// <summary>
		/// 	Целевой падеж
		/// </summary>
		public Padezh Padezh { get; set; }

		/// <summary>
		/// 	Целевой пол
		/// </summary>
		public FioSex Sex { get; set; }

		/// <summary>
		/// 	Заместитель для регекса или дополнение для Append
		/// </summary>
		public string Changer { get; set; }

		/// <summary>
		/// Переписывающее правило
		/// </summary>
		public bool Rewriter { get; set; }

		/// <summary>
		/// Проверять префикс а не суффикс
		/// </summary>
		public bool IsPrefix { get; set; }

		/// <summary>
		/// 	Проверяет применимость правила
		/// </summary>
		/// <param name="str"> </param>
		/// <returns> </returns>
		public bool Match(FioStruct str) {
			var item = str.GetPart(Part);
			if (string.IsNullOrWhiteSpace(item)) {
				return false;
			}
			if(IsPrefix) {
				return string.IsNullOrWhiteSpace(Suffix) || item.StartsWith(Suffix);
			}else
				return string.IsNullOrWhiteSpace(Suffix) || item.EndsWith(Suffix);
		}

		/// <summary>
		/// 	Применяет правило к целевой структуре
		/// </summary>
		/// <param name="str"> </param>
		public void Apply(FioStruct str) {
			var item = str.GetPart(Part);
			if (Type == FioTrasformerRuleType.Append) {
				item = item + Changer;
			}
			else {
				item = Regex.Replace(item, Suffix + "$", Changer, RegexOptions.Compiled);
			}
			str.SetPart(Part, item);
		}
	}
}