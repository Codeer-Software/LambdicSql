using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class ClauseConverterAttribute : SymbolConverterMethodAttribute
    {
        /// <summary>
        /// Indent.
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Separator between arguments.By default it uses " ".
        /// </summary>
        public string Separator { get; set; } = " ";

        /// <summary>
        /// It is the predicate attached at the end. By default it is empty.
        /// </summary>
        public string AfterPredicate { get; set; }

        /// <summary>
        /// Convert expression to code parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var name = string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;
            name = name.Trim();

            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.Convert(e)).ToList();
            if (!string.IsNullOrEmpty(AfterPredicate)) args.Add(AfterPredicate);
            
            return new HParts(name, new HParts(args) { Separator = Separator }) { IsFunctional = true, Separator = " ", Indent = Indent };
        }
    }



    //TODO めっちゃリファクタリング

    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class FormatConverterAttribute : SymbolConverterMethodAttribute
    {
        FormatSymbolName _formatSymbolName = new FormatSymbolName();


        /// <summary>
        /// 
        /// </summary>
        public int FirstLineElemetCount = -1;

        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get { return _formatSymbolName.Text; } set { _formatSymbolName.Text = value; } }

        /// <summary>
        /// Convert expression to code parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var array = _formatSymbolName.GetSymbolName(expression, converter);
            if (FirstLineElemetCount == -1)
            {
                return new HParts(array) { EnableChangeLine = false };
            }
            var first = new HParts(array.Take(FirstLineElemetCount)) { EnableChangeLine = false };
            var h = new HParts(first) { IsFunctional = true };
            h.AddRange(array.Skip(FirstLineElemetCount));
            return h;
        }

    }

    class FormatSymbolName
    {
        internal string Text { get; set; }
        
        CodeParts _clauseName;
        CodeParts[] _clauseNameSrc;
        Dictionary<int, int> _argumentIndexAndPartsIndex;
        Dictionary<int, string> _argumentIndexAndSeparators;

        internal bool IsUsedInNamePart(int index)
            => _argumentIndexAndPartsIndex == null || !_argumentIndexAndPartsIndex.ContainsKey(index);


        //やっぱり
        //括弧、それからセパレータ


        internal CodeParts[] GetSymbolName(MethodCallExpression expression, ExpressionConverter converter)
        {
            //hit cache.
            if (_clauseName != null)
            {
                return new CodeParts[] { _clauseName };
            }
            if (_clauseNameSrc != null)
            {
                var array = _clauseNameSrc.ToArray();
                foreach (var e in _argumentIndexAndPartsIndex)
                {
                    var argCore = converter.Convert(expression.Arguments[e.Key]);
                    string sep;
                    if (_argumentIndexAndSeparators.TryGetValue(e.Key, out sep))
                    {
                        array[e.Value] = new HParts(argCore, sep) { EnableChangeLine = false };
                    }
                    else
                    {
                        array[e.Value] = argCore;
                    }
                }
                return array;
            }

            //use method name.
            if (string.IsNullOrEmpty(Text))
            {
                _clauseName = expression.Method.Name.ToUpper();
                return new CodeParts[] { _clauseName };
            }

            //use argument.
            var name = Text.Trim();
            var index = Text.IndexOf("[");
            if (index == -1)
            {
                _clauseName = name;
                return new CodeParts[] { _clauseName };
            }

            var clauseNameSrc = new List<CodeParts>();
            var argumentIndexAndPartsIndex = new Dictionary<int, int>();
            var argumentIndexAndSeparators = new Dictionary<int, string>();
            while (true)
            {
                index = name.IndexOf("[");
                if (index == -1) break;
                var next = name.IndexOf("]");
                if (next == -1)
                {
                    //TODO message.
                    throw new NotSupportedException();
                }

                var nextText = name.Substring(next + 1);
                
                //
                var p1 = name.Substring(0, index);
                if (!string.IsNullOrEmpty(p1))
                {
                    if (argumentIndexAndPartsIndex.Count != 0)
                    {
                        //p1を見ていって、[ ][,][)] 以外になるまで
                        int i = 0;
                        for (; i < p1.Length; i++)
                        {
                            switch (p1[i])
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
                        var sep = p1.Substring(0, i);
                        p1 = p1.Substring(i);
                        argumentIndexAndSeparators[argumentIndexAndPartsIndex.Last().Key] = sep;
                    }
                    clauseNameSrc.Add(p1);
                }

                //ここで引数の特殊文字を解析
                var p2 = name.Substring(index + 1, next - index - 1);

                int x = 0;
                if (!int.TryParse(p2, out x))
                {
                    //TODO message.
                    throw new NotSupportedException();
                }
                argumentIndexAndPartsIndex[x] = clauseNameSrc.Count;
                clauseNameSrc.Add(null);
                name = nextText;
            }

            if (!string.IsNullOrEmpty(name))
            {
                var p1 = name;
                //一つ前が引数なら　そこに着ける
                if (argumentIndexAndPartsIndex.Count != 0)
                {
                    //p1を見ていって、[ ][,][)] 以外になるまで
                    int i = 0;
                    for (; i < p1.Length; i++)
                    {
                        switch (p1[i])
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
                    var sep = p1.Substring(0, i);
                    p1 = p1.Substring(i);
                    argumentIndexAndSeparators[argumentIndexAndPartsIndex.Last().Key] = sep;
                }
                clauseNameSrc.Add(p1);
            }

            _clauseNameSrc = clauseNameSrc.ToArray();
            _argumentIndexAndPartsIndex = argumentIndexAndPartsIndex;
            _argumentIndexAndSeparators = argumentIndexAndSeparators;
            return GetSymbolName(expression, converter);
        }
    }
}
