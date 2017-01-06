using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Code;

namespace LambdicSql
{
    /// <summary>
    /// Expression.
    /// </summary>
    public class Sql
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public Parts Parts { get; }

        internal Sql(Parts parts)
        {
            Parts = parts;
        }
    }
    
    /// <summary>
    /// Expressions that represent part of the query.
    /// </summary>
    /// <typeparam name="T">The type represented by SqlExpression.</typeparam>
    public class Sql<T> : Sql
    {
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
        public static implicit operator T(Sql<T> src) => InvalitContext.Throw<T>("implicit operator");

        internal Sql(Parts parts) : base(parts) { }
    }

    /// <summary>
    /// Expressions that represent arguments of recursive SQL.
    /// </summary>
    /// <typeparam name="TSelected">The type represented by SqlExpression.</typeparam>
    public class SqlRecursiveArguments<TSelected> : Sql<TSelected>
    {
        internal SqlRecursiveArguments(Parts parts) : base(parts) { }
    }
}
