namespace LambdicSql.QueryBase
{
    public interface IClause
    {
        IClause Clone();
        string ToString(ISqlStringConverter decoder);
    }
}
