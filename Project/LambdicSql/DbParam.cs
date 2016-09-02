using LambdicSql.Inside;
using System.Data;

namespace LambdicSql
{
    public class DbParam
    {
        public object Value { get; set; }
        public DbType? DbType { get; set; }
        public ParameterDirection? Direction { get; set; }
        public string SourceColumn { get; set; }
        public DataRowVersion? SourceVersion { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }
        public int? Size { get; set; }

        internal static DbParam Parse(string dbTypeText)
        {
            dbTypeText = dbTypeText.ToLower().Trim();
            var index = dbTypeText.IndexOf("(");
            string type = dbTypeText;
            string num = string.Empty;
            if (index != -1)
            {
                type = dbTypeText.Substring(0, index);
                num = dbTypeText.Replace(type, string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
            }
            DbType? dbType = null;
            switch (type)
            {
                case "nchar": dbType = System.Data.DbType.StringFixedLength; break;
                case "char": dbType = System.Data.DbType.AnsiStringFixedLength; break;
                case "varchar": dbType = System.Data.DbType.AnsiString; break;
                case "nvarchar": dbType = System.Data.DbType.String; break;
                case "datetime": dbType = System.Data.DbType.DateTime; break;
                case "datetime2": dbType = System.Data.DbType.DateTime2; break;
                default:return null;
            }
            var param = new DbParam() { DbType = dbType };
            if (!string.IsNullOrEmpty(num)) param.Size = int.Parse(num);
            return param;
        }

        internal DbParam Clone()=> (DbParam)MemberwiseClone();
    }

    public class DbParam<T> : DbParam
    {
        public static implicit operator T(DbParam<T> src) => InvalitContext.Throw<T>("new DbParameter<T>");
    }

    //TODO 良く使う DbStringとDbTimeは別クラスにしておくか
}
