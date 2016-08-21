using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public interface ISqlStringConverter
    {
        DecodeContext Context { get; }
        string ToString(object obj);
        object ToObject(Expression exp);
    }
}
