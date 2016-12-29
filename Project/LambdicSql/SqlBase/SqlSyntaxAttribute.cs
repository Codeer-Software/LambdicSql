using LambdicSql.Inside;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public abstract class SqlSyntaxConverterAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(IExpressionConverter converter, MemberExpression exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(object obj)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxFuncAttribute : SqlSyntaxConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Separator { get; set; } = ", ";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var index = method.SkipMethodChain(0);
            var args = method.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? method.Method.Name.ToUpper() : Name;

            var hArgs = new HText(args) { Separator = Separator }.ConcatToBack(")");
            return new HText(Line(name, "("), hArgs) { IsFunctional = true };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxTopAttribute : SqlSyntaxConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return LineSpace(method.Method.Name.ToUpper(), args[0].Customize(new CustomizeParameterToObject()));
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxClauseAttribute : SqlSyntaxConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public int Indent { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Separator { get; set; } = " ";

        /// <summary>
        /// 
        /// </summary>
        public string AfterPredicate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var index = method.SkipMethodChain(0);
            var args = method.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? method.Method.Name.ToUpper() : Name;

            var elements = new List<ExpressionElement>();
            elements.AddRange(args);
            if (!string.IsNullOrEmpty(AfterPredicate)) elements.Add(AfterPredicate);

            var arguments = new HText(elements) { Separator = Separator };
            return new HText(name, arguments) { IsFunctional = true, Separator = " ", Indent = Indent};
        }
    }

    class SqlSyntaxLikeAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "LIKE"), args[1]);
        }
    }

    class SqlSyntaxBetweenAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Clause(LineSpace(args[0], "BETWEEN"), args[1], "AND", args[2]);
        }
    }

    class SqlSyntaxInAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Func(LineSpace(args[0], "IN"), args[1]);
        }
    }

    class SqlSyntaxAllAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return new DisableBracketsText(Func("ALL", args[0]));
        }

        internal class DisableBracketsText : ExpressionElement
        {
            ExpressionElement _core;

            internal DisableBracketsText(ExpressionElement core)
            {
                _core = core;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            {
                return _core.ToString(false, indent, context);
            }

            public override ExpressionElement ConcatAround(string front, string back) => this;

            public override ExpressionElement ConcatToFront(string front) => new DisableBracketsText(_core.ConcatToFront(front));

            public override ExpressionElement ConcatToBack(string back) => new DisableBracketsText(_core.ConcatToBack(back));

            public override ExpressionElement Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }






    class SqlSyntaxAssignAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            ExpressionElement arg1 = converter.Convert(exp.Arguments[0]).Customize(new CustomizeColumnOnly());
            return new HText(arg1, "=", converter.Convert(exp.Arguments[1])) { Separator = " " };
        }
    }

    class SqlSyntaxConditionAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            var obj = converter.ToObject(exp.Arguments[0]);
            return (bool)obj ? converter.Convert(exp.Arguments[1]) : (ExpressionElement)string.Empty;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxKeywordAttribute : SqlSyntaxConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => string.IsNullOrEmpty(Name) ? method.Method.Name.ToUpper() : Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(object obj) 
            => obj == null ? string.Empty : 
               string.IsNullOrEmpty(Name) ? obj.ToString().ToUpper() : Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MemberExpression member)
            => string.IsNullOrEmpty(Name) ? member.Member.Name.ToUpper() : Name;
    }

    class SqlSyntaxCurrentDateTimeAttribute : SqlSyntaxConverterAttribute
    {
        class CurrentDateTimeExpressionElement : ExpressionElement
        {
            string _front = string.Empty;
            string _back = string.Empty;
            string _core;

            internal CurrentDateTimeExpressionElement(string core)
            {
                _core = core;
            }

            CurrentDateTimeExpressionElement(string core, string front, string back)
            {
                _core = core;
                _front = front;
                _back = back;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context) => true;

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
                => string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + "CURRENT" + context.Option.CurrentDateTimeSeparator + _core + _back;

            public override ExpressionElement ConcatAround(string front, string back)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back + back);

            public override ExpressionElement ConcatToFront(string front)
                => new CurrentDateTimeExpressionElement(_core, front + _front, _back);

            public override ExpressionElement ConcatToBack(string back)
                => new CurrentDateTimeExpressionElement(_core, _front, _back + back);

            public override ExpressionElement Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }

        public string Name { get; set; }

        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method) 
            => new CurrentDateTimeExpressionElement(Name);
    }

    class SqlSyntaxConditionClauseAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var condition = converter.Convert(method.Arguments[method.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty;
            return Clause(method.Method.Name.ToUpper(), condition);
        }
    }


    class SqlSyntaxSelectAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            //ALL, DISTINCT
            var modify = new List<Expression>();
            for (int i = method.SkipMethodChain(0); i < method.Arguments.Count - 1; i++)
            {
                modify.Add(method.Arguments[i]);
            }

            var select = LineSpace(new ExpressionElement[] { "SELECT" }.Concat(modify.Select(e => converter.Convert(e))).ToArray());

            //select elemnts.
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            ExpressionElement selectTargetText = null;
            ObjectCreateInfo createInfo = null;

            //*
            if (typeof(IAsterisk).IsAssignableFrom(selectTargets.Type))
            {
                var asteriskType = selectTargets.Type.IsGenericType ? selectTargets.Type.GetGenericTypeDefinition() : null;
                if (asteriskType == typeof(IAsterisk<>)) createInfo = ObjectCreateAnalyzer.MakeSelectInfo(asteriskType);
                select.Add("*");
            }
            //new { item = db.tbl.column }
            else
            {
                createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
                selectTargetText =
                    new VText(createInfo.Members.Select(e => ToStringSelectedElement(converter, e)).ToArray()) { Indent = 1, Separator = "," };
            }

            return new SelectClauseText(createInfo, selectTargetText == null ? (ExpressionElement)select : new VText(select, selectTargetText));
        }

        static ExpressionElement ToStringSelectedElement(IExpressionConverter converter, ObjectCreateMemberInfo element)
        {
            //single select.
            //for example, COUNT(*).
            if (string.IsNullOrEmpty(element.Name)) return converter.Convert(element.Expression);

            //normal select.
            return LineSpace(converter.Convert(element.Expression), "AS", element.Name);
        }
        
    }
    class SqlSyntaxFromAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            return new HText(new ExpressionElement[] { "FROM", table }) { IsFunctional = true, Separator = " " };
        }

        internal static ExpressionElement ToTableName(IExpressionConverter decoder, Expression exp)
        {
            //where query, write tables side by side
            var arry = exp as NewArrayExpression;
            if (arry != null) return Arguments(arry.Expressions.Select(e => ToTableName(decoder, e)).ToArray());

            var table = decoder.Convert(exp);

            //TODO refactoring.
            var body = GetSqlExpressionBody(exp);
            if (body != null) return new SubQueryAndNameText(body, table);// new HText(table, body) { Separator = " ", EnableChangeLine = false };

            return table;
        }

        internal static string GetSqlExpressionBody(Expression exp)
        {
            var member = exp as MemberExpression;
            while (member != null)
            {
                if (typeof(ISqlExpression).IsAssignableFrom(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }

            var method = exp as MethodCallExpression;
            if (method != null && 0 < method.Arguments.Count)
            {
                member = method.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    if (typeof(ISqlExpression).IsAssignableFrom(member.Type)) return member.Member.Name;
                }
            }
            return null;
        }

        class SubQueryAndNameText : ExpressionElement
        {
            string _front = string.Empty;
            string _back = string.Empty;
            string _body;
            ExpressionElement _define;

            internal SubQueryAndNameText(string body, ExpressionElement table)
            {
                _body = body;
                _define = new HText(table, _body) { Separator = " ", EnableChangeLine = false };
            }

            SubQueryAndNameText(string body, ExpressionElement define, string front, string back)
            {
                _body = body;
                _define = define;
                _front = front;
                _back = back;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context) => context.WithEntied.ContainsKey(_body) ? true : _define.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            {
                if (context.WithEntied.ContainsKey(_body))
                {
                    return string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray()) + _front + _body + _back;
                }
                return _define.ToString(isTopLevel, indent, context);
            }

            public override ExpressionElement ConcatAround(string front, string back)
                => new SubQueryAndNameText(_body, _define.ConcatAround(front, back), front + _front, _back + back);

            public override ExpressionElement ConcatToFront(string front)
                => new SubQueryAndNameText(_body, _define.ConcatToFront(front), front + _front, _back);

            public override ExpressionElement ConcatToBack(string back)
                => new SubQueryAndNameText(_body, _define.ConcatToBack(back), _front, _back + back);

            public override ExpressionElement Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }
    }

    class SqlSyntaxJoinAttribute : SqlSyntaxConverterAttribute
    {
        public string Name { get; set; }
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var startIndex = method.SkipMethodChain(0);
            var table = SqlSyntaxFromAttribute.ToTableName(converter, method.Arguments[startIndex]);
            var condition = (startIndex + 1) < method.Arguments.Count ? converter.Convert(method.Arguments[startIndex + 1]) : null;

            var list = new List<ExpressionElement>();
            list.Add(Name);
            list.Add(table);
            if (condition != null)
            {
                list.Add("ON");
                list.Add(condition);
            }
            return new HText(list.ToArray()) { IsFunctional = true, Separator = " ", Indent = 1 };
        }
    }

    class SqlSyntaxSortedByAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => LineSpace(converter.Convert(method.Arguments[0]), method.Method.Name.ToUpper());
    }

    class SqlSyntaxSetAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var array = method.Arguments[1] as NewArrayExpression;
            var sets = new VText();
            sets.Add("SET");
            sets.Add(new VText(array.Expressions.Select(e => converter.Convert(e)).ToArray()) { Indent = 1, Separator = "," });
            return sets;
        }
    }

    class SqlSyntaxOrderByAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var arg = method.Arguments[method.SkipMethodChain(0)];
            var array = arg as NewArrayExpression;

            var orderBy = new VText();
            orderBy.Add("ORDER BY");
            var sort = new VText() { Separator = "," };
            sort.AddRange(1, array.Expressions.Select(e => converter.Convert(e)).ToList());
            orderBy.Add(sort);
            return orderBy;
        }
    }

    class SqlSyntaxWithAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var arry = method.Arguments[0] as NewArrayExpression;
            if (arry != null)
            {
                var v = new VText() { Indent = 1, Separator = "," };
                var names = new List<string>();
                foreach (var e in arry.Expressions)
                {
                    var table = converter.Convert(e);
                    var body = SqlSyntaxFromAttribute.GetSqlExpressionBody(e);
                    names.Add(body);
                    v.Add(Clause(LineSpace(body, "AS"), table));
                }
                return new WithEntriedText(new VText("WITH", v), names.ToArray());
            }

            //引数を二つにせなあかんのか？
            {
                var table = converter.Convert(method.Arguments[0]);
                var body = SqlSyntaxFromAttribute.GetSqlExpressionBody(method.Arguments[0]);
                var v = new VText() { Indent = 1 };
                v.Add(Clause(LineSpace(new RecursiveTargetText(Line(body, table)), "AS"), converter.Convert(method.Arguments[1])));
                return new WithEntriedText(new VText("WITH", v), new[] { body });
            }
        }
        class WithEntriedText : ExpressionElement
        {
            ExpressionElement _core;
            string[] _names;


            internal WithEntriedText(ExpressionElement core, string[] names)
            {
                _core = core;
                _names = names;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context)
                => _core.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            {
                foreach (var e in _names) context.WithEntied[e] = true;
                return _core.ToString(isTopLevel, indent, context);
            }

            public override ExpressionElement ConcatAround(string front, string back)
                => new WithEntriedText(_core.ConcatAround(front, back), _names);

            public override ExpressionElement ConcatToFront(string front)
                => new WithEntriedText(_core.ConcatToFront(front), _names);

            public override ExpressionElement ConcatToBack(string back)
                => new WithEntriedText(_core.ConcatToBack(back), _names);

            public override ExpressionElement Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }
        class RecursiveTargetText : ExpressionElement
        {
            ExpressionElement _core;

            internal RecursiveTargetText(ExpressionElement core)
            {
                _core = core;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context)
                => _core.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            {
                if (context.Option.ExistRecursive)
                {
                    return _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context);
                }
                return _core.ToString(isTopLevel, indent, context);
            }

            public override ExpressionElement ConcatAround(string front, string back)
                => new RecursiveTargetText(_core.ConcatAround(front, back));

            public override ExpressionElement ConcatToFront(string front)
                => new RecursiveTargetText(_core.ConcatToFront(front));

            public override ExpressionElement ConcatToBack(string back)
                => new RecursiveTargetText(_core.ConcatToBack(back));

            public override ExpressionElement Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }
    }
    class SqlSyntaxRecursiveAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var selectTargets = method.Arguments[method.Arguments.Count - 1];
            var createInfo = ObjectCreateAnalyzer.MakeSelectInfo(selectTargets);
            return new RecursiveClauseText(createInfo, Blanket(createInfo.Members.Select(e => (ExpressionElement)e.Name).ToArray()));
        }

        class RecursiveClauseText : ExpressionElement
        {
            ExpressionElement _core;
            ObjectCreateInfo _createInfo;

            internal RecursiveClauseText(ObjectCreateInfo createInfo, ExpressionElement core)
            {
                _core = core;
                _createInfo = createInfo;
            }

            public override bool IsSingleLine(ExpressionConvertingContext context) => _core.IsSingleLine(context);

            public override bool IsEmpty => _core.IsEmpty;

            public override string ToString(bool isTopLevel, int indent, ExpressionConvertingContext context)
            {
                context.ObjectCreateInfo = _createInfo;
                return _core.ToString(isTopLevel, indent, context);
            }

            public override ExpressionElement ConcatAround(string front, string back) => new SelectClauseText(_createInfo, _core.ConcatAround(front, back));

            public override ExpressionElement ConcatToFront(string front) => new SelectClauseText(_createInfo, _core.ConcatToFront(front));

            public override ExpressionElement ConcatToBack(string back) => new SelectClauseText(_createInfo, _core.ConcatToBack(back));

            public override ExpressionElement Customize(ISqlTextCustomizer customizer) => customizer.Custom(this);
        }
    }

    class SqlSyntaxRowsAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();

            //Sql server can't use parameter.
            if (method.Arguments.Count == 1)
            {
                return LineSpace("ROWS", args[0].Customize(new CustomizeParameterToObject()), "PRECEDING");
            }
            else
            {
                return LineSpace("ROWS BETWEEN", args[0].Customize(new CustomizeParameterToObject()),
                    "PRECEDING AND", args[1].Customize(new CustomizeParameterToObject()), "FOLLOWING");
            }
        }
    }

    class SqlSyntaxPartitionByAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var partitionBy = new VText();
            partitionBy.Add("PARTITION BY");

            var elements = new VText() { Indent = 1, Separator = "," };
            var array = method.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            partitionBy.Add(elements);

            return partitionBy;
        }
    }

    class SqlSyntaxOverAttribute : SqlSyntaxConverterAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var v = new VText();
            var overMethod = method;
            v.Add(overMethod.Method.Name.ToUpper() + "(");
            v.AddRange(1, overMethod.Arguments.Skip(1).
                Where(e => !(e is ConstantExpression)). //Skip null.
                Select(e => converter.Convert(e)).ToArray());
            return v.ConcatToBack(")");
        }
    }
}
