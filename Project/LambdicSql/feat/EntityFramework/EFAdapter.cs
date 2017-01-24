using LambdicSql.BuilderServices.CodeParts;
using LambdicSql.ConverterServices;
using LambdicSql.ConverterServices.SymbolConverters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.feat.EntityFramework
{
    /// <summary>
    /// Extensions for adjust Entity Framework.
    /// In order to use these, it is necessary that arbitrary  Entity Framework is installed. 
    /// LambdicSql has no dependency on  Entity Framework from the project compositionally with  Entity Framework. 
    /// That is to avoid inconsistencies between versions. Please install your favorite version of  Entity Framework by yourself.
    /// </summary>
    public static class EFAdapter
    {
        /// <summary>
        /// Debug Log.
        /// </summary>
        public static Action<string> Log { get; set; }

        /// <summary>
        /// Get entity.
        /// It can only be used within methods of the LambdicSql.Db class.
        /// </summary>
        /// <typeparam name="TEntity">Entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <returns>Entity.</returns>
        [TConverter]
        public static TEntity T<TEntity>(this IQueryable<TEntity> queryable) { throw new InvalitContextException(nameof(T)); }

        /// <summary>
        /// Execute query.
        /// </summary>
        /// <typeparam name="T">Type of result entity.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="dbContext">DbContext object.</param>
        /// <returns>Query result.</returns>
        public static IEnumerable<T> SqlQuery<T>(this Sql<T> query, object dbContext)
            => SqlQuery<T>((Sql)query, dbContext);

        /// <summary>
        /// Execute query.
        /// </summary>
        /// <typeparam name="T">Type of result entity.</typeparam>
        /// <param name="query">Query.</param>
        /// <param name="dbContext">DbContext object.</param>
        /// <returns>Query result.</returns>
        public static IEnumerable<T> SqlQuery<T>(this Sql query, object dbContext)
        {
            var cnn = EFWrapper.GetGetConnection(dbContext)(dbContext);
            var info = query.Build(cnn.GetType());

            //debug.
            Debug(info);

            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.GetParams().Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();

                try
                {
                    return EFWrapper<T>.GetSqlQuery(dbContext)(dbContext, info.Text, args);
                }
                catch (Exception e)
                {
                    throw GetCoreException(e);
                }
            }
        }

        /// <summary>
        /// Execute query.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <param name="dbContext">DbContext object.</param>
        /// <returns>Number of rows affected.</returns>
        public static int ExecuteSqlCommand(this Sql query, object dbContext)
        {
            var cnn = EFWrapper.GetGetConnection(dbContext)(dbContext);
            var info = query.Build(cnn.GetType());

            //debug.
            Debug(info);

            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.GetParams().Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
            }

            try
            {
                return EFWrapper.GetExecuteSqlCommand(dbContext)(dbContext, info.Text, args);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        static Exception GetCoreException(Exception e)
        {
            while (true)
            {
                if (e.InnerException == null) return e;
                e = e.InnerException;
            }
        }

        static IDbDataParameter CreateParameter(IDbCommand com, string name, DbParam src)
        {
            var dst = com.CreateParameter();
            dst.ParameterName = name;
            dst.Value = src.Value;

            if (src.DbType != null) dst.DbType = src.DbType.Value;
            if (src.Direction != null) dst.Direction = src.Direction.Value;
            if (src.SourceColumn != null) dst.SourceColumn = src.SourceColumn;
            if (src.SourceVersion != null) dst.SourceVersion = src.SourceVersion.Value;
            if (src.Precision != null) dst.Precision = src.Precision.Value;
            if (src.Scale != null) dst.Scale = src.Scale.Value;
            if (src.Size != null) dst.Size = src.Size.Value;

            return dst;
        }

        static void Debug(BuildedSql info)
        {
            if (Log == null) return;
            Log(info.Text);
            foreach (var e in info.GetParams())
            {
                Log(e.Key + " = " + (e.Value.Value == null ? string.Empty : e.Value.Value.ToString()));
            }
            Log(string.Empty);
        }
    }

    class TConverterAttribute : MethodConverterAttribute
    {
        public override ICode Convert(MethodCallExpression expression, ExpressionConverter converter)
            => converter.ConvertToCode(expression.Arguments[0]);
    }
}
