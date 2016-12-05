using LambdicSql.Inside;
using LambdicSql.SqlBase;

namespace LambdicSql
{
    public static class SetOperationExtensions
    {
        class Dummy { }

        public static SqlExpression<TResult> Concat<TResult>(this ISqlExpressionBase<TResult> expression1, ISqlExpressionBase expression2)
          => new SqlExpressionCoupled<TResult>(expression1, expression2);

        public static SqlQuery<TSelected> Concat<TSelected>(this ISqlQuery<TSelected> query, ISqlExpressionBase expression)
          => new SqlQuery<TSelected>(new SqlExpressionCoupled<TSelected>(query, expression));

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
