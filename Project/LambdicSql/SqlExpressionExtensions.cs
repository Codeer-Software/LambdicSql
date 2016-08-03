using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Data;

namespace LambdicSql
{
    public static class SqlExpressionExtensions
    {
        public static DB Cast<DB>(this ISqlExpression query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static TResult Cast<DB, TResult>(this ISqlExpression<DB, ISqlWords<TResult>> query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }
        
        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpression<ISqlWords<TSelected>> exp, Type connectionType)
             where TSelected : class
        {
            var parameters = new PrepareParameters();
            var converter = new SqlStringConverter(exp.DbInfo, parameters, QueryCustomizeResolver.CreateCustomizer(connectionType.FullName), 0);
            return new SqlInfo<TSelected>(exp.DbInfo, exp.ToString(converter), parameters.GetParameters());
        }

        public static ISqlExpression<TDB, TResult> Concat<TDB, TResult>(this ISqlExpression<TDB, TResult> query, ISqlExpression addExp)
            where TDB : class
          => new SqlExpressionCore<TDB, TResult>((SqlExpressionCore<TDB, TResult>)query, addExp.Expression);
    }

    public interface ISqlHelper : ISqlWords { }
    public interface IQueryDesigner<T> : ISqlWords<T>, ISqlFuncs, IWindowWords, ISqlHelper { }

    public class NoSelected { }
}
