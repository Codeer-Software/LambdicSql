using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace LambdicSql
{
    [SqlSyntax]
    public static class Utils
    {
        public static T Cast<T>(this ISqlExpression query) => InvalitContext.Throw<T>(nameof(Cast));
        public static T Cast<T>(this ISqlExpression<T> query) => InvalitContext.Throw<T>(nameof(Cast));
        public static TTable Cast<TTable>(this ISqlExpression<IQuery<TTable>> query) => InvalitContext.Throw<TTable>(nameof(Cast));
        public static T Cast<T>(this IMethodChain words) => InvalitContext.Throw<T>(nameof(Cast));
        public static bool Condition(bool enable, bool condition) => InvalitContext.Throw<bool>(nameof(Condition));
        public static object Text(string text, params object[] args) => InvalitContext.Throw<object>(nameof(Text));
        public static T Text<T>(string text, params object[] args) => InvalitContext.Throw<T>(nameof(Text));
        public static IQuery<Non> TwoWaySql(string text, params object[] args) => InvalitContext.Throw<IQuery<Non>>(nameof(Text));
        public static TEntity T<TEntity>(this IQueryable<TEntity> queryable) => InvalitContext.Throw<TEntity>(nameof(T));

        static string MethodsToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Cast): return string.Empty;
                case nameof(T): return string.Empty;
                case nameof(Condition): return Condition(converter, method);
                case nameof(Text): return Text(converter, method);
                case nameof(TwoWaySql): return TwoWaySql(converter, method);
            }
            throw new NotSupportedException();
        }

        static string Condition(ISqlStringConverter converter, MethodCallExpression method)
        {
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[0], out obj);
            return (bool)obj ? converter.ToString(method.Arguments[1]) : string.Empty;
        }

        static string Text(ISqlStringConverter converter, MethodCallExpression method)
        {
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[0], out obj);
            var text = (string)obj;

            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string TwoWaySql(ISqlStringConverter converter, MethodCallExpression method)
        {
            object obj;
            ExpressionToObject.GetExpressionObject(method.Arguments[0], out obj);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);

            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
/*
 * 
    public class TwoWaySqlUtility
    {
        public static ISqlExpression Format(string sql, params ISqlExpression[] exps)
            => new SqlExpressionFormatText(TowWaySqlSpec.ToStringFormat(sql), exps);
    }
    class SqlExpressionFormatText : ISqlExpression
    {
        string _sql;
        ISqlExpression[] _exps;
        public DbInfo DbInfo { get; }

        public SqlExpressionFormatText(string sql, ISqlExpression[] exps)
        {
            _sql = sql;
            _exps = exps;
            var dbInfoOwner = _exps.Where(e => e.DbInfo != null).FirstOrDefault();
            if (dbInfoOwner != null)
            {
                DbInfo = dbInfoOwner.DbInfo;
            }
        }

        public string ToString(ISqlStringConverter converter)
            => string.Format(_sql, _exps.Select(e => e == null ? string.Empty : e.ToString(converter)).ToArray());
    }
*/