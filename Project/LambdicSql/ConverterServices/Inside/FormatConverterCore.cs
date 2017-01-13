using LambdicSql.BuilderServices.CodeParts;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;
using System.Collections;
using LambdicSql.Inside.CodeParts;
using System.Collections.ObjectModel;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.BuilderServices;

namespace LambdicSql.ConverterServices.Inside
{
    class FormatConverterCore
    {
        string _format;
        int _firstLineElemetCount = -1;
        List<Code> _partsSrc;
        Dictionary<int, ArgumentInfo> _parameterMappingInfo;

        internal FormatDirection FormatDirection { get; set; } = FormatDirection.Horizontal;

        internal string Format
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
                Init();
            }
        }

        internal int Indent { get; set; }

        public Code Convert(ReadOnlyCollection<Expression> arguments, ExpressionConverter converter)
        {
            //ReadOnlyCollection<Expression>
            var array = ConvertByFormat(arguments, converter);
            if (FormatDirection == FormatDirection.Vertical)
            {
                var first = new HCode(array.Take(_firstLineElemetCount)) { EnableChangeLine = false };
                var v = new VCode(first);
                v.AddRange(1, array.Skip(_firstLineElemetCount));
                return v;
            }
            else
            {
                var first = new HCode(array.Take(_firstLineElemetCount)) { EnableChangeLine = false };
                var h = new HCode(first) { IsFunctional = true };
                h.AddRange(array.Skip(_firstLineElemetCount));
                return h;
            }
        }

        Code[] ConvertByFormat(ReadOnlyCollection<Expression> arguments, ExpressionConverter converter)
        {
            var array = _partsSrc.Select(e => new Code[] { e }).ToArray();
            foreach (var e in _parameterMappingInfo)
            {
                var argExp = arguments[e.Key];

                Code[] code = null;
                if (e.Value.IsArrayExpand)
                {
                    //NewArrayExpressionの場合
                    var newArrayExp = argExp as NewArrayExpression;
                    if (newArrayExp != null)
                    {
                        code = newArrayExp.Expressions.Select(x => converter.Convert(x)).ToArray();
                    }
                    else
                    {
                        var obj = converter.ToObject(newArrayExp);
                        var list = new List<Code>();
                        foreach (var x in (IEnumerable)obj)
                        {
                            list.Add(converter.Convert(x));
                        }
                        code = list.ToArray();
                    }
                    if (!string.IsNullOrEmpty(e.Value.ArrayExpandSeparator))
                    {
                        for (int i = 0; i < code.Length - 1; i++)
                        {
                            code[i] = new HCode(code[i], e.Value.ArrayExpandSeparator) { EnableChangeLine = false };
                        }
                    }
                    if (0 < code.Length)
                    {
                        code[code.Length - 1] = new HCode(code[code.Length - 1], e.Value.Separator) { EnableChangeLine = false };
                    }
                }
                else
                {
                    Code argCore = null;
                    if (e.Value.IsDefineName)
                    {
                        argCore = (string)converter.ToObject(argExp);
                    }
                    else
                    {
                        argCore = converter.Convert(argExp);
                    }
                    if (!string.IsNullOrEmpty(e.Value.Separator))
                    {
                        code = new Code[] { new HCode(argCore, e.Value.Separator) { EnableChangeLine = false } };
                    }
                    else
                    {
                        code = new Code[] { argCore };
                    }
                }
                if (e.Value.IsDirectValue)
                {
                    var customizer = new CustomizeParameterToObject();
                    code = code.Select(x => x.Customize(customizer)).ToArray();
                }
                if (e.Value.IsColumnOnly)
                {
                    var customizer = new CustomizeColumnOnly();
                    code = code.Select(x => x.Customize(customizer)).ToArray();
                }
                array[e.Value.PartsIndex] = code;
            }
            return array.SelectMany(e => e).ToArray();
        }

        void Init()
        {
            var format = Format;
            _firstLineElemetCount = 0;
            _partsSrc = new List<Code>();
            _parameterMappingInfo = new Dictionary<int, ArgumentInfo>();

            while (true)
            {
                var argFirst = format.IndexOf("[");
                if (argFirst == -1) break;
                var argLast = format.IndexOf("]");
                if (argLast == -1)
                {
                    throw new NotSupportedException("Invalid format.");
                }

                var before = format.Substring(0, argFirst);
                before = AnalizeFormat(before);

                var arg = format.Substring(argFirst + 1, argLast - argFirst - 1);
                AnalizeArg(arg);

                format = format.Substring(argLast + 1);
            }

            AnalizeFormat(format);
        }


        class ArgumentInfo
        {
            internal int PartsIndex { get; set; }
            internal string Separator { get; set; }
            internal bool IsArrayExpand { get; set; }
            internal string ArrayExpandSeparator { get; set; }
            internal bool IsDirectValue { get; set; }
            internal bool IsColumnOnly { get; set; }
            internal bool IsDefineName { get; set; }
        }

        void AnalizeArg(string arg)
        {
            var info = new ArgumentInfo();

            //expand array.
            var index = arg.IndexOf('<');
            if (index != -1)
            {
                var end = arg.IndexOf('>');
                if (end == -1) throw new NotSupportedException("Invalid format.");
                var left = arg.Substring(0, index);
                var mid = arg.Substring(index + 1, end - index - 1);
                var right = arg.Substring(end + 1);
                arg = left + right;
                info.IsArrayExpand = true;
                info.ArrayExpandSeparator = mid;
            }

            //direct value.
            if (arg.IndexOf('$') != -1)
            {
                arg = arg.Replace("$", string.Empty);
                info.IsDirectValue = true;
            }

            //define name.
            if (arg.IndexOf('!') != -1)
            {
                arg = arg.Replace("!", string.Empty);
                info.IsDefineName = true;
            }

            //column only.
            if (arg.IndexOf('#') != -1)
            {
                arg = arg.Replace("#", string.Empty);
                info.IsColumnOnly = true;
            }

            if (!int.TryParse(arg.Trim(), out index))
            {
                throw new NotSupportedException("Invalid format.");
            }
            info.PartsIndex = _partsSrc.Count;
            _parameterMappingInfo[index] = info;
            _partsSrc.Add(null);
        }

        string AnalizeFormat(string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                if (_parameterMappingInfo.Count != 0)
                {
                    int i = 0;
                    for (; i < format.Length; i++)
                    {
                        switch (format[i])
                        {
                            case ' ':
                            case ',':
                            case ')':
                                continue;
                            default:
                                break;
                        }
                        break;
                    }
                    var sep = format.Substring(0, i);
                    format = format.Substring(i);
                    _parameterMappingInfo[_parameterMappingInfo.Last().Key].Separator = sep;
                }
                if (_firstLineElemetCount == 0 && format.IndexOf('|') != -1)
                {
                    _firstLineElemetCount = _partsSrc.Count + 1;
                    format = format.Replace("|", string.Empty);
                }
                _partsSrc.Add(format);
            }

            return format;
        }
    }
}
