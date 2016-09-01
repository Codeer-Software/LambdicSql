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

            //TODO params detail.
            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.Params.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
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

            //TODO
            Debug.Print(info.SqlText);

            //TODO params detail.
            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.Params.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
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
        static IDbDataParameter CreateParameter(IDbCommand com, string name, object obj)
        {
            var param = com.CreateParameter();
            param.ParameterName = name;
            param.Value = obj;
            return param;
        }
    }
}
