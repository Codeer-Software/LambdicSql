﻿using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes.Inside
{
    class SqlSyntaxOrderByAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var arg = method.Arguments[method.SkipMethodChain(0)];
            var array = arg as NewArrayExpression;

            var orderBy = new VParts();
            orderBy.Add("ORDER BY");
            var sort = new VParts() { Separator = "," };
            sort.AddRange(1, array.Expressions.Select(e => converter.Convert(e)).ToList());
            orderBy.Add(sort);
            return orderBy;
        }
    }

}