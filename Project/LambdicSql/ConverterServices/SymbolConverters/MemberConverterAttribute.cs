using LambdicSql.BuilderServices.CodeParts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MemberConverterAttribute : Attribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the member.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public Code Convert(MemberExpression expression, ExpressionConverter converter)
        {
            if (string.IsNullOrEmpty(Name)) Name = expression.Member.Name.ToUpper();
            return Name;
        }
    }
}
