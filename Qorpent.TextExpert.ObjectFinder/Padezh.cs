#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : Padezh.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Падеж
	/// </summary>
	public enum Padezh {
		/// <summary>
		/// 	Именительный
		/// </summary>
		Imenit,

		/// <summary>
		/// 	Родительный (кого, чье)
		/// </summary>
		Rodit,

		/// <summary>
		/// 	Дательный ( кому )
		/// </summary>
		Datel,

		/// <summary>
		/// 	Винительный
		/// </summary>
		Vinit,

		/// <summary>
		/// 	Твороительный
		/// </summary>
		Tvorit,

		/// <summary>
		/// 	Предложный
		/// </summary>
		Predlozh,

		/// <summary>
		/// 	Все
		/// </summary>
		Any
	}
}