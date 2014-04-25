#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioVariant.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System.Text;

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Внутренняя структура подготовки фамилий
	/// </summary>
	public class FioVariant {
		/// <summary>
		/// 	Создает вариант
		/// </summary>
		/// <param name="fio"> </param>
		/// <param name="weight"> </param>
		/// <param name="padezh"> </param>
		/// <param name="onlyLastName"> </param>
		/// <param name="addNameUsed"> </param>
		/// <param name="abbrevated"> </param>
		/// <param name="lastNameFirst"> </param>
		public FioVariant(string fio, int weight, Padezh padezh, bool onlyLastName, bool addNameUsed, bool abbrevated,
		                  bool lastNameFirst) {
			Padezh = padezh;
			Fio = fio;
			Weight = weight;
			Abbrevated = abbrevated;
			LastNameFirst = lastNameFirst;
			AddNameUsed = addNameUsed;
			OnlyLastName = onlyLastName;
		}


		/// <summary>
		/// 	Падеж
		/// </summary>
		public Padezh Padezh { get; set; }

		/// <summary>
		/// 	Написание фамилии
		/// </summary>
		public string Fio { get; set; }

		/// <summary>
		/// Возвращает ФИО в читабельном кейсе
		/// </summary>
		public string FioCased {
			get {
				bool starter = true;
				var sb = new StringBuilder();
				foreach (var c in Fio) {
					if (c == ' '  || c == '.' || c=='-') {
						starter = true;
						sb.Append(c);
						continue;
					}
					if (starter) {
						sb.Append(c);
						starter = false;
					}
					else {
						sb.Append(c.ToString().ToLower());
					}
				}
				return sb.ToString();
			}
		}

		/// <summary>
		/// 	Вес
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// 	Является абревиатурой?
		/// </summary>
		public bool Abbrevated { get; set; }


		/// <summary>
		/// 	Фамилия идет первой?
		/// </summary>
		public bool LastNameFirst { get; set; }

		/// <summary>
		/// 	Использовано отчество?
		/// </summary>
		public bool AddNameUsed { get; set; }


		/// <summary>
		/// 	Только фамилия?
		/// </summary>
		public bool OnlyLastName { get; set; }

		/// <summary>
		/// </summary>
		/// <param name="other"> </param>
		/// <returns> </returns>
		public bool Equals(FioVariant other) {
			return Padezh.Equals(other.Padezh) && string.Equals(Fio, other.Fio) && Weight == other.Weight;
		}

		/// <summary>
		/// </summary>
		/// <returns> </returns>
		public override int GetHashCode() {
			unchecked {
				var hashCode = Padezh.GetHashCode();
				hashCode = (hashCode*397) ^ (Fio != null ? Fio.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ Weight;
				return hashCode;
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="obj"> </param>
		/// <returns> </returns>
		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			return obj is FioVariant && Equals((FioVariant) obj);
		}
	}
}