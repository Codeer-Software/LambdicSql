using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class JoinConverterAttribute : SymbolConverterMethodAttribute
    {
        public string Name { get; set; }
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var table = FromConverterAttribute.ToTableName(converter, expression.Arguments[startIndex]);
            var condition = (startIndex + 1) < expression.Arguments.Count ? converter.Convert(expression.Arguments[startIndex + 1]) : null;

            var list = new List<BuildingParts>();
            list.Add(Name);
            list.Add(table);
            if (condition != null)
            {
                list.Add("ON");
                list.Add(condition);
            }
            return new HParts(list.ToArray()) { IsFunctional = true, Separator = " ", Indent = 1 };
        }
    }
}
