using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Parts.Inside.BuildingPartsFactoryUtils;

namespace LambdicSql.ConverterServices.SqlSyntaxes
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
        /// <param name="expression"></param>
        /// <returns></returns>
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;

            var hArgs = new HParts(args) { Separator = Separator }.ConcatToBack(")");
            return new HParts(Line(name, "("), hArgs) { IsFunctional = true };
        }
    }
}
