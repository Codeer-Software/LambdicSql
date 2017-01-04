using LambdicSql.BuilderServices.Parts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes
{
    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public abstract class SqlSyntaxConverterNewAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public abstract BuildingParts Convert(NewExpression expression, ExpressionConverter converter);
    }
}
