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
	/// 	Õ‡·Ó ÚËÔÓ‚˚ı Ô‡‚ËÎ ÔÂÓ·‡ÁÓ‚‡ÌËˇ ËÏÂÌ Ë Ù‡ÏËÎËÈ
	/// </summary>
	public static class FioTransformerRuleSet {
		/// <summary>
		/// 	—Ú‡Ì‰‡ÚÌ˚È Ì‡·Ó Ô‡‚ËÎ
		/// </summary>
		public static readonly FioTransformerRule[] Default = new[]
			{
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "Œ", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "≈", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "»", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "”", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "ﬁ", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "»’", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "€’", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "”¿", Padezh.Any, FioTrasformerRuleType.Append, ""),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "»◊", Padezh.Rodit, FioTrasformerRuleType.Append, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "»◊", Padezh.Vinit, FioTrasformerRuleType.Append, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "»◊", Padezh.Datel, FioTrasformerRuleType.Append, "”"),
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "»◊", Padezh.Tvorit, FioTrasformerRuleType.Append, "≈Ã")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.AddName, "»◊", Padezh.Predlozh, FioTrasformerRuleType.Append, "≈")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "¬Õ¿", Padezh.Rodit, FioTrasformerRuleType.Replace,
				                       "¬Õ€"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "¬Õ¿", Padezh.Datel, FioTrasformerRuleType.Replace,
				                       "¬Õ≈"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "¬Õ¿", Padezh.Vinit, FioTrasformerRuleType.Replace,
				                       "¬Õ”"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "¬Õ¿", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "¬ÕŒ…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.AddName, "¬Õ¿", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "¬Õ≈"),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "— ¿", Padezh.Rodit, FioTrasformerRuleType.Replace, "— »")
				,
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "— ¿", Padezh.Datel, FioTrasformerRuleType.Replace, "— ≈")
				,
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "— ¿", Padezh.Vinit, FioTrasformerRuleType.Replace, "— ”")
				,
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "— ¿", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "— Œ…"),
				new FioTransformerRule(FioSex.Any, FioStructPart.LastName, "— ¿", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "— ≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¬", Padezh.Rodit, FioTrasformerRuleType.Append, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "À‹", Padezh.Rodit, FioTrasformerRuleType.Replace, "Àﬂ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Õ", Padezh.Rodit, FioTrasformerRuleType.Append, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "“»…", Padezh.Rodit, FioTrasformerRuleType.Replace,
				                       "“»ﬂ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "»…", Padezh.Rodit, FioTrasformerRuleType.Replace, "Œ√Œ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "€…", Padezh.Rodit, FioTrasformerRuleType.Replace, "Œ√Œ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Œ…", Padezh.Rodit, FioTrasformerRuleType.Replace, "Œ√Œ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¿", Padezh.Rodit, FioTrasformerRuleType.Replace, "€"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "ﬂ", Padezh.Rodit, FioTrasformerRuleType.Replace, "»"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Rodit, FioTrasformerRuleType.Replace, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¬", Padezh.Datel, FioTrasformerRuleType.Append, "”"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "À‹", Padezh.Datel, FioTrasformerRuleType.Replace, "Àﬁ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Õ", Padezh.Datel, FioTrasformerRuleType.Append, "”"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "“»…", Padezh.Datel, FioTrasformerRuleType.Replace,
				                       "“»ﬁ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "»…", Padezh.Datel, FioTrasformerRuleType.Replace, "ŒÃ”")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "€…", Padezh.Datel, FioTrasformerRuleType.Replace, "ŒÃ”")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Œ…", Padezh.Datel, FioTrasformerRuleType.Replace, "ŒÃ”")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¿", Padezh.Datel, FioTrasformerRuleType.Replace, "≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "ﬂ", Padezh.Datel, FioTrasformerRuleType.Replace, "≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Datel, FioTrasformerRuleType.Replace, "”"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¬", Padezh.Vinit, FioTrasformerRuleType.Append, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "À‹", Padezh.Vinit, FioTrasformerRuleType.Replace, "Àﬂ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Õ", Padezh.Vinit, FioTrasformerRuleType.Append, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "“»…", Padezh.Vinit, FioTrasformerRuleType.Replace,
				                       "“»ﬂ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "»…", Padezh.Vinit, FioTrasformerRuleType.Replace, "Œ√Œ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "€…", Padezh.Vinit, FioTrasformerRuleType.Replace, "Œ√Œ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Œ…", Padezh.Vinit, FioTrasformerRuleType.Replace, "Œ√Œ")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¿", Padezh.Vinit, FioTrasformerRuleType.Replace, "”"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "ﬂ", Padezh.Vinit, FioTrasformerRuleType.Replace, "ﬁ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Vinit, FioTrasformerRuleType.Replace, "¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¬", Padezh.Tvorit, FioTrasformerRuleType.Append, "€Ã"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "À‹", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "À≈Ã"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Õ", Padezh.Tvorit, FioTrasformerRuleType.Append, "€Ã"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "“»…", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "“»≈Ã"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "»…", Padezh.Tvorit, FioTrasformerRuleType.Replace, "»Ã")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "€…", Padezh.Tvorit, FioTrasformerRuleType.Replace, "€Ã")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Œ…", Padezh.Tvorit, FioTrasformerRuleType.Replace, "»Ã")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¿", Padezh.Tvorit, FioTrasformerRuleType.Replace, "Œ…")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "ﬂ", Padezh.Tvorit, FioTrasformerRuleType.Replace, "≈…"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Tvorit, FioTrasformerRuleType.Replace, "ŒÃ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¬", Padezh.Predlozh, FioTrasformerRuleType.Append, "≈")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "À‹", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "À≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Õ", Padezh.Predlozh, FioTrasformerRuleType.Append, "≈")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "“»…", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "“»»"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "»…", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "ŒÃ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "€…", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "ŒÃ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "Œ…", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "ŒÃ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "¿", Padezh.Predlozh, FioTrasformerRuleType.Replace, "≈")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "ﬂ", Padezh.Predlozh, FioTrasformerRuleType.Replace, "≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.LastName, "", Padezh.Predlozh, FioTrasformerRuleType.Replace, "≈")
				,
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "…", Padezh.Rodit, FioTrasformerRuleType.Replace, "ﬂ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "‹", Padezh.Rodit, FioTrasformerRuleType.Replace, "ﬂ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "ﬂ", Padezh.Rodit, FioTrasformerRuleType.Replace, "»"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "¬≈À", Padezh.Rodit, FioTrasformerRuleType.Replace, "¬À¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Rodit, FioTrasformerRuleType.Replace, "¿"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "…", Padezh.Datel, FioTrasformerRuleType.Replace, "ﬁ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "‹", Padezh.Datel, FioTrasformerRuleType.Replace, "ﬁ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "ﬂ", Padezh.Datel, FioTrasformerRuleType.Replace, "≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "¬≈À", Padezh.Datel, FioTrasformerRuleType.Replace, "¬À”"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Datel, FioTrasformerRuleType.Replace, "”"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "…", Padezh.Vinit, FioTrasformerRuleType.Replace, "ﬂ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "‹", Padezh.Vinit, FioTrasformerRuleType.Replace, "ﬂ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "ﬂ", Padezh.Vinit, FioTrasformerRuleType.Replace, "ﬁ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "¬≈À", Padezh.Vinit, FioTrasformerRuleType.Replace, "¬À¿"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Vinit, FioTrasformerRuleType.Replace, "¿"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "…", Padezh.Tvorit, FioTrasformerRuleType.Replace, "≈Ã"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "‹", Padezh.Tvorit, FioTrasformerRuleType.Replace, "≈Ã"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "ﬂ", Padezh.Tvorit, FioTrasformerRuleType.Replace, "≈…"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "¬≈À", Padezh.Tvorit, FioTrasformerRuleType.Replace, "¬ÀŒÃ"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Tvorit, FioTrasformerRuleType.Replace, "ŒÃ"),
				
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "‹", Padezh.Predlozh, FioTrasformerRuleType.Replace, "≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "»…", Padezh.Predlozh, FioTrasformerRuleType.Replace, "»»"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "≈…", Padezh.Predlozh, FioTrasformerRuleType.Replace, "≈≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "¬≈À", Padezh.Predlozh, FioTrasformerRuleType.Replace, "¬À≈"),
				new FioTransformerRule(FioSex.Male, FioStructPart.Name, "", Padezh.Predlozh, FioTrasformerRuleType.Replace, "≈"),
				

				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿", Padezh.Rodit, FioTrasformerRuleType.Replace, "Œ…")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿ﬂ", Padezh.Rodit, FioTrasformerRuleType.Replace,
				                       "Œ…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿", Padezh.Datel, FioTrasformerRuleType.Replace, "Œ…")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿ﬂ", Padezh.Datel, FioTrasformerRuleType.Replace,
				                       "Œ…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿", Padezh.Vinit, FioTrasformerRuleType.Replace, "”")
				,
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿ﬂ", Padezh.Vinit, FioTrasformerRuleType.Replace,
				                       "”ﬁ"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "Œ…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿ﬂ", Padezh.Tvorit, FioTrasformerRuleType.Replace,
				                       "Œ…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "Œ…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.LastName, "¿ﬂ", Padezh.Predlozh, FioTrasformerRuleType.Replace,
				                       "Œ…"),
				
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, " ¿“≈–»Õ", Padezh.Any, FioTrasformerRuleType.Replace, "≈ ¿“≈–»Õ"){Rewriter = true,IsPrefix=true},
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, " ¿", Padezh.Rodit, FioTrasformerRuleType.Replace, " »"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "Õ¿", Padezh.Rodit, FioTrasformerRuleType.Replace, "Õ€"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "À¿", Padezh.Rodit, FioTrasformerRuleType.Replace, "À€"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "ﬂ", Padezh.Rodit, FioTrasformerRuleType.Replace, "»"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "√¿", Padezh.Rodit, FioTrasformerRuleType.Replace, "√»"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "¿", Padezh.Rodit, FioTrasformerRuleType.Replace, "€"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "¿", Padezh.Datel, FioTrasformerRuleType.Replace, "≈"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "ﬂ", Padezh.Datel, FioTrasformerRuleType.Replace, "»"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "¿", Padezh.Vinit, FioTrasformerRuleType.Replace, "”"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "ﬂ", Padezh.Vinit, FioTrasformerRuleType.Replace, "ﬁ"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "¿", Padezh.Tvorit, FioTrasformerRuleType.Replace, "Œ…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "ﬂ", Padezh.Tvorit, FioTrasformerRuleType.Replace, "≈…"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "¿", Padezh.Predlozh, FioTrasformerRuleType.Replace, "≈"),
				new FioTransformerRule(FioSex.Female, FioStructPart.Name, "ﬂ", Padezh.Predlozh, FioTrasformerRuleType.Replace, "»"),
			};


	
	}
}