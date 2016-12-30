using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.Expression.SqlSyntax.Inside
{

    class SqlSyntaxInsertIntoAttribute : SqlSyntaxMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var table = converter.Convert(method.Arguments[0]);

            //column should not have a table name.
            var arg = converter.Convert(method.Arguments[1]).Customize(new CustomizeColumnOnly());
            return Func(LineSpace("INSERT INTO", table), arg);
        }
    }
}
