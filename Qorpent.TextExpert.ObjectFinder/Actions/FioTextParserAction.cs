#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTextParserAction.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System.Linq;
using Qorpent.Mvc;
using Qorpent.Mvc.Binding;

namespace Comdiv.TextExpert.ObjectFinder.QWeb {
	/// <summary>
	/// 	Действие по описанию вариантов фамилий
	/// </summary>
	[Action("texof.texttofio")]
	public class FioTextParserAction : ActionBase {
		/// <summary>
		/// 	Выполняет преобразование фамили в набор вариантов
		/// </summary>
		/// <returns> </returns>
		protected override object MainProcess() {
			var parser = new FioTextParser();
			return parser.ExecuteEx(text)
				.Select(x => new {txt = x.Item1, positions = x.Item2.ToArray()});
			
		}

		/// <summary>
		/// 	Исходная фамилия
		/// </summary>
		[Bind(IsLargeText = true)] public string text;
	}
}