using LambdicSql.QueryBase;

namespace LambdicSql.ORM
{
    public interface IClause
    {
        IClause Clone();
        string ToString(ISqlStringConverter decoder);
    }
}
