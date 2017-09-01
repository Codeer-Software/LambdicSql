using LambdicSql.BuilderServices.CodeParts;
using System.Linq.Expressions;
using LambdicSql.ConverterServices.Inside;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class MethodFormatConverterAttribute : MethodConverterAttribute
    {
        FormatConverterCore _core = new FormatConverterCore();

        /// <summary>
        /// Direction to arrange.
        /// </summary>
        public FormatDirection FormatDirection { get { return _core.FormatDirection; } set { _core.FormatDirection = value; } }

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
        /// Indent.
        /// </summary>
        public int Indent { get { return _core.Indent; } set { _core.Indent = value; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MethodFormatConverterAttribute() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="format">format.</param>
        public MethodFormatConverterAttribute(string format) => Format = format;

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
            => _core.Convert(expression.Arguments, converter);
    }
}
