using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.ConverterServices.Inside;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.Specialized.SymbolConverters
{
    /// <summary>
    /// Converter for WHERE and HAVING clause conversion.
    /// </summary>
    public class ConditionClauseConverterAttribute : MethodConverterAttribute
    {
        ICode _nameCode;
        string _name;

        /// <summary>
        /// Name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _nameCode = value.ToCode();
            }
        }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var condition = converter.ConvertToCode(expression.Arguments[expression.SkipMethodChain(0)]);
            if (condition.IsEmpty) return string.Empty.ToCode();
            return Clause(_nameCode, condition);
        }
    }
}
