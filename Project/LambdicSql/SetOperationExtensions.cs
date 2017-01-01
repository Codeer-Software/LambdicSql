using LambdicSql.Inside;

namespace LambdicSql
{
    /// <summary>
    /// Set operation exxtensions for ISqlExpressionBase.
    /// </summary>
    public static class SetOperationExtensions
    {
        class Non { }

        /// <summary>
        /// Concatenate expression1 and expression2.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Concat<TResult>(this SqlExpression<TResult> expression1, ISqlExpression expression2)
          => new SqlExpressionCoupled<TResult>(expression1, expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Union<TResult>(this SqlExpression<TResult> expression1, ISqlExpression expression2)
            => expression1.Concat(Sql<Non>.Of(db => Keywords.Union())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Union<TResult>(this SqlExpression<TResult> expression1, IAggregatePredicateAll all, ISqlExpression expression2)
            => expression1.Concat(Sql<Non>.Of(db => Keywords.Union(Keywords.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Intersect<TResult>(this SqlExpression<TResult> expression1, ISqlExpression expression2)
            => expression1.Concat(Sql<Non>.Of(db => Keywords.Intersect())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Intersect<TResult>(this SqlExpression<TResult> expression1, IAggregatePredicateAll all, ISqlExpression expression2)
            => expression1.Concat(Sql<Non>.Of(db => Keywords.Intersect(Keywords.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Except<TResult>(this SqlExpression<TResult> expression1, ISqlExpression expression2)
            => expression1.Concat(Sql<Non>.Of(db => Keywords.Except())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Except<TResult>(this SqlExpression<TResult> expression1, IAggregatePredicateAll all, ISqlExpression expression2)
            => expression1.Concat(Sql<Non>.Of(db => Keywords.Except(Keywords.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using MINUS clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Minus<TResult>(this SqlExpression<TResult> expression1, ISqlExpression expression2)
            => expression1.Concat(Sql<Non>.Of(db => Keywords.Minus())).Concat(expression2);
    }
}
