using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.SqlBase.TextParts.SqlTextUtils;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public class SqlSyntaxAttribute : Attribute { }

    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public abstract class SqlSyntaxMethodAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method);
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxFuncAttribute : SqlSyntaxMethodAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var args = method.Arguments.Select(e => converter.Convert(e)).ToArray();
            return Func(method.Method.Name.ToUpper(), args);
        }
    }
}
