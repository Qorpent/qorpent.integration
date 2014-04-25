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
	/// 	������� ��� ������� �������
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
		/// 	����������� ��������� ����
		/// </summary>
		/// <param name="candidate"> </param>
		/// <returns> </returns>
		public static string NormalizeFio(string candidate) {
			candidate = GetNormalizedFio(candidate).ToUpper();
			candidate = candidate.Replace('�', '�');
			candidate = candidate.Replace("�����", "�����");
			candidate = candidate.Replace("����", "����");
			candidate = candidate.Replace("�����", "�����");
			candidate = candidate.Replace("�����", "������");
			candidate = candidate.Replace("������", "�������");

	
			
			if(candidate.StartsWith("�������")) {
				candidate = "�" + candidate;
			}
			candidate = candidate.Replace(" �������", " ��������");

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
		/// ������� �������������� ����
		/// </summary>
		public  static IDictionary<string,string> LightNames = new Dictionary<string, string>
			{
				{"����","���������"},
				{"����","���������"},
				{"����","���������"},
				{"����","���������"},
				{"�����","����������"},

				{"�����","������"},
				{"�����","������"},
				{"�����","������"},
				{"�����","������"},
				{"������","�������"},

				{"�����","������"},
				{"�����","������"},
				{"�����","������"},
				{"�����","������"},
				{"������","�������"},

				{"����","�����"},
				{"����","�����"},
				{"����","�����"},
				{"����","�����"},
				{"�����","������"},
				
				{"����","��������"},
				{"����","���������"},
				{"����","���������"},
				{"����","���������"},
				{"�����","����������"},

				{"�����","��������"},
				{"�����","���������"},
				{"�����","���������"},
				{"�����","���������"},
				{"������","����������"},

				{"������","��������"},
				{"�������","���������"},
				{"�������","���������"},
				{"�������","���������"},
				// ������ - ������ �������� - ��� ������ ����� 2 ������ ���������, �������� ������������ ����� ������ ����� - ���������

				// ���������� ��� - ����
				{"����","�������"},
				{"����","�������"},
				{"����","�������"},
				{"����","�������"},
				{"�����","��������"},

			};
	}
}