namespace LambdicSql.QueryBase
{
    public interface ISqlStringConverter
    {
        DbInfo DbInfo { get; }
        string ToString(object obj);
        string ToString(object obj, object parameters);
    }
}
