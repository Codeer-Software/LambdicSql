using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System.Linq;
using System.Linq.Expressions;
using LambdicSql.BuilderServices.Inside;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class ClauseStyleConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// Indent.
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            if (string.IsNullOrEmpty(Name)) Name = expression.Method.Name.ToUpper();

            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.ConvertToCode(e)).ToList();
            args.Insert(0, Name.ToCode());
            return new HCode(args) { AddIndentNewLine = true, Separator = " ", Indent = Indent };
        }
    }
}
