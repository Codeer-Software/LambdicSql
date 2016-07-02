using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Free
{
    public class FreeClause : IClause
    {
        string _text;
        public FreeClause(string text) { _text = text; }
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder) => _text;
    }
}
