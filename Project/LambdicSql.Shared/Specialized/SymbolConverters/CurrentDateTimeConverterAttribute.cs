using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.Inside.CodeParts;
using System.Linq.Expressions;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// Converter for CURRENT_XXX conversion.There are spaces between these keywords depending on the DB.
    /// </summary>
    public class CurrentDateTimeConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// DATE or TIME or TIMESTAMP.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter) => new CurrentDateTimeCode(Name);
    }
}
