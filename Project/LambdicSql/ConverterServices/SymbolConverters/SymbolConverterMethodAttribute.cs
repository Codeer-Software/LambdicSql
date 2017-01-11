using LambdicSql.BuilderServices.Parts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class SymbolConverterMethodAttribute : Attribute
    {
        /// <summary>
        /// Convert expression to code parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public abstract CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter);
    }
}
