using LambdicSql.Inside;
using LambdicSql.SqlBase;

namespace LambdicSql
{
    /// <summary>
    /// Query.
    /// </summary>
    /// <typeparam name="TSelected">Type of selected at SELECT clause.</typeparam>
    public class SqlQuery<TSelected> : ISqlQuery<TSelected>
    {
        ISqlExpressionBase _core;

        /// <summary>
        /// Entity of selected at SELECT clause.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        public TSelected Body => InvalitContext.Throw<TSelected>(nameof(Body));

        /// <summary>
        /// DataBase Information.
        /// </summary>
        public DbInfo DbInfo => _core.DbInfo;

        /// <summary>
        /// Create SQL text.
        /// </summary>
        /// <param name="convertor">Convertor.</param>
        /// <returns>SQL text.</returns>
        public SqlText Convert(ISqlStringConverter convertor) => _core.Convert(convertor);

        internal SqlQuery(ISqlExpressionBase core) { _core = core; }
    }
}
