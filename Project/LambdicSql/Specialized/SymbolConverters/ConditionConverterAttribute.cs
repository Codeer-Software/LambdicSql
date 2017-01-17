using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.BuilderServices.Inside;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq.Expressions;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// 
    /// </summary>
    public class ConditionConverterAttribute : NewConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public override ICode Convert(NewExpression expression, ExpressionConverter converter)
        {
            var obj = converter.ConvertToObject(expression.Arguments[0]);
            return (bool)obj ? converter.ConvertToCode(expression.Arguments[1]) : string.Empty.ToCode();
        }
    }
}
