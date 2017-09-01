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
        /// <para>Format.</para>
        /// <para>for exsample...</para>
        /// <para>[MethodFormatConverter(Format = "GROUP BY |[&lt; ,&gt;0] WITH ROLLUP")]</para>
        /// <para>| -> | Letters that are more distant are not broken. The character on the right side will be broken as appropriate.</para>
        /// <para>[i] -> It is converted to the argument of the specified index.</para>
        /// <para>[&lt;&gt;i] -> Indicates that arguments of the specified index will be expanded. It must be an array type. Specify a separator in &lt;&gt;.</para>
        /// <para>[$i] -> It means converting an argument to a direct value.</para>
        /// <para>[#i] -> It specifies that the argument contains a column and that it is converted to only column names without a table name.</para>
        /// <para>[!i] -> Specifies that the argument is a special character and is to be converted with intact characters without ''.</para>
        /// <para>[*i] -> It use the variable name.</para>
        /// <para>&amp;left; -> ]</para>
        /// <para>&amp;right; -> ]</para>
        /// </summary>
        public string Format { get { return _core.Format; } set { _core.Format = value; } }

        /// <summary>
        /// Vanish, if empty params.
        /// </summary>
        public bool VanishIfEmptyParams { get { return _core.VanishIfEmptyParams; } set { _core.VanishIfEmptyParams = value; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public NewFormatConverterAttribute() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="format">format.</param>
        public NewFormatConverterAttribute(string format) => Format = format;

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(NewExpression expression, ExpressionConverter converter)
            => _core.Convert(expression.Arguments, converter);
    }
}
