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
using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.ConverterServices.Inside
{
    class FormatConverterCore
    {
        string _format;
        int _firstLineElemetCount = -1;
        List<ICode> _partsSrc;
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

        public ICode Convert(ReadOnlyCollection<Expression> arguments, ExpressionConverter converter)
        {
            //ReadOnlyCollection<Expression>
            var array = ConvertByFormat(arguments, converter);
            if (FormatDirection == FormatDirection.Vertical)
            {
                var first = new HCode(array.Take(_firstLineElemetCount)) { EnableChangeLine = false };
                var v = new VCode(first) { Indent = Indent };
                v.AddRange(1, array.Skip(_firstLineElemetCount));
                return v;
            }
            else
            {
                var first = new HCode(array.Take(_firstLineElemetCount)) { EnableChangeLine = false };
                var h = new HCode(first) { AddIndentNewLine = true, Indent = Indent };
                h.AddRange(array.Skip(_firstLineElemetCount));
                return h;
            }
        }

        ICode[] ConvertByFormat(ReadOnlyCollection<Expression> arguments, ExpressionConverter converter)
        {
            var array = _partsSrc.Select(e => new ICode[] { e }).ToArray();
            foreach (var e in _parameterMappingInfo)
            {
                var argExp = arguments[e.Key];

                ICode[] code = null;
                if (e.Value.IsArrayExpand)
                {
                    //NewArrayExpressionの場合
                    var newArrayExp = argExp as NewArrayExpression;
                    if (newArrayExp != null)
                    {
                        /*
                        //TODO あー、これは十分ではない
                        //以下が区別がつかない
                        //そもそも区別がつかない・・・
                        //Values(params object[] x)
                        //byte[] bin
                        //Value(bin)
                        if (newArrayExp.Expressions.Count == 1 && newArrayExp.Expressions[0].Type.IsArray)
                        {
                            //TODO refactoring.
                            var obj = converter.ToObject(newArrayExp.Expressions[0]);
                            var list = new List<Code>();
                            foreach (var x in (IEnumerable)obj)
                            {
                                list.Add(converter.Convert(x));
                            }
                            code = list.ToArray();
                        }
                        else
                        {
                            code = newArrayExp.Expressions.Select(x => converter.Convert(x)).ToArray();
                        }
                        */

                        code = newArrayExp.Expressions.Select(x => converter.ConvertToCode(x)).ToArray();
                    }
                    else
                    {
                        var obj = converter.ConvertToObject(argExp);
                        var list = new List<ICode>();
                        foreach (var x in (IEnumerable)obj)
                        {
                            list.Add(converter.ConvertToCode(x));
                        }
                        code = list.ToArray();
                    }
                    if (!string.IsNullOrEmpty(e.Value.ArrayExpandSeparator))
                    {
                        for (int i = 0; i < code.Length - 1; i++)
                        {
                            code[i] = new HCode(code[i], e.Value.ArrayExpandSeparator.ToCode()) { EnableChangeLine = false };
                        }
                    }
                    if (0 < code.Length)
                    {
                        code[code.Length - 1] = new HCode(code[code.Length - 1], e.Value.Separator.ToCode()) { EnableChangeLine = false };
                    }
                }
                else
                {
                    ICode argCore = null;
                    if (e.Value.IsDefineName)
                    {
                        argCore = ((string)converter.ConvertToObject(argExp)).ToCode();
                    }
                    else
                    {
                        argCore = converter.ConvertToCode(argExp);
                    }
                    if (!string.IsNullOrEmpty(e.Value.Separator))
                    {
                        code = new ICode[] { new HCode(argCore, e.Value.Separator.ToCode()) { EnableChangeLine = false } };
                    }
                    else
                    {
                        code = new ICode[] { argCore };
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
            _partsSrc = new List<ICode>();
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
                _partsSrc.Add(format.ToCode());
            }

            return format;
        }
    }
}
