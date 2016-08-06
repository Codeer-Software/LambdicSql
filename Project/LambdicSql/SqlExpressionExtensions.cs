using LambdicSql.Dialect;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq;

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
            //TODO error to all.
            throw new NotSupportedException("do not call cast except in expression.");
        }
        
        public static ISqlExpression<TResult> Concat<TResult>(this ISqlExpression<TResult> query, ISqlExpression addExp)
          => new SqlExpressionMulti<TResult>(query, addExp);

        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpression<ISqlKeyWord<TSelected>> exp, Type connectionType)
             where TSelected : class
          => new SqlInfo<TSelected>(ToSqlInfo((ISqlExpression)exp, connectionType));

        public static SqlInfo ToSqlInfo(this ISqlExpression exp, Type connectionType)
        {
            var context = new DecodeContext(exp.DbInfo);
            var converter = new SqlStringConverter(context, DialectResolver.CreateCustomizer(connectionType.FullName));
            var text = exp.ToString(converter);

            //adjust. remove empty line.
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            text = string.Join(Environment.NewLine, lines.Where(e => !string.IsNullOrEmpty(e.Trim())).ToArray());

            return new SqlInfo(exp.DbInfo, text, context.SelectClauseInfo, context.Parameters.GetParameters());
        }
    }
}
