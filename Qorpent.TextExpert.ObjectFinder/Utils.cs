#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : Utils.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System.Collections.Generic;
using System.Text;

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	сРХКХРШ ДКЪ РХОНБШУ ТСМЙЖХИ
	/// </summary>
	public static class Utils {

        private static string GetNormalizedFio(string str)
        {
            var sb = new StringBuilder();
            bool wasletter = false;
            for (var ci = 0; ci < str.Length; ci++)
            {
                var c = str[ci];
                if (wasletter && c == ' ')
                {
                    sb.Append(' ');
                    wasletter = false;
                }
                else if (char.IsLetter(c))
                {
                    sb.Append(c);
                    wasletter = true;
                }
                else if (c == '-')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Trim();
        }
		/// <summary>
		/// 	мНПЛЮКХГСЕР МЮОХЯЮМХЕ ХЛЕМ
		/// </summary>
		/// <param name="candidate"> </param>
		/// <returns> </returns>
		public static string NormalizeFio(string candidate) {
			candidate = GetNormalizedFio(candidate).ToUpper();
			candidate = candidate.Replace('╗', 'е');
			candidate = candidate.Replace("юдхеб", "юдэеб");
			candidate = candidate.Replace("хебм", "эебм");
			candidate = candidate.Replace("хебхв", "эебхв");
			candidate = candidate.Replace("цемюд", "цеммюд");
			candidate = candidate.Replace("хмнйем", "хммнйем");

	
			
			if(candidate.StartsWith("йюрепхм")) {
				candidate = "е" + candidate;
			}
			candidate = candidate.Replace(" йюрепхм", " ейюрепхм");

			var stub = new FioStruct(candidate);
			if(!string.IsNullOrWhiteSpace(stub.LastName)) {
				stub.LastName = FixUp(stub.LastName);
			}
			if (!string.IsNullOrWhiteSpace(stub.Name))
			{
				stub.Name = FixUp(stub.Name);
			}
			if (!string.IsNullOrWhiteSpace(stub.AddName))
			{
				stub.AddName = FixUp(stub.AddName);
			}

			return stub.ToString(true,false,true);
		}

		private static string FixUp(string lastName) {
			if(LightNames.ContainsKey(lastName)) {
				return LightNames[lastName];
			}
			return lastName;
		}

		/// <summary>
		/// яКНБЮПЭ СЛЕМЭЬХРЕКЭМШУ ХЛЕМ
		/// </summary>
		public  static IDictionary<string,string> LightNames = new Dictionary<string, string>
			{
				{"йюръ","ейюрепхмю"},
				{"йюрх","ейюрепхмш"},
				{"йюре","ейюрепхме"},
				{"йюрч","ейюрепхмс"},
				{"йюреи","ейюрепхмни"},

				{"йячью","йяемхъ"},
				{"йячьх","йяемхх"},
				{"йячье","йяемхх"},
				{"йячьс","йяемхч"},
				{"йячьеи","йяемхеи"},

				{"йячую","йяемхъ"},
				{"йячух","йяемхх"},
				{"йячуе","йяемхх"},
				{"йячус","йяемхч"},
				{"йячуни","йяемхеи"},

				{"люью","люпхъ"},
				{"люьх","люпхх"},
				{"люье","люпхх"},
				{"люьс","люпхч"},
				{"люьеи","люпхеи"},
				
				{"бнбю","бкюдхлхп"},
				{"бнбш","бкюдхлхпю"},
				{"бнбе","бкюдхлхпс"},
				{"бнбс","бкюдхлхпю"},
				{"бнбни","бкюдхлхпнл"},

				{"бнбйю","бкюдхлхп"},
				{"бнбйх","бкюдхлхпю"},
				{"бнбйе","бкюдхлхпс"},
				{"бнбйс","бкюдхлхпю"},
				{"бнбйни","бкюдхлхпнл"},

				{"бнкндъ","бкюдхлхп"},
				{"бнкнкдх","бкюдхлхпю"},
				{"бнкнкдч","бкюдхлхпю"},
				{"бнкнкде","бкюдхлхпс"},
				// бнкнде - МЕКЭГЪ ЯЙКНМЪРЭ - ДБЮ ОЮДЕФЮ ХЛЕЧР 2 ПЮГМШУ МЮОХЯЮМХЪ, НЯРЮБХКХ ЯРЮРХВРХЯЕЙХ АНКЕЕ ВЮЯРШИ ОЮДЕФ - ДЮРЕКЭМШИ

				// ОПНАКЕЛМНЕ ХЛЪ - яюью
				{"дхлю","длхрпхи"},
				{"дхлш","длхрпхъ"},
				{"дхле","длхрпхч"},
				{"дхлс","длхрпхъ"},
				{"дхлни","длхрпхел"},

			};
	}
}