using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql
{
    /// <summary>
    /// Expressions that represent part of the query.
    /// </summary>
    /// <typeparam name="T">The type represented by SqlExpression.</typeparam>
    public abstract class SqlExpression<T> : ISqlExpression<T>
    {
        /// <summary>
        /// DB information.
        /// </summary>
        public abstract DbInfo DbInfo { get; protected set; }

        /// <summary>
        /// Entity represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        public T Body => InvalitContext.Throw<T>(nameof(Body));

        /// <summary>
        /// Stringify.
        /// </summary>
        /// <param name="convertor">Convertor.</param>
        /// <returns>Text.</returns>
        public abstract SqlText Convert(ISqlStringConverter convertor);

        /// <summary>
        /// Implicitly convert to the type represented by SqlExpression.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <param name="src"></param>
        public static implicit operator T(SqlExpression<T> src) => InvalitContext.Throw<T>("implicit operator");
    }
}
