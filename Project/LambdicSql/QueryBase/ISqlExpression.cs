using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    public interface ISqlExpression
    {
        IQuery Query { get; }
        string ToString(ISqlStringConverter decoder);
        Expression Expression { get; }
    }

    public interface ISqlExpression<TDB> : ISqlExpression { }
    public interface ISqlExpression<TDB, TResult> : ISqlExpression { }
}
