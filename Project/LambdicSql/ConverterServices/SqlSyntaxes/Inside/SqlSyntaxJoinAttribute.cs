using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxJoinAttribute : SqlSyntaxConverterMethodAttribute
    {
        public string Name { get; set; }
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var startIndex = method.SkipMethodChain(0);
            var table = SqlSyntaxFromAttribute.ToTableName(converter, method.Arguments[startIndex]);
            var condition = (startIndex + 1) < method.Arguments.Count ? converter.Convert(method.Arguments[startIndex + 1]) : null;

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
