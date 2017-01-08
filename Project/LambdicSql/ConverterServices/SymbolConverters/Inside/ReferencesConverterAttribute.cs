﻿using LambdicSql.BuilderServices.Code;
using LambdicSql.BuilderServices.Code.Inside;
using LambdicSql.ConverterServices.Inside;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{
    class ReferencesConverter : SymbolConverterMethodAttribute
    {
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var create = LineSpace("REFERENCES", converter.Convert(expression.Arguments[startIndex]));
            var args = expression.Arguments.Skip(startIndex + 1).Select(e => converter.Convert(e).Customize(new CustomizeColumnOnly())).ToArray();
            return Func(create, args);
        }
    }
}