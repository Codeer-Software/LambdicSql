using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql.feat.EntityFramework
{
    public static class EFSql
    {
        public class SqlCore<TDB>
        {
            TDB _db;
            internal SqlCore(TDB db)
            {
                _db = db;
            }

            public SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, TResult>> exp)
            {
                var info = DBDefineAnalyzer.GetDbInfo(() => _db);
                return new SqlExpressionSingle<TResult>(info, exp.Body, _db);
            }
        }

        public static SqlCore<T> USing<T>(T dbContext) => new SqlCore<T>(dbContext);
    }
}
