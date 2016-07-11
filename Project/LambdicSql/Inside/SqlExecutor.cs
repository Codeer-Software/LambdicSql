using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace LambdicSql.Inside
{
    class DbExecutor<TSelect> : ISqlExecutor<TSelect>
        where TSelect : class
    {
        IDbAdapter _adaptor;
        IQuery<TSelect> _info;

        internal DbExecutor(IDbAdapter adaptor, IQuery<TSelect> info)
        {
            _adaptor = adaptor;
            _info = info;
        }

        public IEnumerable<TSelect> Read()
        {
            if (_info.Create == null)
            {
                throw new NotSupportedException("selected type is not able to be created.");
            }
            using (var con = _adaptor.CreateConnection())
            {
                con.Open();
                using (var com = _adaptor.CreateCommand())
                {
                    var parameters = new PrepareParameters();
                    var text = GetCommandText(parameters);
                    Sql.Log?.Invoke(text);
                    com.CommandText = text;
                    com.Connection = con;
                    com.Parameters.AddRange(parameters.GetParameters().Select(e => _adaptor.CreateParameter(e.Key, e.Value)).ToArray());
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
        }

        public int Write()
        {
            using (var con = _adaptor.CreateConnection())
            {
                con.Open();
                using (var com = _adaptor.CreateCommand())
                {
                    var parameters = new PrepareParameters();
                    var text = GetCommandText(parameters);
                    Sql.Log?.Invoke(text);
                    com.CommandText = text;
                    com.Connection = con;
                    com.Parameters.AddRange(parameters.GetParameters().Select(e => _adaptor.CreateParameter(e.Key, e.Value)).ToArray());
                    return com.ExecuteNonQuery();
                }
            }
        }

        string GetCommandText(PrepareParameters parameters) =>
            SqlStringConverter.ToString(_info, parameters, _adaptor.CreateQueryCustomizer());
    }

    //TODO
    class DbExecutor2<TSelect> : ISqlExecutor<TSelect>
        where TSelect : class
    {
        DbConnection _connection;
        IQuery<TSelect> _info;

        internal DbExecutor2(DbConnection connection, IQuery<TSelect> info)
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
                    com.Parameters.AddRange(parameters.GetParameters().Select(e => CreateParameter(com, e.Key, e.Value)).ToArray());
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
                    com.Parameters.AddRange(parameters.GetParameters().Select(e => CreateParameter(com, e.Key, e.Value)).ToArray());
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

        static DbParameter CreateParameter(DbCommand com, string name, object obj)
        {
            var param = com.CreateParameter();
            param.ParameterName = name;
            param.Value = obj;
            return param;
        }

        string GetCommandText(PrepareParameters parameters) =>
            SqlStringConverter.ToString(_info, parameters, null);//TODO
    }
}
