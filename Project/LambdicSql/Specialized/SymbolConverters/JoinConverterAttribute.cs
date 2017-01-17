using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using System.Linq.Expressions;
using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// 
    /// </summary>
    public class JoinConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var startIndex = expression.SkipMethodChain(0);
            var table = FromConverterAttribute.ConvertTable(converter, expression.Arguments[startIndex]);
            var condition = ((startIndex + 1) < expression.Arguments.Count) ? converter.ConvertToCode(expression.Arguments[startIndex + 1]) : null;

            var join = new HCode() { IsFunctional = true, Separator = " ", Indent = 1 };
            join.Add(Name.ToCode());
            join.Add(table);
            if (condition != null)
            {
                join.Add("ON".ToCode());
                join.Add(condition);
            }
            return join;
        }
    }
}
