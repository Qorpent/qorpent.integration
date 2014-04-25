#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTransformerRuleSet.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	����� ������� ������ �������������� ���� � �������
	/// </summary>
	public static class FioTransformerRuleSet {
		/// <summary>
		/// 	����������� ����� ������
		/// </summary>
		public static readonly FioTransformerRule[] Default = new[]
			{
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "�", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "�", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "�", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "�", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "�", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "��", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "��", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "��", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "��", Padezh.Rodit, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "��", Padezh.Vinit, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "��", Padezh.Datel, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "��", Padezh.Tvorit, FioTrasformerRuleType.Append, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "��", Padezh.Predlozh, FioTrasformerRuleType.Append, "�")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "���", Padezh.Rodit, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "���", Padezh.Datel, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "���", Padezh.Vinit, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "���", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "����"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "���", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "���", Padezh.Rodit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "���", Padezh.Datel, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "���", Padezh.Vinit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "���", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "����"),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "���", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Rodit, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Rodit, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "���", Padezh.Rodit, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Datel, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Datel, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Datel, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "���", Padezh.Datel, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Datel, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Datel, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Datel, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Vinit, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Vinit, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Vinit, FioTrasformerRuleType.Append, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "���", Padezh.Vinit, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Vinit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Vinit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Vinit, FioTrasformerRuleType.Replace, "���")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Tvorit, FioTrasformerRuleType.Append, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Tvorit, FioTrasformerRuleType.Append, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "���", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "����"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Predlozh, FioTrasformerRuleType.Append, "�")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Predlozh, FioTrasformerRuleType.Append, "�")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "���", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "��", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Predlozh, FioTrasformerRuleType.Replace, "�")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "�", Padezh.Predlozh, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Predlozh, FioTrasformerRuleType.Replace, "�")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "���", Padezh.Rodit, FioTrasformerRuleType.Replace, "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "���", Padezh.Datel, FioTrasformerRuleType.Replace, "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "���", Padezh.Vinit, FioTrasformerRuleType.Replace, "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "���", Padezh.Tvorit, FioTrasformerRuleType.Replace, "����"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "�", Padezh.Predlozh, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "��", Padezh.Predlozh, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "��", Padezh.Predlozh, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "���", Padezh.Predlozh, FioTrasformerRuleType.Replace, "���"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Predlozh, FioTrasformerRuleType.Replace, "�"),
				

				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "��", Padezh.Rodit, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "��")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "��", Padezh.Datel, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "��", Padezh.Vinit, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "��", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "�", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "��", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "��"),
				
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�������", Padezh.Any, FioTrasformerRuleType.Replace, "��������"){Rewriter = true,IsPrefix=true},
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "��", Padezh.Rodit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Rodit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Datel, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Vinit, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Tvorit, FioTrasformerRuleType.Replace, "��"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Predlozh, FioTrasformerRuleType.Replace, "�"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "�", Padezh.Predlozh, FioTrasformerRuleType.Replace, "�"),
			};


	
	}
}