using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
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
}
