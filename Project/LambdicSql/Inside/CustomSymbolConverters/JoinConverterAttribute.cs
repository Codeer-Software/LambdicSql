using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using System.Linq.Expressions;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    class JoinConverterAttribute : MethodConverterAttribute
    {
        public string Name { get; set; }

        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var table = FromConverterAttribute.ConvertTable(converter, expression.Arguments[startIndex]);
            var condition = ((startIndex + 1) < expression.Arguments.Count) ? converter.Convert(expression.Arguments[startIndex + 1]) : null;

            var join = new HCode() { IsFunctional = true, Separator = " ", Indent = 1 };
            join.Add(Name);
            join.Add(table);
            if (condition != null)
            {
                join.Add("ON");
                join.Add(condition);
            }
            return join;
        }
    }
}
