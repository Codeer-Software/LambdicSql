using LambdicSql.BuilderServices.CodeParts;
using System;
using System.Linq.Expressions;

//TODO こいつらの名前も変える必要あるよね
namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for constructor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public abstract class SymbolConverterNewAttribute : Attribute
    {
        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public abstract Code Convert(NewExpression expression, ExpressionConverter converter);
    }
}
