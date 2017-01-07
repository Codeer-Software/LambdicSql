using LambdicSql.BuilderServices;
using System;
using LambdicSql.BuilderServices.Code;

namespace LambdicSql
{
    /// <summary>
    /// Enhancement of ISqlExpressionBase.
    /// </summary>
    public static class SqlExtensions
    {
        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql<TSelected> Build<TSelected>(this Sql<TSelected> expression, Type connectionType)
          => new BuildedSql<TSelected>(Build((Sql)expression, connectionType));

        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql<TSelected> Build<TSelected>(this Sql<TSelected> expression, DialectOption option)
          => new BuildedSql<TSelected>(Build((Sql)expression, option));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql Build(this Sql expression, Type connectionType)
            => Build(expression, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql Build(this Sql expression, DialectOption option)
        {
            var context = new BuildingContext(option);
            return new BuildedSql(expression.Parts.ToString(true, 0, context), context.ParameterInfo.GetDbParams());
        }

        class Dummy { }
        
        /// <summary>
        /// Concatenate expression1 and expression2.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Concat<TResult>(this Sql<TResult> expression1, Sql expression2)
          => new Sql<TResult>(new VParts(expression1.Parts, expression2.Parts));

        //TODO test.
        /// <summary>
        /// Concatenate expression1 and expression2.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Concat(this Sql expression1, Sql expression2)
          => new Sql(new VParts(expression1.Parts, expression2.Parts));

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Union())).Concat(expression2);

        //TODO test.
        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Union(this Sql expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Union())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Union(Symbols.All()))).Concat(expression2);

        //TODO test.
        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Union(this Sql expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Union(Symbols.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Intersect())).Concat(expression2);

        //TODO test.
        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Intersect(this Sql expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Intersect())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Intersect(Symbols.All()))).Concat(expression2);

        //TODO test.
        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Intersect(this Sql expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Intersect(Symbols.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Except())).Concat(expression2);

        //TODO test.
        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Except(this Sql expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Except())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Except(Symbols.All()))).Concat(expression2);

        //TODO test
        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Except(this Sql expression1, IAggregatePredicateAll all, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Except(Symbols.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using MINUS clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Minus<TResult>(this Sql<TResult> expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Minus())).Concat(expression2);

        //TODO test
        /// <summary>
        /// Concatenate expression1 and expression2 using MINUS clause.
        /// </summary>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql Minus(this Sql expression1, Sql expression2)
            => expression1.Concat(Db<Dummy>.Sql(db => Symbols.Minus())).Concat(expression2);
    }
}
