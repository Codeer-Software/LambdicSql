using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Case
{
    public class CaseClause : IClause
    {
        //TODO　Case
        string _text;
        public CaseClause(string text) { _text = text; }
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder) => _text;
    }
}
