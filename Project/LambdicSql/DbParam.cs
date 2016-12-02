using LambdicSql.Inside;
using System.Data;

namespace LambdicSql
{
    /// <summary>
    /// Represents a parameter information.
    /// </summary>
    public class DbParam
    {
        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the DbType of the parameter.
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.
        /// </summary>
        public ParameterDirection? Direction { get; set; }

        /// <summary>
        /// Gets or sets the name of the source column that is mapped to the DataSet and used for loading or returning the Value.
        /// </summary>
        public string SourceColumn { get; set; }

        /// <summary>
        /// Gets or sets the DataRowVersion to use when loading Value.
        /// </summary>
        public DataRowVersion? SourceVersion { get; set; }

        /// <summary>
        /// Indicates the precision of numeric parameters.
        /// </summary>
        public byte? Precision { get; set; }

        /// <summary>
        /// Indicates the scale of numeric parameters.
        /// </summary>
        public byte? Scale { get; set; }

        /// <summary>
        /// The size of the parameter.
        /// </summary>
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
}
