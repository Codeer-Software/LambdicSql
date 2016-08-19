using LambdicSql.Dialect;
using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;

namespace LambdicSql
{
    public static class SqlExpressionExtensions
    {
        public static SqlExpression<TResult> Concat<TResult>(this ISqlExpression<TResult> query, ISqlExpression addExp)
          => new SqlExpressionCoupled<TResult>(query, addExp);

        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpression<IQuery<TSelected>> exp, Type connectionType)
          => new SqlInfo<TSelected>(ToSqlInfo((ISqlExpression)exp, connectionType));

        public static SqlInfo ToSqlInfo(this ISqlExpression exp, Type connectionType)
        {
            //TODO refactoring.
            string prefix = connectionType.FullName == "Oracle.ManagedDataAccess.Client.OracleConnection" ?
                            ":" : "@";

            var context = new DecodeContext(exp.DbInfo, prefix);
            var converter = new SqlStringConverter(context, DialectResolver.CreateCustomizer(connectionType.FullName));
            var text = exp.ToString(converter);

            //adjust. remove empty line.
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            text = string.Join(Environment.NewLine, lines.Where(e => !string.IsNullOrEmpty(e.Trim())).ToArray());

            return new SqlInfo(exp.DbInfo, text, context.SelectClauseInfo, context.Parameters);
        }
    }
}
