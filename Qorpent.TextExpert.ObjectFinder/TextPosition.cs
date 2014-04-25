#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : TextPosition.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System;

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Позиция элемента в строке
	/// </summary>

	public struct TextPosition {
		/// <summary>
		/// </summary>
		/// <param name="line"> </param>
		/// <param name="col"> </param>
		/// <param name="charpos"> </param>
		public TextPosition(int line, int col, int charpos) : this() {
			Line = line;
			Col = col;
			CharPos = charpos;
		}

		/// <summary>
		/// 	Строка
		/// </summary>
		public int Line { get; set; }

		/// <summary>
		/// 	Колонка
		/// </summary>
		public int Col { get; set; }

		/// <summary>
		/// 	Конкретная позиция в тексте
		/// </summary>
		public int CharPos { get; set; }

		/// <summary>
		/// </summary>
		/// <param name="s"> </param>
		/// <returns> </returns>
		public static implicit operator TextPosition(string s) {
			return new TextPosition(Convert.ToInt32(s.Split(':')[0]), Convert.ToInt32(s.Split(':')[1]),
			                        Convert.ToInt32(s.Split(':')[2]));
		}

		/// <summary>
		/// </summary>
		/// <returns> </returns>
		public override string ToString() {
			return Line + ":" + Col + ":" + CharPos;
		}
	}
}