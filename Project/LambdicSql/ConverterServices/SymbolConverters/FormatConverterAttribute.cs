using LambdicSql.BuilderServices.CodeParts;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    //TODO params展開の印
    //TODO 縦対応 → まあ、引数だけの話かな。そこを区切ればいいんじゃないかなー
    //TODO 直値対応 引数にそのような情報を付ける
    //TODO カラムだけとかね。
    //そしたらいらなくなるかな？

    /// <summary>
    /// 
    /// </summary>
    public enum FormatDirection
    {
        /// <summary>
        /// 
        /// </summary>
        Horizontal,

        /// <summary>
        /// 
        /// </summary>
        Vertical
    }

    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class FormatConverterAttribute : SymbolConverterMethodAttribute
    {
        string _format;
        int _firstLineElemetCount = -1;
        List<Code> _partsSrc;
        Dictionary<int, int> _argumentIndexAndPartsIndex;
        Dictionary<int, string> _argumentIndexAndSeparators;

        /// <summary>
        /// 
        /// </summary>
        public FormatDirection FormatDirection { get; set; } = FormatDirection.Horizontal;

        /// <summary>
        /// Format.
        /// </summary>
        public string Format
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

        /// <summary>
        /// Indent.
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var array = ConvertByFormat(expression, converter);
            if (_firstLineElemetCount == -1)
            {
                return new HCode(array) { EnableChangeLine = false };
            }
            var first = new HCode(array.Take(_firstLineElemetCount)) { EnableChangeLine = false };
            var h = new HCode(first) { IsFunctional = true };
            h.AddRange(array.Skip(_firstLineElemetCount));
            return h;
        }
        
        internal Code[] ConvertByFormat(MethodCallExpression expression, ExpressionConverter converter)
        {
            var array = _partsSrc.ToArray();
            foreach (var e in _argumentIndexAndPartsIndex)
            {
                var argCore = converter.Convert(expression.Arguments[e.Key]);
                string sep;
                if (_argumentIndexAndSeparators.TryGetValue(e.Key, out sep))
                {
                    array[e.Value] = new HCode(argCore, sep) { EnableChangeLine = false };
                }
                else
                {
                    array[e.Value] = argCore;
                }
            }
            return array;
        }

        void Init()
        {
            var format = Format;
            _firstLineElemetCount = -1;
            _partsSrc = new List<Code>();
            _argumentIndexAndPartsIndex = new Dictionary<int, int>();
            _argumentIndexAndSeparators = new Dictionary<int, string>();

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

        void AnalizeArg(string arg)
        {
            int index = 0;
            if (!int.TryParse(arg, out index))
            {
                throw new NotSupportedException("Invalid format.");
            }
            _argumentIndexAndPartsIndex[index] = _partsSrc.Count;
            _partsSrc.Add(null);
        }

        string AnalizeFormat(string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                if (_argumentIndexAndPartsIndex.Count != 0)
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
                    _argumentIndexAndSeparators[_argumentIndexAndPartsIndex.Last().Key] = sep;
                }
                if (_firstLineElemetCount == -1 && format.IndexOf('|') != -1)
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
