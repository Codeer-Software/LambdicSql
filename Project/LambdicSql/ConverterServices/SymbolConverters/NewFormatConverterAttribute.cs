using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices.Inside;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class NewFormatConverterAttribute : NewConverterAttribute
    {
        FormatConverterCore _core = new FormatConverterCore();

        /// <summary>
        /// 
        /// </summary>
        public FormatDirection FormatDirection { get { return _core.FormatDirection; } set { _core.FormatDirection = value; } }

        /// <summary>
        /// 
        /// </summary>
        public string Format { get { return _core.Format; } set { _core.Format = value; } }

        /// <summary>
        /// 
        /// </summary>
        public int Indent { get { return _core.Indent; } set { _core.Indent = value; } }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override Code Convert(NewExpression expression, ExpressionConverter converter)
            => _core.Convert(expression.Arguments, converter);
    }
}
