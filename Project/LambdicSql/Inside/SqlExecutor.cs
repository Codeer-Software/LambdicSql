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

        internal DbExecutor(IDbConnection connection, IQuery<TSelect> info)
        {
            _connection = connection;
            _info = info;
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
                    var parameters = new PrepareParameters();
                    var text = GetCommandText(parameters);
                    Sql.Log?.Invoke(text);
                    com.CommandText = text;
                    com.Connection = _connection;
                    foreach (var obj in parameters.GetParameters().Select(e => CreateParameter(com, e.Key, e.Value)))
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
                    var parameters = new PrepareParameters();
                    var text = GetCommandText(parameters);
                    Sql.Log?.Invoke(text);
                    com.CommandText = text;
                    com.Connection = _connection;
                    foreach (var obj in parameters.GetParameters().Select(e => CreateParameter(com, e.Key, e.Value)))
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
        {
            if (_connection.GetType().FullName == "Npgsql.NpgsqlConnection")
            {
                return new PostgresCustomizer();
            }
            return null;
        }
    }
}
