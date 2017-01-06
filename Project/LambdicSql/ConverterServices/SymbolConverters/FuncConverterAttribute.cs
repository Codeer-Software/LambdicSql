﻿using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Code;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Code.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for SQL Function.
    /// </summary>
    public class FuncConverterAttribute : SymbolConverterMethodAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Separator between arguments.By default it uses ", ".
        /// </summary>
        public string Separator { get; set; } = ", ";

        /// <summary>
        /// Convert expression to code parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override Parts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;

            var hArgs = new HParts(args) { Separator = Separator }.ConcatToBack(")");
            return new HParts(Line(name, "("), hArgs) { IsFunctional = true };
        }
    }
}