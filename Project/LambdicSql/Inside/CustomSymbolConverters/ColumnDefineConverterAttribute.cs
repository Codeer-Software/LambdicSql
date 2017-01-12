using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.Inside.CustomSymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for arguments.
    /// </summary>
    public class ColumnDefineConverterAttribute : SymbolConverterNewAttribute
    {
        /// <summary>
        /// Separator between arguments.By default it uses " ".
        /// </summary>
        public string Separator { get; set; } = " ";

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override Code Convert(NewExpression expression, ExpressionConverter converter)
        {
            var h = new HCode() { Separator = Separator };
            h.Add(converter.Convert(expression.Arguments[0]));
            h.Add(converter.Convert(expression.Arguments[1]));
            var array = expression.Arguments[2] as NewArrayExpression;
            if (array != null)
            {
                h.AddRange(array.Expressions.Select(e => converter.Convert(e)));
            }
            return h;
        }
    }
}
