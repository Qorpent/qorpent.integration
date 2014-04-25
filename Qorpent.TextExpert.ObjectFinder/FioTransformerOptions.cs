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
		/// ����������� �� ��������� - �������� ����� ��������������� ����������� ����
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
		/// 	True - ������� ��������������� ��������� ������� � ����
		/// </summary>
		public bool AutoTranslate { get; set; }

		/// <summary>
		/// 	True - ������� ��������������� ����������� ����
		/// </summary>
		public bool AutoSex { get; set; }

		/// <summary>
		/// </summary>
		public int SelfLastNameWeight { get; set; }

		/// <summary>
		/// 	���� ��������� ���
		/// </summary>
		public FioSex Sex { get; set; }

		/// <summary>
		/// 	�������� � ���������� ����������� ��� �����
		/// </summary>
		public bool NotDotedAbbrevations { get; set; }
        /// <summary>
        /// 
        /// </summary>
	    public bool IgnoreErrors { get; set; }
	}
}