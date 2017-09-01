using LambdicSql.BuilderServices.CodeParts;
using System.Linq.Expressions;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class ClauseStyleConverterAttribute : MethodConverterAttribute
    {
        GeneralStyleConverterCore _core = new GeneralStyleConverterCore();

        /// <summary>
        /// Indent.
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get { return _core.Name; } set { _core.Name = value; } }

        /// <summary>
        /// Vanish, if empty params.
        /// </summary>
        public bool VanishIfEmptyParams { get { return _core.VanishIfEmptyParams; } set { _core.VanishIfEmptyParams = value; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClauseStyleConverterAttribute() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">name.</param>
        public ClauseStyleConverterAttribute(string name) => Name = name;

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var args = _core.InitAndConvertArguments(expression, converter);
            if (args == null) return string.Empty.ToCode();
            var hArgs = new HCode(args) { Separator = ", " };
            return new HCode(_core.NameCode, hArgs) { AddIndentNewLine = true, Separator = " ", Indent = Indent };
        }
    }
}
