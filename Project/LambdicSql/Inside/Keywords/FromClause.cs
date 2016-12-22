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
        internal static SqlText ConvertFrom(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertNonCodition(Clause, "FROM", converter, methods);

        internal static SqlText ConvertCrossJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertNonCodition(SubClause, "CROSS JOIN", converter, methods);

        internal static SqlText ConvertLeftJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("LEFT JOIN", converter, methods);

        internal static SqlText ConvertRightJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("RIGHT JOIN", converter, methods);

        internal static SqlText ConvertJoin(ISqlStringConverter converter, MethodCallExpression[] methods)
            => ConvertCondition("JOIN", converter, methods);

        internal static SqlText ConvertWith(ISqlStringConverter converter, MethodCallExpression[] methods)
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

        static SqlText ConvertNonCodition(Func<SqlText, SqlText[], HText> makeSqlText, string name, ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            return makeSqlText(name, new[] { table });
        }

        static SqlText ConvertCondition(string name, ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            var startIndex = method.SkipMethodChain(0);
            var table = ToTableName(converter, method.Arguments[startIndex]);
            var condition = (startIndex + 1) < method.Arguments.Count ? converter.Convert(method.Arguments[startIndex + 1]) : null;
            return SubClause(name, table, "ON", condition);
        }

        static SqlText ToTableName(ISqlStringConverter decoder, Expression exp)
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
                if (typeof(ISqlExpressionBase).IsAssignableFrom(member.Type)) return member.Member.Name;
                member = member.Expression as MemberExpression;
            }

            var method = exp as MethodCallExpression;
            if (method != null)
            {
                member = method.Arguments[0] as MemberExpression;
                if (member != null)
                {
                    if (typeof(ISqlExpressionBase).IsAssignableFrom(member.Type)) return member.Member.Name;
                }
            }
            return null;
        }
    }

    //TODO あれー、それでいくと、事前のCustomは破綻してんじゃない？


    class SubQueryAndNameText : SqlText
    {
        string _front = string.Empty;
        string _back = string.Empty;
        string _body;
        SqlText _define;

        internal SubQueryAndNameText(string body, SqlText table)
        {
            _body = body;
            _define = new HText(table, _body) { Separator = " ", EnableChangeLine = false };
        }

        SubQueryAndNameText(string body, SqlText define, string front, string back)
        {
            _body = body;
            _define = define;
            _front = front;
            _back = back;
        }
        
        public override bool IsSingleLine(SqlConvertingContext context) => context.WithEntied.ContainsKey(_body) ? true : _define.IsSingleLine(context);

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
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

        public override SqlText ConcatAround(string front, string back)
            => new SubQueryAndNameText(_body, _define.ConcatAround(front, back), front + _front, _back + back);

        public override SqlText ConcatToFront(string front)
            => new SubQueryAndNameText(_body, _define.ConcatToFront(front), front + _front, _back);

        public override SqlText ConcatToBack(string back)
            => new SubQueryAndNameText(_body, _define.ConcatToBack(back), _front, _back + back);

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }

    class WithEntriedText : SqlText
    {
        SqlText _core;
        string[] _names;
       

        internal WithEntriedText(SqlText core, string[] names)
        {
            _core = core;
            _names = names;
        }

        public override bool IsSingleLine(SqlConvertingContext context)
            => _core.IsSingleLine(context);

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
        {
            foreach (var e in _names) context.WithEntied[e] = true;
            return _core.ToString(isTopLevel, indent, context);
        }

        public override SqlText ConcatAround(string front, string back)
            => new WithEntriedText(_core.ConcatAround(front, back), _names);

        public override SqlText ConcatToFront(string front)
            => new WithEntriedText(_core.ConcatToFront(front), _names);

        public override SqlText ConcatToBack(string back)
            => new WithEntriedText(_core.ConcatToBack(back), _names);

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
    class RecursiveTargetText : SqlText
    {
        SqlText _core;

        internal RecursiveTargetText(SqlText core)
        {
            _core = core;
        }

        public override bool IsSingleLine(SqlConvertingContext context)
            => _core.IsSingleLine(context);

        public override bool IsEmpty => false;

        public override string ToString(bool isTopLevel, int indent, SqlConvertingContext context)
        {
            if ( context.Option.ExistRecursive)
            {
                return _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context);
            }
            return _core.ToString(isTopLevel, indent, context);
        }

        public override SqlText ConcatAround(string front, string back)
            => new RecursiveTargetText(_core.ConcatAround(front, back));

        public override SqlText ConcatToFront(string front)
            => new RecursiveTargetText(_core.ConcatToFront(front));

        public override SqlText ConcatToBack(string back)
            => new RecursiveTargetText(_core.ConcatToBack(back));

        public override SqlText Customize(ISqlTextCustomizer customizer)
            => customizer.Custom(this);
    }
}
