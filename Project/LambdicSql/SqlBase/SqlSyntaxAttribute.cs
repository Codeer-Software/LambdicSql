using LambdicSql.Inside;
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
    public abstract class SqlSyntaxAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(IExpressionConverter converter, NewExpression exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(IExpressionConverter converter, MemberExpression exp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual ExpressionElement Convert(object obj)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxFuncAttribute : SqlSyntaxAttribute
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

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxTopAttribute : SqlSyntaxAttribute
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
            return LineSpace(method.Method.Name.ToUpper(), args[0].Customize(new CustomizeParameterToObject()));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxPredicateAttribute : SqlSyntaxAttribute
    {
        //TODO リンク遅い
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => LineSpace(new ExpressionElement[] { method.Method.Name.ToUpper() }.Concat(method.Arguments.Select(e => converter.Convert(e))).ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxClauseAttribute : SqlSyntaxAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public int Indent { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var index = method.SkipMethodChain(0);
            var args = method.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            return new HText(new ExpressionElement[] { method.Method.Name.ToUpper() }.Concat(args)) { IsFunctional = true, Separator = " ", Indent = Indent};
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxNameAttribute : SqlSyntaxAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => method.Method.Name.ToUpper();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(object obj) => obj == null ? string.Empty : obj.ToString().ToUpper();
    }
}
