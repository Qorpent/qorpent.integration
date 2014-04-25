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
	/// 	������� �������������� ���
	/// </summary>
	public sealed class FioTransformerRule {
		/// <summary>
		/// 	������� �������
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
		/// 	������� ����� �����
		/// </summary>
		public FioStructPart Part { get; set; }

		/// <summary>
		/// 	������� ��� ������ ������������
		/// </summary>
		public string Suffix { get; set; }

		/// <summary>
		/// 	��� ��������������
		/// </summary>
		public FioTrasformerRuleType Type { get; set; }

		/// <summary>
		/// 	������� �����
		/// </summary>
		public Padezh Padezh { get; set; }

		/// <summary>
		/// 	������� ���
		/// </summary>
		public FioSex Sex { get; set; }

		/// <summary>
		/// 	����������� ��� ������� ��� ���������� ��� Append
		/// </summary>
		public string Changer { get; set; }

		/// <summary>
		/// �������������� �������
		/// </summary>
		public bool Rewriter { get; set; }

		/// <summary>
		/// ��������� ������� � �� �������
		/// </summary>
		public bool IsPrefix { get; set; }

		/// <summary>
		/// 	��������� ������������ �������
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
		/// 	��������� ������� � ������� ���������
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