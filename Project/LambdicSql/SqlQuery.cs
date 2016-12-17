using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;

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
        /// Data converted from Expression to a form close to a string representation.
        /// </summary>
        /// <returns>text.</returns>
        public SqlText SqlText => _core.SqlText;
        
        internal SqlQuery(ISqlExpressionBase core) { _core = core; }
    }
}
