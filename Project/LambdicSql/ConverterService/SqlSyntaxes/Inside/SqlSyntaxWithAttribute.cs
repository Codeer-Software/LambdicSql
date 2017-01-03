using LambdicSql.SqlBuilder;
using LambdicSql.SqlBuilder.Sentences;
using System.Collections.Generic;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Sentences.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxWithAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override Sentence Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var arry = method.Arguments[0] as NewArrayExpression;
            if (arry != null)
            {
                var v = new VSentence() { Indent = 1, Separator = "," };
                var names = new List<string>();
                foreach (var e in arry.Expressions)
                {
                    var table = converter.Convert(e);
                    var body = SqlSyntaxFromAttribute.GetSqlExpressionBody(e);
                    names.Add(body);
                    v.Add(Clause(LineSpace(body, "AS"), table));
                }
                return new WithEntriedText(new VSentence("WITH", v), names.ToArray());
            }

            //引数を二つにせなあかんのか？
            {
                var table = converter.Convert(method.Arguments[0]);
                var body = SqlSyntaxFromAttribute.GetSqlExpressionBody(method.Arguments[0]);
                var v = new VSentence() { Indent = 1 };
                v.Add(Clause(LineSpace(new RecursiveTargetText(Line(body, table)), "AS"), converter.Convert(method.Arguments[1])));
                return new WithEntriedText(new VSentence("WITH", v), new[] { body });
            }
        }
        class WithEntriedText : Sentence
        {
            Sentence _core;
            string[] _names;


            internal WithEntriedText(Sentence core, string[] names)
            {
                _core = core;
                _names = names;
            }

            public override bool IsSingleLine(SqlBuildingContext context)
                => _core.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            {
                foreach (var e in _names) context.WithEntied[e] = true;
                return _core.ToString(isTopLevel, indent, context);
            }

            public override Sentence ConcatAround(string front, string back)
                => new WithEntriedText(_core.ConcatAround(front, back), _names);

            public override Sentence ConcatToFront(string front)
                => new WithEntriedText(_core.ConcatToFront(front), _names);

            public override Sentence ConcatToBack(string back)
                => new WithEntriedText(_core.ConcatToBack(back), _names);

            public override Sentence Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }
        class RecursiveTargetText : Sentence
        {
            Sentence _core;

            internal RecursiveTargetText(Sentence core)
            {
                _core = core;
            }

            public override bool IsSingleLine(SqlBuildingContext context)
                => _core.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, SqlBuildingContext context)
            {
                if (context.Option.ExistRecursive)
                {
                    return _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context);
                }
                return _core.ToString(isTopLevel, indent, context);
            }

            public override Sentence ConcatAround(string front, string back)
                => new RecursiveTargetText(_core.ConcatAround(front, back));

            public override Sentence ConcatToFront(string front)
                => new RecursiveTargetText(_core.ConcatToFront(front));

            public override Sentence ConcatToBack(string back)
                => new RecursiveTargetText(_core.ConcatToBack(back));

            public override Sentence Customize(ISqlTextCustomizer customizer)
                => customizer.Custom(this);
        }
    }
}
