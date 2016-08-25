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
        public static IEnumerable<T> SqlQuery<T>(this ISqlExpression<IQuery<T>> exp)
        {
            var cnn = EFWrapper.GetConnection(exp.DbContext);
            var info = exp.ToSqlInfo(cnn.GetType());

            //TODO params detail.
            object[] args;
            using (var com = cnn.CreateCommand())
            {
                args = info.Params.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray();
            }

            try
            {
                return EFWrapper<T>.SqlQuery(exp.DbContext, info.SqlText, args);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        public static int ExecuteSqlCommand(this ISqlExpression exp)
        {
            var cnn = EFWrapper.GetConnection(exp.DbContext);
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
                return EFWrapper.ExecuteSqlCommand(exp.DbContext, info.SqlText, args);
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
