using LambdicSql.BuilderServices.Parts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for constructor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public abstract class SymbolConverterNewAttribute : Attribute
    {
        /// <summary>
        /// Convert expression to code parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public abstract CodeParts Convert(NewExpression expression, ExpressionConverter converter);
    }
}
