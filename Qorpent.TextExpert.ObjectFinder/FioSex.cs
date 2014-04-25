#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioSex.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Пол человека
	/// </summary>
	public enum FioSex {
		/// <summary>
		/// 	Мужской
		/// </summary>
		Male,

		/// <summary>
		/// 	Женский
		/// </summary>
		Female,

		/// <summary>
		/// 	Все
		/// </summary>
		Any,
        /// <summary>
        /// Ошибка при определении
        /// </summary>
	    Error,
	}
}