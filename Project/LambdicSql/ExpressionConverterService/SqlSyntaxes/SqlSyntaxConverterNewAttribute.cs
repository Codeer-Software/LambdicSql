using LambdicSql.SqlBuilder.ExpressionElements;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes
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
        /// <param name="exp"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(IExpressionConverter converter, NewExpression exp);
    }
}
