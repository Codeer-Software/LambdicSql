using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;

namespace LambdicSql
{
    /// <summary>
    /// Expression.
    /// </summary>
    public interface ISqlExpression
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        BuildingParts BuildingParts { get; }
    }
    
    /// <summary>
    /// Expressions that represent part of the query.
    /// </summary>
    /// <typeparam name="T">The type represented by SqlExpression.</typeparam>
    public class SqlExpression<T> : ISqlExpression
    {
        /// <summary>
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public BuildingParts BuildingParts { get; }

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

        internal SqlExpression(BuildingParts parts)
        {
            BuildingParts = parts;
        }
    }

    /// <summary>
    /// Expressions that represent arguments of recursive SQL.
    /// </summary>
    /// <typeparam name="TSelected">The type represented by SqlExpression.</typeparam>
    public class SqlRecursiveArgumentsExpression<TSelected> : SqlExpression<TSelected>
    {
        internal SqlRecursiveArgumentsExpression(BuildingParts parts) : base(parts) { }
    }
}
