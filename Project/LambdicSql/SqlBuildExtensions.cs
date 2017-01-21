using LambdicSql.BuilderServices;
using LambdicSql.BuilderServices.Inside;
using System;

namespace LambdicSql
{
    /// <summary>
    /// 
    /// </summary>
    public static class SqlBuildExtensions
    {
        /// <summary>
        /// Build.
        /// </summary>
        /// <param name="sql">Sql.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>SQL text and parameters.</returns>
        public static BuildedSql Build(this Sql sql, Type connectionType)
            => Build(sql, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Build.
        /// </summary>
        /// <param name="sql">Sql.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>SQL text and parameters.</returns>
        public static BuildedSql Build(this Sql sql, DialectOption option)
        {
            var context = new BuildingContext(option);
            var sqalText = sql.Code.ToString(context);
            return new BuildedSql(sqalText, context.ParameterInfo.GetDbParams());
        }

        /// <summary>
        /// Build.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <param name="sql">Sql.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>SQL text and parameters.</returns>
        public static BuildedSql<T> Build<T>(this Sql<T> sql, Type connectionType)
          => new BuildedSql<T>(Build((Sql)sql, connectionType));

        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <param name="sql">Sql.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql<T> Build<T>(this Sql<T> sql, DialectOption option)
          => new BuildedSql<T>(Build((Sql)sql, option));
    }
}
