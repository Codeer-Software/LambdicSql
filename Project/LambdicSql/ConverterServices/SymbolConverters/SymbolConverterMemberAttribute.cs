﻿using LambdicSql.BuilderServices.Parts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class SymbolConverterMemberAttribute : Attribute
    {
        /// <summary>
        /// Convert expression to code parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public abstract CodeParts Convert(MemberExpression expression, ExpressionConverter converter);
    }
}
