using LambdicSql.ConverterService;
using LambdicSql.Inside;
using LambdicSql.SqlBuilder.Sentences;

namespace LambdicSql
{
    //TODO このインターフェイスの存在価値は
    //いくつかの引数に取りやすいことと、
    //★メインは、変換の一部の実装がやりやすいだけ。

    /// <summary>
    /// Expression.
    /// </summary>
    public interface ISqlExpression
    {
        //TODO メンバーは非公開でいいでしょう。

        /// <summary>
        /// Data Base info.
        /// </summary>
        DbInfo DbInfo { get; }

        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        Sentence ExpressionElement { get; }
    }
    
    //拡張なのは不自然よね・・・。まあinternalならいいか。いや、やめよう！

    /// <summary>
    /// Expressions that represent part of the query.
    /// </summary>
    /// <typeparam name="T">The type represented by SqlExpression.</typeparam>
    public abstract class SqlExpression<T> : ISqlExpression
    {
        /// <summary>
        /// DB information.
        /// </summary>
        public abstract DbInfo DbInfo { get; protected set; }

        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public abstract Sentence ExpressionElement { get; }

        /// <summary>
        /// Entity represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        public T Body => InvalitContext.Throw<T>(nameof(Body));

        /// <summary>
        /// Implicitly convert to the type represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator T(SqlExpression<T> src) => InvalitContext.Throw<T>("implicit operator");
    }
}
