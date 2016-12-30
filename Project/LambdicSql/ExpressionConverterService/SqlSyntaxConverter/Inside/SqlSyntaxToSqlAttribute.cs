﻿using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter.Inside
{
    class SqlSyntaxToSqlAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var text = (string)converter.ToObject(method.Arguments[0]);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }
}