#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTransformerOptions.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// </summary>
	public class FioTransformerOptions {
		/// <summary>
		///  онструктор по умолчанию - включает опцию автоматического определени€ пола
		/// </summary>
		public FioTransformerOptions() {
			AutoSex = true;
		}
		/// <summary>
		/// </summary>
		public static FioTransformerOptions Default {
			get { return new FioTransformerOptions {AutoSex = true, AutoTranslate = true}; }
		}

		/// <summary>
		/// 	True - попытка автоматического склонени€ фамилий и имен
		/// </summary>
		public bool AutoTranslate { get; set; }

		/// <summary>
		/// 	True - попытка автоматического определени€ пола
		/// </summary>
		public bool AutoSex { get; set; }

		/// <summary>
		/// </summary>
		public int SelfLastNameWeight { get; set; }

		/// <summary>
		/// 	явно указанный пол
		/// </summary>
		public FioSex Sex { get; set; }

		/// <summary>
		/// 	¬ключать в результаты абревиатуры без точек
		/// </summary>
		public bool NotDotedAbbrevations { get; set; }
        /// <summary>
        /// 
        /// </summary>
	    public bool IgnoreErrors { get; set; }
	}
}