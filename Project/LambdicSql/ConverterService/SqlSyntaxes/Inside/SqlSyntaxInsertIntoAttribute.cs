﻿using LambdicSql.SqlBuilder.Parts;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Parts.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes.Inside
{
    class SqlSyntaxInsertIntoAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var table = converter.Convert(method.Arguments[0]);

            //column should not have a table name.
            var arg = converter.Convert(method.Arguments[1]).Customize(new CustomizeColumnOnly());
            return Func(LineSpace("INSERT INTO", table), arg);
        }
    }
}