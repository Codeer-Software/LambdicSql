using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace LambdicSql.feat.EntityFramework
{
    public static class EFApaptExtensions
    {
        public static IEnumerable<T> SqlQuery<T>(this ISqlExpressionBase<IQuery<T>> exp, object dbContext)
            => SqlQuery<T>((ISqlExpressionBase)exp, dbContext);

        public static IEnumerable<T> SqlQuery<T>(this ISqlExpressionBase exp, object dbContext)
        {
            var cnn = EFWrapper.GetConnection(dbContext);
            var info = exp.ToSqlInfo(cnn.GetType());
            
            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
            }

            try
            {
                return EFWrapper<T>.SqlQuery(dbContext, info.SqlText, args);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        public static int ExecuteSqlCommand(this ISqlExpressionBase exp, object dbContext)
        {
            var cnn = EFWrapper.GetConnection(dbContext);
            var info = exp.ToSqlInfo(cnn.GetType());
            
            Debug.Print(info.SqlText);

            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
            }

            try
            {
                return EFWrapper.ExecuteSqlCommand(dbContext, info.SqlText, args);
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
    }
}
