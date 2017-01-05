using LambdicSql.BuilderServices.Syntaxes;
using LambdicSql.BuilderServices.Syntaxes.Inside;
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
            return arry == null ? ConvertRecurciveWith(expression, converter) : ConvertNormalWith(converter, arry);
        }

        static Syntax ConvertNormalWith(ExpressionConverter converter, NewArrayExpression arry)
        {
            var with = new VSyntax() { Indent = 1, Separator = "," };
            var names = new List<string>();
            foreach (var e in arry.Expressions)
            {
                var table = converter.Convert(e);
                var body = FromConverterAttribute.GetSubQuery(e);
                names.Add(body);
                with.Add(Clause(LineSpace(body, "AS"), table));
            }
            return new WithEntriedSyntax(new VSyntax("WITH", with), names.ToArray());
        }

        static Syntax ConvertRecurciveWith(MethodCallExpression expression, ExpressionConverter converter)
        {
            var table = converter.Convert(expression.Arguments[0]);
            var sub = FromConverterAttribute.GetSubQuery(expression.Arguments[0]);
            var with = new VSyntax() { Indent = 1 };
            with.Add(Clause(LineSpace(new RecursiveTargetSyntax(Line(sub, table)), "AS"), converter.Convert(expression.Arguments[1])));
            return new WithEntriedSyntax(new VSyntax("WITH", with), new[] { sub });
        }
    }
}
