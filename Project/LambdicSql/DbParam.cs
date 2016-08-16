using LambdicSql.Inside;
using System.Data;

namespace LambdicSql
{
    public class DbParam
    {
        public object Value { get; private set; }
        public DbType? DbType { get; set; }
        public ParameterDirection? Direction { get; set; }
        public bool? IsNullable { get; }
        public string SourceColumn { get; set; }
        public DataRowVersion? SourceVersion { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }
        public int? Size { get; set; }

        internal DbParam(object value)
        {
            Value = value;
        }
    }

    public class DbParam<T> : DbParam
    {
        public DbParam(T value) : base(value) { }
        public static implicit operator T(DbParam<T> src) => InvalitContext.Throw<T>("new DbParameter<T>");
    }
}
