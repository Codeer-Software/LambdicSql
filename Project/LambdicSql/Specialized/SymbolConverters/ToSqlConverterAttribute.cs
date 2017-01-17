using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CodeParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// 
    /// </summary>
    public class ToSqlConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var text = (string)converter.ConvertToObject(expression.Arguments[0]);
            var array = expression.Arguments[1] as NewArrayExpression;
            return new StringFormatCode(text, array.Expressions.Select(e => converter.ConvertToCode(e)).ToArray());
        }
    }
}
