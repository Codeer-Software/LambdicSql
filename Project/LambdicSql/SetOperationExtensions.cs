using LambdicSql.Inside;
using LambdicSql.SqlBase;

namespace LambdicSql
{
    /// <summary>
    /// Set operation exxtensions for ISqlExpressionBase and ISqlQuery.
    /// </summary>
    public static class SetOperationExtensions
    {
        /// <summary>
        /// Concatenate expression1 and expression2.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Concat<TResult>(this ISqlExpressionBase<TResult> expression1, ISqlExpressionBase expression2)
          => new SqlExpressionCoupled<TResult>(expression1, expression2);

        /// <summary>
        /// Concatenate query and expression.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Concat<TSelected>(this ISqlQuery<TSelected> query, ISqlExpressionBase expression)
          => new SqlQuery<TSelected>(new SqlExpressionCoupled<TSelected>(query, expression));

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Union<TResult>(this ISqlExpressionBase<TResult> expression1, ISqlExpressionBase expression2)
            => expression1.Concat(Sql<Non>.Create(db => Keywords.Union())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Union<TResult>(this ISqlExpressionBase<TResult> expression1, bool isAll, ISqlExpressionBase expression2)
            => expression1.Concat(Sql<Non>.Create(db => Keywords.Union(isAll))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Intersect<TResult>(this ISqlExpressionBase<TResult> expression1, ISqlExpressionBase expression2)
            => expression1.Concat(Sql<Non>.Create(db => Keywords.Intersect())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Intersect<TResult>(this ISqlExpressionBase<TResult> expression1, bool isAll, ISqlExpressionBase expression2)
            => expression1.Concat(Sql<Non>.Create(db => Keywords.Intersect(isAll))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Except<TResult>(this ISqlExpressionBase<TResult> expression1, ISqlExpressionBase expression2)
            => expression1.Concat(Sql<Non>.Create(db => Keywords.Except())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Except<TResult>(this ISqlExpressionBase<TResult> expression1, bool isAll, ISqlExpressionBase expression2)
            => expression1.Concat(Sql<Non>.Create(db => Keywords.Except(isAll))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using MINUS clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlExpression<TResult> Minus<TResult>(this ISqlExpressionBase<TResult> expression1, ISqlExpressionBase expression2)
            => expression1.Concat(Sql<Non>.Create(db => Keywords.Minus())).Concat(expression2);

        /// <summary>
        /// Concatenate query and expression using UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Union<TSelected>(this ISqlQuery<TSelected> query, ISqlExpressionBase expression)
            => new SqlQuery<TSelected>(query.Concat(Sql<Non>.Create(db => Keywords.Union())).Concat(expression));

        /// <summary>
        /// Concatenate query and expression using UNION clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Union<TSelected>(this ISqlQuery<TSelected> query, bool isAll, ISqlExpressionBase expression)
            => new SqlQuery<TSelected>(query.Concat(Sql<Non>.Create(db => Keywords.Union(isAll))).Concat(expression));

        /// <summary>
        /// Concatenate query and expression using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Intersect<TSelected>(this ISqlQuery<TSelected> query, ISqlExpressionBase expression)
            => new SqlQuery<TSelected>(query.Concat(Sql<Non>.Create(db => Keywords.Intersect())).Concat(expression));

        /// <summary>
        /// Concatenate query and expression using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Intersect<TSelected>(this ISqlQuery<TSelected> query, bool isAll, ISqlExpressionBase expression)
            => new SqlQuery<TSelected>(query.Concat(Sql<Non>.Create(db => Keywords.Intersect(isAll))).Concat(expression));

        /// <summary>
        /// Concatenate query and expression using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Except<TSelected>(this ISqlQuery<TSelected> query, ISqlExpressionBase expression)
            => new SqlQuery<TSelected>(query.Concat(Sql<Non>.Create(db => Keywords.Except())).Concat(expression));

        /// <summary>
        /// Concatenate query and expression using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="isAll">If isAll is true, add an ALL predicate.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Except<TSelected>(this ISqlQuery<TSelected> query, bool isAll, ISqlExpressionBase expression)
            => new SqlQuery<TSelected>(query.Concat(Sql<Non>.Create(db => Keywords.Except(isAll))).Concat(expression));

        /// <summary>
        /// Concatenate query and expression using MINUS clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="expression">Expression.</param>
        /// <returns>Concatenated result.</returns>
        public static SqlQuery<TSelected> Minus<TSelected>(this ISqlQuery<TSelected> query, ISqlExpressionBase expression)
            => new SqlQuery<TSelected>(query.Concat(Sql<Non>.Create(db => Keywords.Minus())).Concat(expression));
    }
}
