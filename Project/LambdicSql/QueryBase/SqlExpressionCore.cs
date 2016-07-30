using System.Linq.Expressions;

namespace LambdicSql.QueryBase
{
    class SqlExpressionCore<TDB> : ISqlExpression<TDB>
    {
        SqlExpressionCore<TDB> _before;
        Expression _core;
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
        public string ToString(ISqlStringConverter decoder)
        {
            if (_core == null)
            {
                return string.Empty;
            }
            var text = decoder.ToString(_core);
            if (_before != null)
            {
                text = text.Replace("{@BeforeExpression}", _before.ToString(decoder));
            }
            return text;
        }
    }
}
