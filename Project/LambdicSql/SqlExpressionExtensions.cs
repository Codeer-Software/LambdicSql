using LambdicSql.Inside;
using LambdicSql.SqlBuilder;
using System;

namespace LambdicSql
{
    //TODO 拡張にするべきなのか？
    //役割分担を考えればそんなにおかしくないけれども

    /// <summary>
    /// Enhancement of ISqlExpressionBase.
    /// </summary>
    public static class SqlExpressionExtensions
    {
        //--------------------ここは取り込んだ方がええやろ---------------------------------
        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo<TSelected> Build<TSelected>(this SqlExpression<TSelected> expression, Type connectionType)
          => new SqlInfo<TSelected>(Build((ISqlExpression)expression, connectionType));

        //この↓はいりません。
        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo Build(this ISqlExpression expression, Type connectionType)
            => Build(expression, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo Build(this ISqlExpression expression, DialectOption option)
        {
            var context = new SqlBuildingContext(option);
            return new SqlInfo(expression.DbInfo, expression.ExpressionElement.ToString(true, 0, context), context.ObjectCreateInfo, context.ParameterInfo.GetDbParams());
        }
        //-----------------------------------------------------------------------------------

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
        /// <param name="all">ALL predicate.</param>
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
        /// <param name="all">ALL predicate.</param>
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
        /// <param name="all">ALL predicate.</param>
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
