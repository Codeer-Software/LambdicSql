using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;

namespace LambdicSql
{
    public static class SqlExpressionExtensions
    {
        public static SqlExpression<TResult> Concat<TResult>(this ISqlExpressionBase<TResult> query, ISqlExpressionBase addExp)
          => new SqlExpressionCoupled<TResult>(query, addExp);

        public static SqlQuery<TSelected> Concat<TSelected>(this ISqlQuery<TSelected> query, ISqlExpressionBase addExp)
          => new SqlQuery<TSelected>(new SqlExpressionCoupled<TSelected>(query, addExp));

        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpressionBase<IQuery<TSelected>> exp, Type connectionType)
          => new SqlInfo<TSelected>(ToSqlInfo((ISqlExpressionBase)exp, connectionType));

        public static SqlInfo ToSqlInfo(this ISqlExpressionBase exp)
            => ToSqlInfo(exp, new SqlConvertOption(), null);

        public static SqlInfo ToSqlInfo(this ISqlExpressionBase exp, Type connectionType)
            => ToSqlInfo(exp, DialectResolver.CreateCustomizer(connectionType.FullName));

        public static SqlInfo ToSqlInfo(this ISqlExpressionBase exp, SqlConvertOption option)
            => ToSqlInfo(exp, option, null);

        public static SqlInfo ToSqlInfo(this ISqlExpressionBase exp, SqlConvertOption option, ISqlSyntaxCustomizer customizer)
        {
            var context = new DecodeContext(exp.DbInfo, option.ParameterPrefix);
            var converter = new SqlStringConverter(context, option, customizer);
            var text = exp.ToString(converter);

            //adjust. remove empty line.
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            text = string.Join(Environment.NewLine, lines.Where(e => !string.IsNullOrEmpty(e.Trim())).ToArray());

            return new SqlInfo(exp.DbInfo, text, context.SelectClauseInfo, context.Parameters);
        }
    }
}
