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
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Union()) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Union(this Sql expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Union()) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Union(Symbol.All())) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Union(this Sql expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Union(Symbol.All())) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Intersect()) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Intersect(this Sql expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Intersect()) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Intersect(Symbol.All())) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Intersect(this Sql expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Intersect(Symbol.All())) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Except()) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Except(this Sql expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Except()) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Except(Symbol.All())) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Except(this Sql expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Except(Symbol.All())) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using MINUS clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Minus<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Minus()) + expression2;

        /// <summary>
        /// Concatenate expression1 and expression2 using MINUS clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Minus(this Sql expression1, Sql expression2)
            => expression1 + Db<Dummy>.Sql(db => Symbol.Minus()) + expression2;
    }
}
