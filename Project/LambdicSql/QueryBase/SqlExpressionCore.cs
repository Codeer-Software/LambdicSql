using LambdicSql.Inside;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    class SqlExpressionCore<TDB> : ISqlExpression<TDB>
    {
        SqlExpressionCore<TDB> _before;
        Expression _core;
        public Expression Expression => _core;
        public DbInfo DbInfo { get; private set; }
        public SqlExpressionCore(DbInfo dbInfo)
        {
            DbInfo = dbInfo;
        }

        public SqlExpressionCore(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            _core = core;
        }

        public SqlExpressionCore(SqlExpressionCore<TDB> before, Expression core)
        {
            DbInfo = before.DbInfo;
            _before = before;
            _core = core;
        }

        public string ToString(ISqlStringConverter src)
        {
            if (_core == null)
            {
                return string.Empty;
            }
            var decoder = src as SqlStringConverter;
            var text = decoder.ToString(_core);
            if (_before != null)
            {
                text = _before.ToString(decoder) + text;
            }
            /*
            var decoder = src as SqlStringConverter;
            var onditionExpressionText = (_before == null || _before._core == null) ? string.Empty : "{@BeforeExpression}";
            decoder.ContinueConditionExpressionText = onditionExpressionText;
            var old = decoder.ContinueConditionExpressionText;
            var text = decoder.ToString(_core);
            decoder.ContinueConditionExpressionText = old;
            if (_before != null)
            {
                text = text.Replace("{@BeforeExpression}", _before.ToString(decoder));
            }*/
            return text;
        }
    }

    class SqlExpressionCore<TDB, TResult> : SqlExpressionCore<TDB>, ISqlExpression<TDB, TResult>
    {
        public SqlExpressionCore(DbInfo dbInfo) : base(dbInfo) { }
        public SqlExpressionCore(DbInfo dbInfo, Expression core) : base(dbInfo, core) { }
        public SqlExpressionCore(SqlExpressionCore<TDB> before, Expression core) : base(before, core) { }
    }
}
