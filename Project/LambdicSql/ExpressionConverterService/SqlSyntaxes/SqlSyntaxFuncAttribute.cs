using LambdicSql.ExpressionConverterService.Inside;
using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.ExpressionElements.Inside.SqlTextUtils;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxFuncAttribute : SqlSyntaxConverterMethodAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Separator { get; set; } = ", ";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var index = method.SkipMethodChain(0);
            var args = method.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? method.Method.Name.ToUpper() : Name;

            var hArgs = new HText(args) { Separator = Separator }.ConcatToBack(")");
            return new HText(Line(name, "("), hArgs) { IsFunctional = true };
        }
    }
}
