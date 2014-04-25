#region LICENSE

// Copyright 2012-2013 Media Technology LTD 
// Solution: Qorpent.TextExpert
// Original file : FioTextParser.cs
// Project: Comdiv.TextExpert.ObjectFinder
// This code cannot be used without agreement from 
// Media Technology LTD 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Qorpent.TextExpert.ObjectFinder {
	/// <summary>
	/// 	Фильтр, очищающий текст от всего, кроме кандидатов на ФИО
	/// </summary>
	public sealed class FioTextParser {
		/// <summary>
		/// 	Фильтрует входной текст, оставляя только кандидатов на ФИО
		/// </summary>
		/// <param name="sourceString"> </param>
		/// <returns> </returns>
		public string Filter(string sourceString) {
			string postionstring;
			return Filter(sourceString, out postionstring);
		}

		/// <summary>
		/// 	Фильтрует входной текст, оставляя только кандидатов на ФИО
		/// </summary>
		/// <param name="sourceString"> </param>
		/// <param name="postionstring"> справочная строка для позиций токенов </param>
		/// <returns> </returns>
		public string Filter(string sourceString, out string postionstring) {
			postionstring = "";
			if (string.IsNullOrEmpty(sourceString)) {
				return "";
			}
			_src = sourceString;
			_result = new StringBuilder();
			_positionresult = new StringBuilder();
			_wasws = true;
			_wasstart = false;
			_line = 1;
			_col = 0;
			_buffer = "";
			_bufferstartline = 0;
			_bufferstartcol = 0;
			_bufferstartidx = 0;
			for (_idx = 0; _idx < _src.Length; _idx++) {
				_col++;
				_cur = _src[_idx];
				if (_cur == '\r' || _cur == '\n') {
					_col = 0;
					var pre = '\0';
					if (0 != _idx) {
						pre = _src[_idx - 1];
					}
					if (_cur == '\r') {
						_line++;
						_col = 0;
					}
					if (_cur == '\n') {
						if (pre != '\r') {
							_line++;
							_col = 0;
						}
					}
				}

				if (_wasstart) {
					if (char.IsLetter(_cur) || '-' == _cur) {
						_buffer += _cur;
						continue;
					}
					if (char.IsWhiteSpace(_cur) || ',' == _cur || ';' == _cur || ')' == _cur || '(' == _cur) {
						if (_buffer.Length > 1) {
							_positionresult.Append(_bufferstartline + ":" + _bufferstartcol + ":" + _bufferstartidx);
							_result.Append(_buffer);
							if (',' == _cur || ';' == _cur) {
								_result.Append(',');
								_positionresult.Append(',');
							}
							else {
								_result.Append(" ");
								_positionresult.Append(" ");
							}
						}
						Terminate();
						continue;
					}
					if ('.' == _cur) {
						if (_buffer.Length == 1) {
							_positionresult.Append(_bufferstartline + ":" + _bufferstartcol + ":" + _bufferstartidx);
							_result.Append(_buffer);
							_result.Append(". ");
							_positionresult.Append(" ");
						}
						else {
							_positionresult.Append(_bufferstartline + ":" + _bufferstartcol + ":" + _bufferstartidx);
							_result.Append(_buffer);
							_result.Append(";");
							_positionresult.Append(";");
						}
						Terminate();
						continue;
					}
					if ('!' == _cur || '?' == _cur) {
						_positionresult.Append(_bufferstartline + ":" + _bufferstartcol + ":" + _bufferstartidx);
						_result.Append(_buffer);
						_result.Append(";");
						_positionresult.Append(";");
					}
					Terminate();
				}
				else if (_wasws && char.IsUpper(_cur)) {
					_buffer += _cur;
					_wasstart = true;
					_bufferstartline = _line;
					_bufferstartcol = _col;
					_bufferstartidx = _idx;
					if (_wasdelimet) {
						_result.Append(',');
						_positionresult.Append(",");
						_wasdelimet = false;
					}
				}
				else if (_wasws && char.IsWhiteSpace(_cur)) {}
				else {
					_wasdelimet = true;
				}
			}
			if (_wasstart) {
				_positionresult.Append(_bufferstartline + ":" + _bufferstartcol + ":" + _bufferstartidx);
				_result.Append(_buffer);
				Terminate();
			}
			postionstring = _positionresult.ToString();
			return _result.ToString();
		}

		/// <summary>
		/// 	Возвращает кандидатов на фамилии в стек
		/// </summary>
		/// <param name="source"> </param>
		/// <returns> </returns>
		public IEnumerable<string> Execute(string source) {
			return CollectFios(Filter(source));
		}


		/// <summary>
		/// 	Возвращает кандидатов на фамилии в стек
		/// </summary>
		/// <param name="source"> </param>
		/// <returns> </returns>
		public IEnumerable<Tuple<string, IEnumerable<TextPosition>>> ExecuteEx(string source) {
			string postext;
			var result = Filter(source, out postext);
			return CollectFiosEx(result, postext);
		}

		/// <summary>
		/// 	Собирает кандидатов на проверку (уникальные по тексту)
		/// </summary>
		/// <param name="filteredText"> </param>
		/// <returns> </returns>
		public IEnumerable<string> CollectFios(string filteredText) {
			var result = InnerCollect(filteredText).ToArray();
			return result;
			//.Where(x => !result.Any(y => y!=x && y.StartsWith(x))); -- нет пока уверенности что эта эвристика верна
		}

		/// <summary>
		/// 	Собирает кандидатов на проверку (уникальные по тексту)
		/// </summary>
		/// <param name="filteredText"> </param>
		/// <param name="postionsText"> </param>
		/// <returns> </returns>
		public IEnumerable<Tuple<string, IEnumerable<TextPosition>>> CollectFiosEx(string filteredText, string postionsText) {
			var result = InnerCollectEx(filteredText, postionsText).ToArray();
			return result;
			//.Where(x => !result.Any(y => y!=x && y.StartsWith(x))); -- нет пока уверенности что эта эвристика верна
		}

		private static IEnumerable<string> InnerCollect(string filteredText) {
			var sources = filteredText.Split(',', ';');
			var existed = new List<string>();
			foreach (var parts in from source in sources
			                      select source.Trim()
			                      into trimmed where !string.IsNullOrWhiteSpace(trimmed) select trimmed.Split(' ')) {
				for (var s = 0; s < parts.Length; s++) {
					for (var c = 0; c < 3; c++) {
						if ((s + c) >= parts.Length) {
							continue;
						}
						var b = "";
						for (var i = s; i <= s + c; i++) {
							if (!string.IsNullOrEmpty(b)) {
								b += " ";
							}
							b += parts[i];
						}
						b = Utils.NormalizeFio(b);

						if (Regex.IsMatch(b, @"^\w\.(\s\w\.)?$", RegexOptions.Compiled)) {
							continue;
						}
						if (existed.Contains(b)) {
							continue;
						}
						existed.Add(b);

						yield return b;
					}
				}
			}
		}

		private static IEnumerable<Tuple<string, IEnumerable<TextPosition>>> InnerCollectEx(string filteredText,
		                                                                                    string positionsText) {
			var sources = filteredText.Split(',', ';');
			var positions = positionsText.Split(',', ';');
			var existed = new Dictionary<string, IList<TextPosition>>();
			var texts = (from source in sources
			             select source.Trim()
			             into trimmed where !string.IsNullOrWhiteSpace(trimmed) select
				             trimmed.Split(' ')).ToArray();
			var poses = (from source in positions
			             select source.Trim()
			             into trimmed where !string.IsNullOrWhiteSpace(trimmed) select
				             trimmed.Split(' ').Select(x => (TextPosition) x).ToArray()).ToArray();
			var textpositionset = new List<Tuple<string[], TextPosition[]>>();
			for (var i = 0; i < texts.Length; i++) {
				textpositionset.Add(new Tuple<string[], TextPosition[]>(texts[i], poses[i]));
			}

			foreach (var parts in textpositionset) {
				for (var s = 0; s < parts.Item1.Length; s++) {
					for (var c = 0; c < 3; c++) {
						if ((s + c) >= parts.Item1.Length) {
							continue;
						}
						var b = "";
						for (var i = s; i <= s + c; i++) {
							if (!string.IsNullOrEmpty(b)) {
								b += " ";
							}
							b += parts.Item1[i];
						}
						b = Utils.NormalizeFio(b);

						if (Regex.IsMatch(b, @"^\w\.(\s\w\.)?$", RegexOptions.Compiled)) {
							continue;
						}
						var pos = parts.Item2[s];
						if (!existed.ContainsKey(b)) {
							existed[b] = new List<TextPosition>();
						}
						existed[b].Add(pos);
					}
				}
			}

			return existed.Select(x => new Tuple<string, IEnumerable<TextPosition>>(x.Key, x.Value));
		}

		private void Terminate() {
			_buffer = "";
			_wasstart = false;
			_wasws = true;
			_wasdelimet = false;
		}

		/// <summary>
		/// 	Буфер текущего слова
		/// </summary>
		private string _buffer;

		private int _bufferstartcol;
		private int _bufferstartidx;
		private int _bufferstartline;

		/// <summary>
		/// 	Текущая колонка
		/// </summary>
		private int _col;

		/// <summary>
		/// 	Текущий символ
		/// </summary>
		private char _cur;

		/// <summary>
		/// 	Текущий индекс символа
		/// </summary>
		private int _idx;

		/// <summary>
		/// 	Текущая строка
		/// </summary>
		private int _line;

		/// <summary>
		/// 	Результат с позициями
		/// </summary>
		private StringBuilder _positionresult;

		/// <summary>
		/// 	Отфильтрованный текст с разделителями
		/// </summary>
		private StringBuilder _result;

		/// <summary>
		/// 	Исходный текст
		/// </summary>
		private string _src;

		/// <summary>
		/// 	Признак наличия явного разделителя между словами или слова с небуквенными символами
		/// </summary>
		private bool _wasdelimet;

		/// <summary>
		/// 	Признак нахождения внутри слова
		/// </summary>
		private bool _wasstart;


		/// <summary>
		/// 	Признак того что был знаковый пробельный символ
		/// </summary>
		private bool _wasws;
	}
}