using LambdicSql.QueryBase;
using System.Collections.Generic;

namespace LambdicSql.Inside
{
    class SqlExecutor<TSelect> : IDBExecutor<TSelect>
        where TSelect : class
    {
        IDbAdapter _adaptor;
        IQuery<TSelect> _info;

        public string CommandText => _adaptor.CreateParser().ToString(_info);

        internal SqlExecutor(IDbAdapter adaptor, IQuery<TSelect> info)
        {
            _adaptor = adaptor;
            _info = info;
        }

        public IEnumerable<TSelect> Read()
        {
            using (var con = _adaptor.CreateConnection())
            {
                con.Open();
                using (var com = _adaptor.CreateCommand())
                {
                    var text = CommandText;
                    Sql.Log?.Invoke(text);
                    com.CommandText = text;
                    com.Connection = con;
                    using (var sdr = com.ExecuteReader())
                    {
                        var reader = new DbResult(sdr);
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
    }
}
