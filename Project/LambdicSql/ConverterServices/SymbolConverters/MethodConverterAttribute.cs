using LambdicSql.BuilderServices.CodeParts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class MethodConverterAttribute : Attribute
    {
        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public abstract Code Convert(MethodCallExpression expression, ExpressionConverter converter);
    }
}
