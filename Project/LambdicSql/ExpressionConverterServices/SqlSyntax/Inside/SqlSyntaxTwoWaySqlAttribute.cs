﻿using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterServices.SqlSyntax.Inside
{
    class SqlSyntaxTwoWaySqlAttribute : SqlSyntaxMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}
