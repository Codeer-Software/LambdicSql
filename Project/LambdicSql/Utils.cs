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
        public static T Cast<T>(this ISqlExpressionBase query) => InvalitContext.Throw<T>(nameof(Cast));
        public static T Cast<T>(this IMethodChain words) => InvalitContext.Throw<T>(nameof(Cast));
        public static bool Condition(bool enable, bool condition) => InvalitContext.Throw<bool>(nameof(Condition));
        public static object Text(string text, params object[] args) => InvalitContext.Throw<object>(nameof(Text));
        public static T Text<T>(string text, params object[] args) => InvalitContext.Throw<T>(nameof(Text));
        public static IQuery<Non> TwoWaySql(string text, params object[] args) => InvalitContext.Throw<IQuery<Non>>(nameof(Text));
        public static T ColumnOnly<T>(T target) => InvalitContext.Throw<T>(nameof(ColumnOnly));

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Cast): return string.Empty;
                case nameof(Condition): return Condition(converter, method);
                case nameof(Text): return Text(converter, method);
                case nameof(TwoWaySql): return TwoWaySql(converter, method);
                case nameof(ColumnOnly): return ColumnOnly(converter, method);
            }
            throw new NotSupportedException();
        }

        static string Condition(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            return (bool)obj ? converter.ToString(method.Arguments[1]) : string.Empty;
        }

        static string Text(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = (string)obj;

            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string TwoWaySql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);

            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string ColumnOnly(ISqlStringConverter converter, MethodCallExpression method)
        {
            var dic = converter.Context.DbInfo.GetLambdaNameAndColumn().ToDictionary(e => e.Value.SqlFullName, e => e.Value.SqlColumnName);
            string col;
            if (dic.TryGetValue(converter.ToString(method.Arguments[0]), out col)) return col;
            throw new NotSupportedException("invalid column.");
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

