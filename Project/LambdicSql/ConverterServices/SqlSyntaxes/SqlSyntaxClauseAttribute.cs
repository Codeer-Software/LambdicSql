using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxClauseAttribute : SqlSyntaxConverterMethodAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Separator { get; set; } = " ";

        /// <summary>
        /// 
        /// </summary>
        public string AfterPredicate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;

            var elements = new List<BuildingParts>();
            elements.AddRange(args);
            if (!string.IsNullOrEmpty(AfterPredicate)) elements.Add(AfterPredicate);

            var arguments = new HParts(elements) { Separator = Separator };
            return new HParts(name, arguments) { IsFunctional = true, Separator = " ", Indent = Indent };
        }
    }
}
