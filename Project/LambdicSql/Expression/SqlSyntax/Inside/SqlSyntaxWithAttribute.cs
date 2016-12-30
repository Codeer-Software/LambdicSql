using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Collections.Generic;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Expression.SqlSyntax.Inside
{
    class SqlSyntaxWithAttribute : SqlSyntaxMethodAttribute
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
}
