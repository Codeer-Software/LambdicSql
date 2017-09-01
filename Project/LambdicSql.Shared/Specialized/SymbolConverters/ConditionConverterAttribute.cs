using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq.Expressions;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// Attributes for converting the Condition utility.
    /// </summary>
    public class ConditionConverterAttribute : NewConverterAttribute
    {
        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(NewExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ConvertToObject(expression.Arguments[0]);
            return (bool)obj ? (ICode)new AroundCode(converter.ConvertToCode(expression.Arguments[1]), "(", ")") : string.Empty.ToCode();
        }
    }
}
