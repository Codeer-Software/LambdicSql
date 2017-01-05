using LambdicSql.BuilderServices.Syntaxes;
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
        /// Convert expression to syntax.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Syntax.</returns>
        public abstract Syntax Convert(NewExpression expression, ExpressionConverter converter);
    }
}
