using LambdicSql.SqlBase;

namespace LambdicSql
{
    public interface ISqlQuery : ISqlExpressionBase { }

    public interface ISqlQuery<out TSelected> : ISqlExpressionBase<IQuery<TSelected>>, ISqlQuery
    {
        TSelected Body { get; }
    }
}

