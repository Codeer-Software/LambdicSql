using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxConverter
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
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression exp);
    }
}
