using LambdicSql.BuilderServices.Parts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes
{
    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class SqlSyntaxConverterMethodAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public abstract BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter);
    }
}
