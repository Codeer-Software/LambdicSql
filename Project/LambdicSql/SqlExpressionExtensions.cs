using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;

namespace LambdicSql
{
    public static class SqlExpressionExtensions
    {
        public static DB Cast<DB>(this ISqlExpression query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static TResult Cast<TResult>(this ISqlExpression<ISqlWords<TResult>> query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static ISqlExpression<TResult> Concat<TResult>(this ISqlExpression<TResult> query, ISqlExpression addExp)
          => new SqlExpression<TResult>((SqlExpression<TResult>)query, addExp.Expression);

        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpression<ISqlWords<TSelected>> exp, Type connectionType)
             where TSelected : class
        {
            var parameters = new PrepareParameters();
            var converter = new SqlStringConverter(exp.DbInfo, parameters, QueryCustomizeResolver.CreateCustomizer(connectionType.FullName), 0);
            return new SqlInfo<TSelected>(exp.DbInfo, exp.ToString(converter), parameters.GetParameters());
        }
    }
}
