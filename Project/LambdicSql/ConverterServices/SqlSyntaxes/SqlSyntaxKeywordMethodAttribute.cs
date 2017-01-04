﻿using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes
{
    /// <summary>
    /// SQL syntax attribute for keyword.
    /// </summary>
    public class SqlSyntaxKeywordMethodAttribute : SqlSyntaxConverterMethodAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to building parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>BuildingParts.</returns>
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;
    }
}
