using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.Expression.SqlSyntax.Inside
{
    class SqlSyntaxColumnOnlyAttribute : SqlSyntaxMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var col = converter.Convert(method.Arguments[0]) as DbColumnText;
            if (col == null) throw new NotSupportedException("invalid column.");
            return col.Customize(new CustomizeColumnOnly());
        }
    }
}
