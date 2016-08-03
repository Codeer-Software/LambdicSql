using LambdicSql.Inside;
using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    class SqlExpressionCore<TDB> : ISqlExpression<TDB>
    {
        SqlExpressionCore<TDB> _before;
        Expression _core;
        public Expression Expression => _core;
        public IQuery Query { get; private set; }
        public SqlExpressionCore(IQuery query)
        {
            Query = query;
        }

        public SqlExpressionCore(IQuery query, Expression core)
        {
            Query = query;
            _core = core;
        }

        public SqlExpressionCore(SqlExpressionCore<TDB> before, Expression core)
        {
            Query = before.Query;
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
        public SqlExpressionCore(IQuery query) : base(query) { }
        public SqlExpressionCore(IQuery query, Expression core) : base(query, core) { }
        public SqlExpressionCore(SqlExpressionCore<TDB> before, Expression core) : base(before, core) { }
    }
}
