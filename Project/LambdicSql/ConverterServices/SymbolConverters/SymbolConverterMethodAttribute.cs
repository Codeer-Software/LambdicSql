using LambdicSql.BuilderServices.Syntaxes;
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
        /// Convert expression to syntax.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Syntax.</returns>
        public abstract Syntax Convert(MethodCallExpression expression, ExpressionConverter converter);
    }
}
