using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Update
{
    public class UpdateClause : IClause
    {
        public IClause Clone() => this;
        public string ToString(ISqlStringConverter decoder) => "DELETE";
    }
}
