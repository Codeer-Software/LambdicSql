namespace LambdicSql.QueryBase
{
    public interface ISqlStringConverter
    {
        DbInfo DbInfo { get; }
        string ToString(object obj);
        string ResolvePrepare(string value);
    }
}
