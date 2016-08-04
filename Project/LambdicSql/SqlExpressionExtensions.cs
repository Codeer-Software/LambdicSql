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

        public static TResult Cast<TResult>(this ISqlExpression<ISqlKeyWord<TResult>> query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static ISqlExpression<TResult> Concat<TResult>(this ISqlExpression<TResult> query, ISqlExpression addExp)
          => new SqlExpression<TResult>((SqlExpression<TResult>)query, addExp.Expression);

        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpression<ISqlKeyWord<TSelected>> exp, Type connectionType)
             where TSelected : class
        {
            var context = new DecodeContext(exp.DbInfo);
            var converter = new SqlStringConverter(context, QueryCustomizeResolver.CreateCustomizer(connectionType.FullName));
            return new SqlInfo<TSelected>(exp.DbInfo, exp.ToString(converter), context.SelectClauseInfo, context.Parameters.GetParameters());
        }
    }
}
