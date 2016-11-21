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


    public static class SetOperationExtensions
    {
        public class Dummy { }

        public static SqlExpression<TResult> Union<TResult>(this ISqlExpressionBase<TResult> lhs, ISqlExpressionBase rhs)
            => lhs.Concat(Sql<Dummy>.Create(db => Keywords.Union())).Concat(rhs);
        public static SqlExpression<TResult> Union<TResult>(this ISqlExpressionBase<TResult> lhs, bool isAll, ISqlExpressionBase rhs)
            => lhs.Concat(Sql<Dummy>.Create(db => Keywords.Union(isAll))).Concat(rhs);
        public static SqlExpression<TResult> Intersect<TResult>(this ISqlExpressionBase<TResult> lhs, ISqlExpressionBase rhs)
            => lhs.Concat(Sql<Dummy>.Create(db => Keywords.Intersect())).Concat(rhs);
        public static SqlExpression<TResult> Intersect<TResult>(this ISqlExpressionBase<TResult> lhs, bool isAll, ISqlExpressionBase rhs)
            => lhs.Concat(Sql<Dummy>.Create(db => Keywords.Intersect(isAll))).Concat(rhs);
        public static SqlExpression<TResult> Except<TResult>(this ISqlExpressionBase<TResult> lhs, ISqlExpressionBase rhs)
            => lhs.Concat(Sql<Dummy>.Create(db => Keywords.Except())).Concat(rhs);
        public static SqlExpression<TResult> Except<TResult>(this ISqlExpressionBase<TResult> lhs, bool isAll, ISqlExpressionBase rhs)
            => lhs.Concat(Sql<Dummy>.Create(db => Keywords.Except(isAll))).Concat(rhs);
        public static SqlExpression<TResult> Minus<TResult>(this ISqlExpressionBase<TResult> lhs, ISqlExpressionBase rhs)
            => lhs.Concat(Sql<Dummy>.Create(db => Keywords.Minus())).Concat(rhs);

        public static SqlQuery<TResult> Union<TResult>(this ISqlQuery<TResult> lhs, ISqlExpressionBase rhs)
            => new SqlQuery<TResult>(lhs.Concat(Sql<Dummy>.Create(db => Keywords.Union())).Concat(rhs));
        public static SqlQuery<TResult> Union<TResult>(this ISqlQuery<TResult> lhs, bool isAll, ISqlExpressionBase rhs)
            => new SqlQuery<TResult>(lhs.Concat(Sql<Dummy>.Create(db => Keywords.Union(isAll))).Concat(rhs));
        public static SqlQuery<TResult> Intersect<TResult>(this ISqlQuery<TResult> lhs, ISqlExpressionBase rhs)
            => new SqlQuery<TResult>(lhs.Concat(Sql<Dummy>.Create(db => Keywords.Intersect())).Concat(rhs));
        public static SqlQuery<TResult> Intersect<TResult>(this ISqlQuery<TResult> lhs, bool isAll, ISqlExpressionBase rhs)
            => new SqlQuery<TResult>(lhs.Concat(Sql<Dummy>.Create(db => Keywords.Intersect(isAll))).Concat(rhs));
        public static SqlQuery<TResult> Except<TResult>(this ISqlQuery<TResult> lhs, ISqlExpressionBase rhs)
            => new SqlQuery<TResult>(lhs.Concat(Sql<Dummy>.Create(db => Keywords.Except())).Concat(rhs));
        public static SqlQuery<TResult> Except<TResult>(this ISqlQuery<TResult> lhs, bool isAll, ISqlExpressionBase rhs)
            => new SqlQuery<TResult>(lhs.Concat(Sql<Dummy>.Create(db => Keywords.Except(isAll))).Concat(rhs));
        public static SqlQuery<TResult> Minus<TResult>(this ISqlQuery<TResult> lhs, ISqlExpressionBase rhs)
            => new SqlQuery<TResult>(lhs.Concat(Sql<Dummy>.Create(db => Keywords.Minus())).Concat(rhs));
    }
}
