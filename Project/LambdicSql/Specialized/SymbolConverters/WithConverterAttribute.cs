﻿using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CodeParts;
using System.Collections.Generic;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// 
    /// </summary>
    public class WithConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var arry = expression.Arguments[0] as NewArrayExpression;
            return arry == null ? ConvertRecurciveWith(expression, converter) : ConvertNormalWith(converter, arry);
        }

        static Code ConvertNormalWith(ExpressionConverter converter, NewArrayExpression arry)
        {
            var with = new VCode() { Indent = 1, Separator = "," };
            var names = new List<string>();
            foreach (var e in arry.Expressions)
            {
                var table = converter.Convert(e);
                var body = FromConverterAttribute.GetSubQuery(e);
                names.Add(body);
                with.Add(Clause(LineSpace(body, "AS"), table));
            }
            return new WithEntriedCode(new VCode("WITH", with), names.ToArray());
        }

        static Code ConvertRecurciveWith(MethodCallExpression expression, ExpressionConverter converter)
        {
            var table = converter.Convert(expression.Arguments[0]);
            var sub = FromConverterAttribute.GetSubQuery(expression.Arguments[0]);
            var with = new VCode() { Indent = 1 };
            with.Add(Clause(LineSpace(new RecursiveTargetCode(Line(sub, table)), "AS"), converter.Convert(expression.Arguments[1])));
            return new WithEntriedCode(new VCode("WITH", with), new[] { sub });
        }
    }
}