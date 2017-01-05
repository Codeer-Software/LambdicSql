using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class PartitionByConverterAttribute : SymbolConverterMethodAttribute
    {
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var partitionBy = new VSyntax();
            partitionBy.Add("PARTITION BY");

            var elements = new VSyntax() { Indent = 1, Separator = "," };
            var array = expression.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            partitionBy.Add(elements);

            return partitionBy;
        }
    }
}
