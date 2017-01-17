using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// Holds the database type.
    /// </summary>
    /// <typeparam name="T">DB's type.</typeparam>
    public class Db<T> where T : class
    {
        /// <summary>
        /// Create sql.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql<TResult> Sql<TResult>(Expression<Func<T, TResult>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            return new Sql<TResult>(MakeSynatx(db, expression.Body));
        }

        /// <summary>
        /// Create a sql.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql<TSelected> Sql<TSelected>(Expression<Func<T, ClauseChain<TSelected>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            return new Sql<TSelected>(MakeSynatx(db, expression.Body));
        }

        /// <summary>
        /// Create a sql.
        /// </summary>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql Sql(Expression<Func<T, ClauseChain<Non>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            return new Sql(MakeSynatx(db, expression.Body));
        }

        /// <summary>
        /// Create a sql.
        /// Add name.
        /// </summary>
        /// <typeparam name="TSelected">It is the type selected in the SELECT clause.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static Sql<TSelected> Sql<TSelected>(Expression<Func<T, Sql<TSelected>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            var core = expression.Body as MemberExpression;
            return new Sql<TSelected>(core.Member.Name);
        }

        /// <summary>
        /// Create a sql.
        /// </summary>
        /// <typeparam name="TResult">The type represented by expression.</typeparam>
        /// <param name="expression">Expression expressing Sql by lambda.</param>
        /// <returns>Sql.</returns>
        public static SqlRecursiveArguments<TResult> Sql<TResult>(Expression<Func<T, Symbol.RecursiveArguments<TResult>>> expression)
        {
            var db = DBDefineAnalyzer.GetDbInfo<T>();
            return new SqlRecursiveArguments<TResult>(MakeSynatx(db, expression.Body));
        }

        static Code MakeSynatx(DbInfo dbInfo, Expression core)
        {
            var converter = new ExpressionConverter(dbInfo);
            return core == null ? string.Empty : converter.ConvertToCode(core);
        }
    }
}
