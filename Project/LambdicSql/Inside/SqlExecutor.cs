using LambdicSql.QueryInfo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LambdicSql.Inside
{
    class SqlExecutor<TSelect> : ISqlExecutor<TSelect>
    {
        string _connectionString;
        string _sql;
        Func<IDbResult, TSelect> _create;

        internal SqlExecutor(string connectionString, string sql, Func<IDbResult, TSelect> create)
        {
            _connectionString = connectionString;
            _sql = sql;
            _create = create;
        }

        public IEnumerable<TSelect> Read()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var com = new SqlCommand(_sql, con))
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
}
