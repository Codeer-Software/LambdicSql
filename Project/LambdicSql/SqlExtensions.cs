using LambdicSql.BuilderServices;
using System;
using LambdicSql.BuilderServices.Syntaxes;

namespace LambdicSql
{
    //TODO 拡張にするべきなのか？
    //役割分担を考えればそんなにおかしくないけれども

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
        public static Command<TSelected> Build<TSelected>(this Sql<TSelected> expression, Type connectionType)
          => new Command<TSelected>(Build((ISql)expression, connectionType));

        //この↓はいりません。
        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static Command Build(this ISql expression, Type connectionType)
            => Build(expression, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static Command Build(this ISql expression, DialectOption option)
        {
            var context = new BuildingContext(option);
            return new Command(expression.Syntax.ToString(true, 0, context), context.ParameterInfo.GetDbParams());
        }

        class Non { }

        //TODO ラムダの中で使われたら？
        //もう、面倒だからラムダの中では使えない属性をつけるか。

        /// <summary>
        /// Concatenate expression1 and expression2.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Concat<TResult>(this Sql<TResult> expression1, ISql expression2)
          => new Sql<TResult>(new VSyntax(expression1.Syntax, expression2.Syntax));

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> expression1, ISql expression2)
            => expression1.Concat(Db<Non>.Sql(db => Keywords.Union())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using UNION clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Union<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, ISql expression2)
            => expression1.Concat(Db<Non>.Sql(db => Keywords.Union(Keywords.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> expression1, ISql expression2)
            => expression1.Concat(Db<Non>.Sql(db => Keywords.Intersect())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using INTERSECT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Intersect<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, ISql expression2)
            => expression1.Concat(Db<Non>.Sql(db => Keywords.Intersect(Keywords.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> expression1, ISql expression2)
            => expression1.Concat(Db<Non>.Sql(db => Keywords.Except())).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using EXCEPT clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="all">ALL predicate.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Except<TResult>(this Sql<TResult> expression1, IAggregatePredicateAll all, ISql expression2)
            => expression1.Concat(Db<Non>.Sql(db => Keywords.Except(Keywords.All()))).Concat(expression2);

        /// <summary>
        /// Concatenate expression1 and expression2 using MINUS clause.
        /// </summary>
        /// <typeparam name="TResult">The type represented by SqlExpression.</typeparam>
        /// <param name="expression1">Expression 1.</param>
        /// <param name="expression2">Exppresion 2.</param>
        /// <returns>Concatenated result.</returns>
        public static Sql<TResult> Minus<TResult>(this Sql<TResult> expression1, ISql expression2)
            => expression1.Concat(Db<Non>.Sql(db => Keywords.Minus())).Concat(expression2);
    }
}
