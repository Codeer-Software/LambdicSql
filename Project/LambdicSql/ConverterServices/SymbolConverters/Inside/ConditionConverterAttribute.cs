﻿using LambdicSql.BuilderServices.Syntaxes;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters.Inside
{

    class ConditionConverterAttribute : SymbolConverterNewAttribute
    {
        public override Syntax Convert(NewExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ToObject(expression.Arguments[0]);
            return (bool)obj ? converter.Convert(expression.Arguments[1]) : (Syntax)string.Empty;
        }
    }
}
