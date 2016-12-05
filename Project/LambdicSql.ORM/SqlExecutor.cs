using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LambdicSql.ORM
{
    class SqlExecutor<TSelect> : ISqlExecutor<TSelect>
        where TSelect : class
    {
        IDbConnection _connection;
        SqlInfo<TSelect> _info;

        internal SqlExecutor(IDbConnection connection, SqlInfo<TSelect> info)
        {
            _connection = connection;
            _info = info;
            SqlOption.Log?.Invoke(_info.SqlText);
        }

        public IEnumerable<TSelect> Read()
        {
            bool openNow = false;
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
                openNow = true;
            }
            try
            {
                var indexInSelect = _info.SelectClauseInfo.Elements.Select(e => e.Name).ToList();
                var create = ExpressionToCreateFunc.ToCreateUseDbResult<TSelect>(indexInSelect, _info.SelectClauseInfo.Expression);

                using (var com = _connection.CreateCommand())
                {
                    com.CommandText = _info.SqlText;
                    com.Connection = _connection;
                    foreach (var obj in _info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value.Value)))
                    {
                        com.Parameters.Add(obj);
                    }
                    using (var sdr = com.ExecuteReader())
                    {
                        var reader = new SqlResult(sdr);
                        var list = new List<TSelect>();
                        while (sdr.Read())
                        {
                            list.Add(create(reader));
                        }
                        return list;
                    }
                }

            }
            finally
            {
                if (openNow)
                {
                    _connection.Close();
                }
            }
        }

        public int Write()
        {
            bool openNow = false;
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
                openNow = true;
            }
            try
            {
                using (var com = _connection.CreateCommand())
                {
                    com.CommandText = _info.SqlText;
                    com.Connection = _connection;
                    foreach (var obj in _info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value.Value)))
                    {
                        com.Parameters.Add(obj);
                    }
                    return com.ExecuteNonQuery();
                }
            }
            finally
            {
                if (openNow)
                {
                    _connection.Close();
                }
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
