#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTransformerSqlFunctions.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace Qorpent.TextExpert.ObjectFinder.SqlSupport {
	/// <summary>
	/// 	������� ��� ���������� � SQL
	/// </summary>
	public static partial class FioTransformerSqlFunctions {
		/// <summary>
		/// 	���������� �������� ��������� ������� � ����
		/// </summary>
		/// <param name="stringtotest"> ������� ��������� </param>
		/// <param name="lastNameWeight"> ��� ���������� ������� </param>
		/// <returns> </returns>
		[SqlFunction(FillRowMethodName = "FillGetFioVariants", Name = "fio_get_variants", IsDeterministic = false,
			DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None,
			TableDefinition =
				"variant nvarchar(255), weight int, padezh nvarchar(255), onlylastname bit, addnameused bit, abbrevated bit, lastnamefirst bit"
			)]
		public static IEnumerable GetFioVariants(SqlString stringtotest, SqlInt32 lastNameWeight) {
			if (stringtotest.IsNull) {
				yield break;
			}
			var options = FioTransformerOptions.Default;
			if (!lastNameWeight.IsNull) {
				options.SelfLastNameWeight = lastNameWeight.Value;
			}
			var result =
				new FioTransformer().GetVariants(stringtotest.Value, options)
					.GroupBy(x => x.Fio).Select(
						g =>
						new FioVariant(g.Key, g.Max(x => x.Weight), g.Min(x => x.Padezh), g.Min(x => x.OnlyLastName),
						               g.Min(x => x.AddNameUsed), g.Min(x => x.Abbrevated), g.Min(x => x.LastNameFirst)));
			foreach (var variant in result) {
				yield return variant;
			}
		}

		/// <summary>
		/// 	���������� �������� ��������� ������� � ���� � ����������� ������ ��� ���� ������
		/// </summary>
		/// <param name="stringtotest"> ������� ��������� </param>
		/// <param name="usefamilyonly"> ������������ ������ ������� </param>
		/// <param name="lastfirstonly"> ������������ ������ ������� � ������ </param>
		/// <param name="mergeabbr"> ������������ � ����������� ��� ����� </param>
		/// <returns> </returns>
		[SqlFunction(FillRowMethodName = "FillGetFioVariants", Name = "fio_get_variants_ex", IsDeterministic = false,
			DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None,
			TableDefinition =
				"variant nvarchar(255), weight int, padezh nvarchar(255), onlylastname bit, addnameused bit, abbrevated bit, lastnamefirst bit"
			)]
		public static IEnumerable GetFioVariantsEx(SqlString stringtotest, SqlBoolean usefamilyonly, SqlBoolean lastfirstonly,
		                                           SqlBoolean mergeabbr) {
			if (stringtotest.IsNull) {
				yield break;
			}
			var options = FioTransformerOptions.Default;
			if (usefamilyonly.IsTrue) {
				options.SelfLastNameWeight = 60;
				
			}
            options.NotDotedAbbrevations = mergeabbr.IsTrue;
		    options.IgnoreErrors = true;
			var result =
				new FioTransformer().GetVariants(stringtotest.Value, options)
					.Where(x => lastfirstonly.IsFalse || x.LastNameFirst)
					.GroupBy(x => x.Fio).Select(
						g =>
						new FioVariant(g.Key, g.Max(x => x.Weight), g.Min(x => x.Padezh), g.Min(x => x.OnlyLastName),
						               g.Min(x => x.AddNameUsed), g.Min(x => x.Abbrevated), g.Min(x => x.LastNameFirst)));
			foreach (var variant in result) {
				yield return variant;
			}
		}

		/// <summary>
		/// 	���������� �������� ��������� ������� � ����
		/// </summary>
		/// <param name="stringtotest"> �������� ����� </param>
		/// <returns> </returns>
		[SqlFunction(FillRowMethodName = "FillTextNameVariants", Name = "fio_get_text_variants", IsDeterministic = false,
			DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None,
			TableDefinition = "variant nvarchar(255)")]
		public static IEnumerable GetFioTextVariants(SqlString stringtotest) {
			if (stringtotest.IsNull) {
				yield break;
			}
			var result = new FioTextParser().Execute(stringtotest.Value);
			foreach (var r in result) {
				yield return r;
			}
		}

		/// <summary>
		/// 	���������� �������� ��������� ������� � ���� � ��������� ���������� � ������� ����������
		/// </summary>
		/// <param name="stringtotest"> �������� ����� </param>
		/// <returns> </returns>
		[SqlFunction(FillRowMethodName = "FillTextNameVariantsEx", Name = "fio_get_text_variants_ex", IsDeterministic = false,
			DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None,
			TableDefinition = "variant nvarchar(255), cnt int, posstr nvarchar(max)")]
		public static IEnumerable GetFioTextVariantsEx(SqlString stringtotest) {
			if (stringtotest.IsNull) {
				yield break;
			}
			IEnumerable<Tuple<string, IEnumerable<TextPosition>>> result;
			try {
				result = new FioTextParser().ExecuteEx(stringtotest.Value);
			}
			catch (Exception ex) {
				throw new Exception("������ � �������: " + stringtotest.Value, ex);
			}
			foreach (var r in result) {
				yield return r;
			}
		}


		/// <summary>
		/// 	��������� ������� ������� ���������
		/// </summary>
		public static void FillTextNameVariants(Object obj, out SqlString variant) {
			var v = (string) obj;
			variant = v;
		}

		/// <summary>
		/// 	��������� ������� ������� ��������� (�����������
		/// </summary>
		public static void FillTextNameVariantsEx(Object obj, out SqlString variant, out SqlInt32 cnt, out SqlString posstr) {
			var v = (Tuple<string, IEnumerable<TextPosition>>) obj;
			variant = v.Item1;
			cnt = v.Item2.Count();
			var str_ = v.Item2.Select(x => x.ToString()).ToArray();
			posstr = string.Join("; ", str_);
		}

		/// <summary>
		/// 	��������� ������� ������� ���������
		/// </summary>
		public static void FillGetFioVariants(Object obj, out SqlString variant, out SqlInt32 weight, out SqlString padezh,
		                                      out SqlBoolean onlylastname, out SqlBoolean addnameused,
		                                      out SqlBoolean abbrevated, out SqlBoolean lastnamefirst) {
			var v = (FioVariant) obj;
			variant = v.Fio.ToUpper();
			weight = v.Weight;
			padezh = v.Padezh.ToString();
			onlylastname = v.OnlyLastName;
			addnameused = v.AddNameUsed;
			abbrevated = v.Abbrevated;
			lastnamefirst = v.LastNameFirst;
		}
	}
}