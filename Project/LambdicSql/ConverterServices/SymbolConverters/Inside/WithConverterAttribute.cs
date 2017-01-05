using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Syntaxes;
using System.Collections.Generic;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class WithConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var arry = expression.Arguments[0] as NewArrayExpression;
            if (arry != null)
            {
                var v = new VSyntax() { Indent = 1, Separator = "," };
                var names = new List<string>();
                foreach (var e in arry.Expressions)
                {
                    var table = converter.Convert(e);
                    var body = FromConverterAttribute.GetSqlExpressionBody(e);
                    names.Add(body);
                    v.Add(Clause(LineSpace(body, "AS"), table));
                }
                return new WithEntriedText(new VSyntax("WITH", v), names.ToArray());
            }

            //引数を二つにせなあかんのか？
            {
                var table = converter.Convert(expression.Arguments[0]);
                var body = FromConverterAttribute.GetSqlExpressionBody(expression.Arguments[0]);
                var v = new VSyntax() { Indent = 1 };
                v.Add(Clause(LineSpace(new RecursiveTargetText(Line(body, table)), "AS"), converter.Convert(expression.Arguments[1])));
                return new WithEntriedText(new VSyntax("WITH", v), new[] { body });
            }
        }
        class WithEntriedText : Syntax
        {
            Syntax _core;
            string[] _names;


            internal WithEntriedText(Syntax core, string[] names)
            {
                _core = core;
                _names = names;
            }

            public override bool IsSingleLine(BuildingContext context)
                => _core.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            {
                foreach (var e in _names) context.WithEntied[e] = true;
                return _core.ToString(isTopLevel, indent, context);
            }

            public override Syntax ConcatAround(string front, string back)
                => new WithEntriedText(_core.ConcatAround(front, back), _names);

            public override Syntax ConcatToFront(string front)
                => new WithEntriedText(_core.ConcatToFront(front), _names);

            public override Syntax ConcatToBack(string back)
                => new WithEntriedText(_core.ConcatToBack(back), _names);

            public override Syntax Customize(ISyntaxCustomizer customizer)
                => customizer.Custom(this);
        }
        class RecursiveTargetText : Syntax
        {
            Syntax _core;

            internal RecursiveTargetText(Syntax core)
            {
                _core = core;
            }

            public override bool IsSingleLine(BuildingContext context)
                => _core.IsSingleLine(context);

            public override bool IsEmpty => false;

            public override string ToString(bool isTopLevel, int indent, BuildingContext context)
            {
                if (context.Option.ExistRecursiveClause)
                {
                    return _core.ConcatToFront("RECURSIVE ").ToString(isTopLevel, indent, context);
                }
                return _core.ToString(isTopLevel, indent, context);
            }

            public override Syntax ConcatAround(string front, string back)
                => new RecursiveTargetText(_core.ConcatAround(front, back));

            public override Syntax ConcatToFront(string front)
                => new RecursiveTargetText(_core.ConcatToFront(front));

            public override Syntax ConcatToBack(string back)
                => new RecursiveTargetText(_core.ConcatToBack(back));

            public override Syntax Customize(ISyntaxCustomizer customizer)
                => customizer.Custom(this);
        }
    }
}
