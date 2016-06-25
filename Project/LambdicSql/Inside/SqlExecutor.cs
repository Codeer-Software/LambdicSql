using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace LambdicSql.Inside
{
    class SqlExecutor<TSelect> : ISqlExecutor<TSelect>
    {
        DbConnection _connection;
        string _sql;
        Func<IDbResult, TSelect> _create;

        internal SqlExecutor(DbConnection connection, string sql, Func<IDbResult, TSelect> create)
        {
            _connection = connection;
            _sql = sql;
            _create = create;
        }

        public IEnumerable<TSelect> Read()
        {
            using (var com = new SqlCommand(_sql))
            using (var sdr = com.ExecuteReader())
            {
                var reader = new DbResult(sdr);
                var list = new List<TSelect>();
                while (sdr.Read())
                {
                    list.Add(_create(reader));
                }
                return list;
            }
        }
    }
}
