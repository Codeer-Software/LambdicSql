using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder.Parts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBuilder.Parts.Inside.SqlTextUtils;

namespace LambdicSql.ConverterService.SqlSyntaxes
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
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
        {
            var index = method.SkipMethodChain(0);
            var args = method.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? method.Method.Name.ToUpper() : Name;

            var hArgs = new HParts(args) { Separator = Separator }.ConcatToBack(")");
            return new HParts(Line(name, "("), hArgs) { IsFunctional = true };
        }
    }
}
