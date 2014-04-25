#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTransformerAction.cs
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
	[Action("texof.fiot")]
	public class FioTransformerAction : ActionBase {
		/// <summary>
		/// 	Выполняет преобразование фамили в набор вариантов
		/// </summary>
		/// <returns> </returns>
		protected override object MainProcess() {
			var opts = new FioTransformerOptions
				{AutoSex = true, NotDotedAbbrevations = usemergedabbr, SelfLastNameWeight = uselastname ? 100 : 0};
			var scaner = new FioTransformer();
			return scaner.GetVariants(fio, opts).ToArray();
		}

		/// <summary>
		/// 	Исходная фамилия
		/// </summary>
		[Bind] public string fio;

		/// <summary>
		/// 	"Говорящая" фамилия
		/// </summary>
		[Bind] public bool uselastname;

		/// <summary>
		/// 	Использовать сведенные абревиатуры
		/// </summary>
		[Bind] public bool usemergedabbr;
	}
}