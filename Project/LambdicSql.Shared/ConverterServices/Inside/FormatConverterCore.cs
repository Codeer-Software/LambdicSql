﻿using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using System;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace LambdicSql.ConverterServices.Inside
{
    class FormatConverterCore
    {
        class ArgumentInfo
        {
            internal int PartsIndex { get; set; }
            internal ICode Separator { get; set; }
            internal bool IsArrayExpand { get; set; }
            internal string ArrayExpandSeparator { get; set; }
            internal bool IsDirectValue { get; set; }
            internal bool IsColumnOnly { get; set; }
            internal bool IsSpecialName { get; set; }
            internal bool IsVariableName { get; set; }
            internal bool IsObjectToParameter { get; set; }
        }

        string _format;
        int _firstLineElemetCount = -1;
        List<ICode> _partsSrc;
        Dictionary<int, List<ArgumentInfo>> _parameterMappingInfo;

        internal int Indent { get; set; }

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
                AnalyzeFormat();
            }
        }

        internal bool VanishIfEmptyParams { get; set; }

        public ICode Convert(ReadOnlyCollection<Expression> arguments, ExpressionConverter converter)
        {
            var code = ConvertByFormat(arguments, converter);
            if (code == null) return string.Empty.ToCode();
            return Layout(code);
        }

        ICode[] ConvertByFormat(ReadOnlyCollection<Expression> arguments, ExpressionConverter converter)
        {
            ICode[][] allCodes = new ICode[_partsSrc.Count][];
            for (int i = 0; i < allCodes.Length; i++)
            {
                allCodes[i] = new ICode[] { _partsSrc[i] };
            }

            //intert parameter.
            bool isEmpty = true;
            int selectManyCount = allCodes.Length;
            foreach (var e in _parameterMappingInfo)
            {
                if (arguments.Count <= e.Key) throw new NotSupportedException("Invalid format.");

                var argExp = arguments[e.Key];
                foreach (var argumentInfo in e.Value)
                {

                    var code = argumentInfo.IsArrayExpand ?
                        ConvertExpandArrayArgument(converter, argumentInfo, argExp) :
                        new ICode[] { ConvertSingleArgument(converter, argumentInfo, argExp) };
                    if (code == null) return null;

                    if (argumentInfo.IsDirectValue)
                    {
                        var customizer = new CustomizeParameterToObject();
                        for (int i = 0; i < code.Length; i++) code[i] = code[i].Accept(customizer);
                    }

                    if (argumentInfo.IsColumnOnly)
                    {
                        var customizer = new CustomizeColumnOnly();
                        for (int i = 0; i < code.Length; i++) code[i] = code[i].Accept(customizer);
                    }

                    if (argumentInfo.IsSpecialName)
                    {
                        var customizer = new CustomizeParameterToDefineName();
                        for (int i = 0; i < code.Length; i++) code[i] = code[i].Accept(customizer);
                    }

                    if (argumentInfo.IsVariableName)
                    {
                        var customizer = new CustomizeParameterToVariableName();
                        for (int i = 0; i < code.Length; i++) code[i] = code[i].Accept(customizer);
                    }

                    if (argumentInfo.IsObjectToParameter)
                    {
                        var customizer = new CustomizeObjectToParameter();
                        for (int i = 0; i < code.Length; i++) code[i] = code[i].Accept(customizer);
                    }

                    allCodes[argumentInfo.PartsIndex] = code;

                    //adjust count.
                    selectManyCount = selectManyCount - 1 + code.Length;

                    if (VanishIfEmptyParams && isEmpty)
                    {
                        for (int i = 0; i < code.Length; i++)
                        {
                            if (!code[i].IsEmpty) isEmpty = false;
                        }
                    }
                }
            }
            if (VanishIfEmptyParams && isEmpty) return null;
            return SelectMany(allCodes, selectManyCount);
        }

        ICode[] ConvertExpandArrayArgument(ExpressionConverter converter, ArgumentInfo argumentInfo, Expression argExp)
        {
            ICode[] code;
            var newArrayExp = argExp as NewArrayExpression;
            if (newArrayExp != null)
            {
                code = new ICode[newArrayExp.Expressions.Count];
                for (int i = 0; i < newArrayExp.Expressions.Count; i++)
                {
                    code[i] = converter.ConvertToCode(newArrayExp.Expressions[i]);
                }
            }
            else
            {
                var obj = converter.ConvertToObject(argExp);
                var list = new List<ICode>();
                foreach (var e in (IEnumerable)obj)
                {
                    list.Add(converter.ConvertToCode(e));
                }
                code = list.ToArray();
            }

            if (!string.IsNullOrEmpty(argumentInfo.ArrayExpandSeparator))
            {
                var isEmpty = true;
                for (int i = code.Length - 1; 0 <= i; i--)
                {
                    var currentIsEmpty = code[i].IsEmpty;
                    if (!isEmpty && !currentIsEmpty)
                    {
                        code[i] = new HCode(code[i], argumentInfo.ArrayExpandSeparator.ToCode()) { EnableChangeLine = false };
                    }
                    if (isEmpty) isEmpty = currentIsEmpty;
                }
            }

            if (0 < code.Length && argumentInfo.Separator != null)
            {
                code[code.Length - 1] = new HCode(code[code.Length - 1], argumentInfo.Separator) { EnableChangeLine = false };
            }

            return code;
        }

        static ICode ConvertSingleArgument(ExpressionConverter converter, ArgumentInfo argumentInfo, Expression argExp)
        {
            var argCore = converter.ConvertToCode(argExp);
            return argumentInfo.Separator == null ?
                     argCore : new HCode(argCore, argumentInfo.Separator) { EnableChangeLine = false };
        }

        ICode Layout(ICode[] code)
        {
            ICode[] front, back;
            TakeAndSkip(code, _firstLineElemetCount, out front, out back);

            if (FormatDirection == FormatDirection.Vertical)
            {
                var mustFirstLine = new HCode(front) { EnableChangeLine = false };
                var layout = new VCode(mustFirstLine) { Indent = Indent };

                //after line, add indent.
                for (int i = 0; i < back.Length; i++)
                {
                    layout.Add(new HCode(back[i]) { Indent = 1 });
                }
                return layout;
            }
            else
            {
                var mustFirstLine = new HCode(front) { EnableChangeLine = false };
                var layout = new HCode(mustFirstLine) { AddIndentNewLine = true, Indent = Indent };
                layout.AddRange(back);
                return layout;
            }
        }

        void AnalyzeFormat()
        {
            var format = Format;
            _firstLineElemetCount = 0;
            _partsSrc = new List<ICode>();
            _parameterMappingInfo = new Dictionary<int, List<ArgumentInfo>>();

            ArgumentInfo info = null;
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
                before = AnalizeFormat(before, info);

                var arg = format.Substring(argFirst + 1, argLast - argFirst - 1);
                info = AnalizeArg(arg);

                format = format.Substring(argLast + 1);
            }

            AnalizeFormat(format, info);
        }

        ArgumentInfo AnalizeArg(string arg)
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

            //special name.
            if (arg.IndexOf('!') != -1)
            {
                arg = arg.Replace("!", string.Empty);
                info.IsSpecialName = true;
            }

            //variable name.
            if (arg.IndexOf('*') != -1)
            {
                arg = arg.Replace("*", string.Empty);
                info.IsVariableName = true;
            }

            //column only.
            if (arg.IndexOf('#') != -1)
            {
                arg = arg.Replace("#", string.Empty);
                info.IsColumnOnly = true;
            }
            
            //object to string.
            if (arg.IndexOf('%') != -1)
            {
                arg = arg.Replace("%", string.Empty);
                info.IsObjectToParameter = true;
            }

            if (!int.TryParse(arg.Trim(), out index))
            {
                throw new NotSupportedException("Invalid format.");
            }
            info.PartsIndex = _partsSrc.Count;

            if (!_parameterMappingInfo.TryGetValue(index, out var argInfos))
            {
                argInfos = new List<ArgumentInfo>();
                _parameterMappingInfo.Add(index, argInfos);
            }
            argInfos.Add(info);
            _partsSrc.Add(null);
            return info;
        }

        string AnalizeFormat(string format, ArgumentInfo beforeArgument)
        {
            if (string.IsNullOrEmpty(format)) return format;

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
                beforeArgument.Separator = sep.ToCode();
            }
            if (_firstLineElemetCount == 0 && format.IndexOf('|') != -1)
            {
                _firstLineElemetCount = _partsSrc.Count + 1;
                format = format.Replace("|", string.Empty);
            }
            _partsSrc.Add(format.Replace("&left;","[").Replace("&right;", "]").ToCode());

            return format;
        }

        static T[] SelectMany<T>(T[][] allCodes, int count)
        {
            var dst = new T[count];
            int i = 0;
            foreach (var x in allCodes)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    dst[i++] = x[j];
                }
            }
            return dst;
        }

        static void TakeAndSkip<T>(T[] src, int count, out T[] front, out T[] back)
        {
            front = new T[count];
            back = new T[src.Length - count];
            for (int i = 0; i < front.Length; i++)
            {
                front[i] = src[i];
            }
            for (int i = 0; i < back.Length; i++)
            {
                back[i] = src[i + count];
            }
        }
    }
}
