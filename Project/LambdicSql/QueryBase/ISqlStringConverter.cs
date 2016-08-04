namespace LambdicSql.QueryBase
{
    public interface ISqlStringConverter
    {
        DecodeContext Context { get; }
        string ToString(object obj);
    }
}
