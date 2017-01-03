using LambdicSql.BuilderServices.Parts;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Expressions that represent arguments of recursive SQL.
    /// </summary>
    /// <typeparam name="TSelected">The type represented by SqlExpression.</typeparam>
    public class SqlRecursiveArgumentsExpression<TSelected> : SqlExpression<TSelected>
    {
        internal SqlRecursiveArgumentsExpression(BuildingParts parts) : base(parts) { }
    }
}
