using LambdicSql.BuilderServices.Syntaxes;
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
        /// Convert expression to syntax.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Syntax.</returns>
        public abstract Syntax Convert(MemberExpression expression, ExpressionConverter converter);
    }
}
