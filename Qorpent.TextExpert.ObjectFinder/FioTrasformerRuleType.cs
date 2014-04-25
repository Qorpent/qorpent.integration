#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTrasformerRuleType.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Тип преобразования на строке
	/// </summary>
	public enum FioTrasformerRuleType {
		/// <summary>
		/// 	Регулярное выражение
		/// </summary>
		Replace,

		/// <summary>
		/// 	Дополнительный суффикс
		/// </summary>
		Append,
	}
}