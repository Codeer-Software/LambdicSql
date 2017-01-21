namespace LambdicSql
{
    //TODO test 型のない方の結合

    /// <summary>
    /// Enhancement of ISqlExpressionBase.
    /// </summary>
    public static class SqlSetOperationsExtensions
    {
        class Dummy { }

        /// <summary>
        /// Concatenate sql1 and sql2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Union()) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using UNION clause.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Union(this Sql sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Union()) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="sql1">sql 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> sql1, IAggregatePredicateAll all, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Union(Symbol.All())) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using UNION clause.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Union(this Sql sql1, IAggregatePredicateAll all, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Union(Symbol.All())) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Intersect()) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using INTERSECT clause.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Intersect(this Sql sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Intersect()) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="sql1">sql 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> sql1, IAggregatePredicateAll all, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Intersect(Symbol.All())) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using INTERSECT clause.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Intersect(this Sql sql1, IAggregatePredicateAll all, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Intersect(Symbol.All())) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Except()) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using EXCEPT clause.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Except(this Sql sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Except()) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="sql1">sql 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> sql1, IAggregatePredicateAll all, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Except(Symbol.All())) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using EXCEPT clause.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Except(this Sql sql1, IAggregatePredicateAll all, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Except(Symbol.All())) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using MINUS clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Minus<TResult>(this Sql<TResult> sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Minus()) + sql2;

        /// <summary>
        /// Concatenate sql1 and sql2 using MINUS clause.
        /// </summary>
        /// <param name="sql1">sql 1.</param>
        /// <param name="sql2">sql 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Minus(this Sql sql1, Sql sql2)
            => sql1 + Db<Dummy>.Sql(db => Symbol.Minus()) + sql2;
    }
}
