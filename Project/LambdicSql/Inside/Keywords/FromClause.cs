using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Inside.Keywords
{
    static class FromClause
    {
        internal static ExpressionElement ConvertFrom(IExpressionConverter converter, MethodCallExpression[] methods)
            => ConvertNonCodition(Clause, "FROM", converter, methods);

        internal static ExpressionElement ConvertCrossJoin(IExpressionConverter converter, MethodCallExpression[] methods)
            => ConvertNonCodition(SubClause, "CROSS JOIN", converter, methods);

        internal static ExpressionElement ConvertLeftJoin(IExpressionConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("LEFT JOIN", converter, methods);

        internal static ExpressionElement ConvertRightJoin(IExpressionConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("RIGHT JOIN", converter, methods);

        internal static ExpressionElement ConvertFullJoin(IExpressionConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("FULL JOIN", converter, methods);

        internal static ExpressionElement ConvertJoin(IExpressionConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("JOIN", converter, methods);

        internal static ExpressionElement ConvertWith(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var arry = method.Arguments[0] as NewArrayExpression;
            if (arry != null)
            {
                var v = new VText() { Indent = 1, Separator = "," };
                var names = new List<string>();
                foreach (var e in arry.Expressions)
                {
                    var table = converter.Convert(e);
                    var body = GetSqlExpressionBody(e);
                    names.Add(body);
                    v.Add(Clause(LineSpace(body, "AS"), table));
                }
                return new WithEntriedText(new VText("WITH", v), names.ToArray());
            }

            //引数を二つにせなあかんのか？
            {
                var table = converter.Convert(method.Arguments[0]);
                var body = GetSqlExpressionBody(method.Arguments[0]);
                var v = new VText() { Indent = 1 };
                v.Add(Clause(LineSpace(new RecursiveTargetText(Line(body, table)), "AS"), converter.Convert(method.Arguments[1])));
                return new WithEntriedText(new VText("WITH", v), new[] { body });
            }
        }

        static ExpressionElement ConvertNonCodition(Func<ExpressionElement, ExpressionElement[], HText> makeSqlText, string name, IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            return makeSqlText(name, new[] { table });
        }

        static ExpressionElement ConvertCondition(string name, IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            var condition = (startIndex + 1) < method.Arguments.Count ? converter.Convert(method.Arguments[startIndex + 1]) : null;
            return SubClause(name, table, "ON", condition);
        }

        static ExpressionElement ToTableName(IExpressionConverter decoder, Expression exp)
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

        static string GetSqlExpressionBody(Expression exp)
        {
            var member = exp as MemberExpression;
            while (member != null)
            {
                if (typeof(ISqlExpression).IsAssignableFrom(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }

            var method = exp as MethodCallExpression;
            if (method != null)
            {
                member = method.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    if (typeof(ISqlExpression).IsAssignableFrom(member.Type)) return member.Member.Name;
                }
            }
            return null;
        }
    }

    //TODO あれー、それでいくと、事前のCustomは破綻してんじゃない？


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
            /*
            var tab = string.Join(string.Empty, Enumerable.Range(0, indent).Select(e => "\t").ToArray());
            var core = new HText(_define, _body) { Separator = " ", EnableChangeLine = false }.ToString(isTopLevel, indent, context);
            return _front + core + _back;*/

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
            if ( context.Option.ExistRecursive)
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
