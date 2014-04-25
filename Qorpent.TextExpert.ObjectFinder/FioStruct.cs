#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioStruct.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System;
using System.Linq;

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	��������� ���
	/// </summary>
	public class FioStruct {
		/// <summary>
		/// </summary>
		public FioStruct() {
			Name = "";
			LastName = "";
			AddName = "";
		}

		/// <summary>
		/// ���������� ����������� ��� ����������� ���������
		/// </summary>
		/// <param name="candidate"></param>
		internal FioStruct(string candidate) {
		    try
		    {
		        var split =
		            candidate.Split(' ').Select(x => x.Trim().ToUpper()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
		        LastName = split[0];
		        if (split.Length > 1)
		        {
		            Name = split[1];
		        }
		        if (split.Length > 2)
		        {
		            AddName = split[2];
		        }
		    }
		    catch (Exception e)
		    {
		        this.Error = e;
		        this.Name = candidate;
		    }
		}
        /// <summary>
        /// 
        /// </summary>
	    public Exception Error { get; set; }

	    /// <summary>
		/// 	���
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 	�������
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// 	��������
		/// </summary>
		public string AddName { get; set; }

		/// <summary>
		/// 	���������� ������ �����
		/// </summary>
		/// <param name="part"> </param>
		/// <returns> </returns>
		/// <exception cref="Exception"></exception>
		public string GetPart(FioStructPart part) {
			switch (part) {
				case FioStructPart.Name:
					return Name;
				case FioStructPart.LastName:
					return LastName;
				case FioStructPart.AddName:
					return AddName;
				default:
					throw new Exception("Unknown part " + part);
			}
		}

		/// <summary>
		/// 	������������� ����� ��� �� ���� �����
		/// </summary>
		/// <param name="part"> </param>
		/// <param name="value"> </param>
		/// <exception cref="Exception"></exception>
		public void SetPart(FioStructPart part, string value) {
			switch (part) {
				case FioStructPart.Name:
					Name = value;
					break;
				case FioStructPart.LastName:
					LastName = value;
					break;
				case FioStructPart.AddName:
					AddName = value;
					break;
				default:
					throw new Exception("Unknown part " + part);
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="other"> </param>
		/// <returns> </returns>
		protected bool Equals(FioStruct other) {
			return string.Equals(Name, other.Name) && string.Equals(LastName, other.LastName) &&
			       string.Equals(AddName, other.AddName);
		}

		/// <summary>
		/// </summary>
		/// <returns> </returns>
		public override int GetHashCode() {
			unchecked {
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ (AddName != null ? AddName.GetHashCode() : 0);
				return hashCode;
			}
		}

		/// <summary>
		/// 	���������� ��� � ���������
		/// </summary>
		/// <param name="val"> </param>
		/// <returns> </returns>
		public static implicit operator FioStruct(string val) {
			val = Utils.NormalizeFio(val);
			var result = new FioStruct(val);
		    return result;

		}

		/// <summary>
		/// 	���������� ��������������� ������ �� ����� �����������
		/// </summary>
		/// <param name="adv"> </param>
		/// <param name="abbr"> </param>
		/// <param name="lastfirst"> </param>
		/// <returns> </returns>
		public string ToString(bool adv, bool abbr, bool lastfirst) {
			try {
				var f = LastName;
				var n = abbr ? (string.IsNullOrWhiteSpace(Name) ? "" : (Name[0] + ".")) : Name;

				var o = adv ? (string.IsNullOrWhiteSpace(AddName) ? "" : (abbr ? AddName[0] + "." : " " + AddName)) : "";
				if (lastfirst) {
					return f + " " + n + o;
				}
				else {
					return n + o + " " + f;
				}
			}
			catch (Exception e) {
				throw new Exception(string.Format("cannot tostring '{0}' '{1}' '{2}'", LastName, Name, AddName), e);
			}
		}

		/// <summary>
		/// 	����������, ����� �� �������� ������ <see cref="T:System.Object" /> �������� ������� <see cref="T:System.Object" />.
		/// </summary>
		/// <returns> true, ���� �������� ������ ����� �������� �������; � ��������� ������ � false. </returns>
		/// <param name="obj"> ������, ������� ��������� �������� � ������� ��������. </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj)) {
				return false;
			}
			if (ReferenceEquals(this, obj)) {
				return true;
			}
			if (obj.GetType() != GetType()) {
				return false;
			}
			return Equals((FioStruct) obj);
		}
	}
}