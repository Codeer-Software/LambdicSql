using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Delete
{
    public class DeleteClause : IClause
    {
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder) => "DELETE";
    }
}
