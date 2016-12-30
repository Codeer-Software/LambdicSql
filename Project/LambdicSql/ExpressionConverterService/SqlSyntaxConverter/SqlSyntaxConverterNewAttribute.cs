using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter
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
