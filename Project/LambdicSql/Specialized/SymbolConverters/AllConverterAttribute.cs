using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside.CodeParts;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CodeParts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// 
    /// </summary>
    public class AllConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = expression.Arguments.Select(e => converter.ConvertToCode(e)).ToArray();
            return new AllDisableBinaryExpressionBracketsCode(Func("ALL", args[0]));
        }
    }
}
