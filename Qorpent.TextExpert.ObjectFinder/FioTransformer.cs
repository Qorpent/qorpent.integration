#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTransformer.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Вспомогательный класс для парсера фамилий, имен, отчеств
	/// </summary>
	public class FioTransformer {
		private static readonly string[] _femaleLastEnds = new[] {"ОВА", "ИНА", "ЕВА", "ЫНА", "КАЯ"};
		private static readonly string[] _maleLastEnds = new[] {"ОВ", "ИН", "ЕВ", "ЫН", "КИЙ"};
		private static readonly string[] _femaleNameEnds = new[] {"А", "Я"};

		/// <summary>
		/// 	Правила преобразования
		/// </summary>
		public FioTransformerRule[] Rules { get; set; }

		/// <summary>
		/// 	Получить пол человека
		/// </summary>
		/// <param name="fioStruct"> </param>
		/// <param name="transformerOptions"> </param>
		/// <returns> </returns>
		public FioSex GetSex(FioStruct fioStruct, FioTransformerOptions transformerOptions) {
		    
		        if (null == fioStruct)
		        {
		            throw new ArgumentNullException("fioStruct");
		        }
                try
                {
		        if (fioStruct.Error != null)
		        {
		            return FioSex.Female;
		        }
		        if (!transformerOptions.AutoSex)
		        {
		            return transformerOptions.Sex;
		        }
		        if (!string.IsNullOrWhiteSpace(fioStruct.AddName))
		        {
		            if (fioStruct.AddName.EndsWith("НА"))
		            {
		                return FioSex.Female;
		            }
		            if (fioStruct.AddName.EndsWith("ВИЧ"))
		            {
		                return FioSex.Male;
		            }
		        }

		        if (_femaleLastEnds.Any(fioStruct.LastName.EndsWith))
		        {
		            return FioSex.Female;
		        }
		        if (_maleLastEnds.Any(fioStruct.LastName.EndsWith))
		        {
		            return FioSex.Male;
		        }
		        if (_femaleNameEnds.Any(fioStruct.Name.EndsWith))
		        {
		            return FioSex.Female;
		        }
		        return FioSex.Male;
		    }
		    catch (Exception e)
		    {
		        if (transformerOptions.IgnoreErrors) return FioSex.Error;
                
		        throw e;
		    }
		}

		/// <summary>
		/// 	Возвращает в указанном падеже
		/// </summary>
		/// <param name="fioStruct"> </param>
		/// <param name="transformerOptions"> </param>
		/// <param name="padezh"> </param>
		/// <returns> </returns>
		public FioStruct GetWithPadezh(FioStruct fioStruct, FioTransformerOptions transformerOptions, Padezh padezh) {
			Rules = Rules ?? FioTransformerRuleSet.Default;
			if (padezh == Padezh.Imenit) {
				return fioStruct;
			}
			
			var result = new FioStruct {Name = fioStruct.Name, LastName = fioStruct.LastName, AddName = fioStruct.AddName};
			var sex = GetSex(fioStruct, transformerOptions?? new FioTransformerOptions
			{
			    AutoSex = true,
			    AutoTranslate = true,
			});
			foreach (var p in new[] {FioStructPart.LastName, FioStructPart.Name, FioStructPart.AddName,}) {
				var rule =
					Rules.Where(
						x => (x.Sex == sex || x.Sex == FioSex.Any) && x.Part == p && (x.Padezh == padezh || x.Padezh == Padezh.Any)
						&&(!x.Rewriter)).
						FirstOrDefault(x => x.Match(result));
				if (null != rule) {
					rule.Apply(result);
				}
				var fixerrules =
					Rules.Where(
						x => (x.Sex == sex || x.Sex == FioSex.Any) && x.Part == p && (x.Padezh == padezh || x.Padezh == Padezh.Any)
						     && (x.Rewriter)).Where(x => x.Match(result));
				foreach (var fixerrule in fixerrules) {
					fixerrule.Apply(result);
				}

			}
			return result;
		}


		/// <summary>
		/// 	Получает все варианты написания ФИО c весами
		/// </summary>
		/// <param name="sourcefio"> </param>
		/// <param name="transformerOptions"> </param>
		/// <returns> </returns>
		public IEnumerable<FioVariant> GetVariants(FioStruct sourcefio, FioTransformerOptions transformerOptions = null) {
		    if (null != sourcefio.Error)
		    {
		        if (transformerOptions.IgnoreErrors)
		        {
		            yield return
		                new FioVariant("ОШИБКА [" + sourcefio.Name + "]", -1, Padezh.Imenit, false, false,
		                    false, false);
		        }
		        else
		        {
		            throw sourcefio.Error;
		        }
		    }
			transformerOptions = transformerOptions ?? FioTransformerOptions.Default;
			foreach (
				var padezh in new[] {Padezh.Imenit, Padezh.Rodit, Padezh.Datel, Padezh.Vinit, Padezh.Tvorit, Padezh.Predlozh}) {
				var transformedfio = GetWithPadezh(sourcefio, transformerOptions, padezh);
				if (transformerOptions.SelfLastNameWeight != 0) {
					yield return
						new FioVariant(transformedfio.LastName, transformerOptions.SelfLastNameWeight, padezh, true, false, false, true);
				}
				if (!string.IsNullOrWhiteSpace(transformedfio.AddName)) {
					yield return new FioVariant(transformedfio.ToString(true, false, true), 100, padezh, false, true, false, true);
					yield return new FioVariant(transformedfio.ToString(true, false, false), 100, padezh, false, true, false, false);

					yield return new FioVariant(transformedfio.ToString(true, true, true), 90, padezh, false, true, true, true);
					yield return new FioVariant(transformedfio.ToString(true, true, false), 90, padezh, false, true, true, false);
					if (transformerOptions.NotDotedAbbrevations) {
						yield return
							new FioVariant(transformedfio.ToString(true, true, true).Replace(".", ""), 90, padezh, false, true, true, true);
						yield return
							new FioVariant(transformedfio.ToString(true, true, false).Replace(".", ""), 90, padezh, false, true, true, false)
							;
					}
				}

				yield return new FioVariant(transformedfio.ToString(false, true, true), 70, padezh, false, false, true, true);
				yield return new FioVariant(transformedfio.ToString(false, true, false), 70, padezh, false, false, true, false);
				if (transformerOptions.NotDotedAbbrevations) {
					yield return
						new FioVariant(transformedfio.ToString(false, true, true).Replace(".", ""), 70, padezh, false, false, true, true);
					yield return
						new FioVariant(transformedfio.ToString(false, true, false).Replace(".", ""), 70, padezh, false, false, true, false)
						;
				}
				yield return new FioVariant(transformedfio.ToString(false, false, true), 80, padezh, false, false, false, true);
				yield return new FioVariant(transformedfio.ToString(false, false, false), 80, padezh, false, false, false, false);
			}
		}
	}
}