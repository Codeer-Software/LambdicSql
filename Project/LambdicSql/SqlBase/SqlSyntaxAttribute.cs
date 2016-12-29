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
        /// <param name="exp"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression exp);
    }

    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public abstract class SqlSyntaxNewAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(IExpressionConverter converter, NewExpression exp);
    }

    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public abstract class SqlSyntaxMemberAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(IExpressionConverter converter, MemberExpression exp);
    }

    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public abstract class SqlSyntaxObjectAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract ExpressionElement Convert(object obj);
    }

    /// <summary>
    /// SQL syntax attribute.
    /// </summary>
    public class SqlSyntaxToStringAttribute : SqlSyntaxObjectAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(object obj) => obj == null ? string.Empty : obj.ToString().ToUpper();
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
