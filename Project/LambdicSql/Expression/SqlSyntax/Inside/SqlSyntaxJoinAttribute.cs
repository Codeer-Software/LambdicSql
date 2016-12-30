using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Expression.SqlSyntax.Inside
{
    class SqlSyntaxJoinAttribute : SqlSyntaxMethodAttribute
    {
        public string Name { get; set; }
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var startIndex = method.SkipMethodChain(0);
            var table = SqlSyntaxFromAttribute.ToTableName(converter, method.Arguments[startIndex]);
            var condition = (startIndex + 1) < method.Arguments.Count ? converter.Convert(method.Arguments[startIndex + 1]) : null;

            var list = new List<ExpressionElement>();
            list.Add(Name);
            list.Add(table);
            if (condition != null)
            {
                list.Add("ON");
                list.Add(condition);
            }
            return new HText(list.ToArray()) { IsFunctional = true, Separator = " ", Indent = 1 };
        }
    }
}
