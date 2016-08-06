using LambdicSql.QueryBase;
using System.Linq;

namespace LambdicSql.Inside
{
    class SqlExpressionFormatText : ISqlExpression
    {
        string _sql;
        ISqlExpression[] _exps;
        public DbInfo DbInfo { get; }

        public SqlExpressionFormatText(string sql, ISqlExpression[] exps)
        {
            _sql = sql;
            _exps = exps;
            var dbInfoOwner = _exps.Where(e => e.DbInfo != null).FirstOrDefault();
            if (dbInfoOwner != null)
            {
                DbInfo = dbInfoOwner.DbInfo;
            }
        }

        public string ToString(ISqlStringConverter converter)
            => string.Format(_sql, _exps.Select(e => e == null ? string.Empty : e.ToString(converter)).ToArray());
    }
}
