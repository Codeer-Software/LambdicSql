using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LambdicSql.Inside
{
    class DbExecutor<TSelect> : ISqlExecutor<TSelect>
        where TSelect : class
    {
        IDbConnection _connection;
        IQuery<TSelect> _info;
        string _sql;
        PrepareParameters _parameters = new PrepareParameters();

        internal DbExecutor(IDbConnection connection, IQuery<TSelect> info)
        {
            _connection = connection;
            _info = info;
            _sql = GetCommandText(_parameters);
            Sql.Log?.Invoke(_sql);
        }

        public IEnumerable<TSelect> Read()
        {
            if (_info.Create == null)
            {
                throw new NotSupportedException("selected type is not able to be created.");
            }
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
                    com.CommandText = _sql;
                    com.Connection = _connection;
                    foreach (var obj in _parameters.GetParameters().Select(e => CreateParameter(com, e.Key, e.Value)))
                    {
                        com.Parameters.Add(obj);
                    }
                    using (var sdr = com.ExecuteReader())
                    {
                        var reader = new SqlResult(sdr);
                        var list = new List<TSelect>();
                        while (sdr.Read())
                        {
                            list.Add(_info.Create(reader));
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
                    com.CommandText = _sql;
                    com.Connection = _connection;
                    foreach (var obj in _parameters.GetParameters().Select(e => CreateParameter(com, e.Key, e.Value)))
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

        string GetCommandText(PrepareParameters parameters) =>
            SqlStringConverter.ToString(_info, parameters, CreateCustomizer());

        IQueryCustomizer CreateCustomizer()
            => QueryCustomizeResolver.CreateCustomizer(_connection.GetType().FullName);
    }

    public static class QueryCustomizeResolver
    {
        public static IQueryCustomizer CreateCustomizer(string connectionTypeFullName)
        {
            if (connectionTypeFullName == "Npgsql.NpgsqlConnection")
            {
                return new PostgresCustomizer();
            }
            if (connectionTypeFullName == "System.Data.SQLite.SQLiteConnection")
            {
                return new SQLiteCustomizer();
            }
            return null;
        }
    }
}
