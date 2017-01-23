using LambdicSql.ConverterServices;
using System.Data;

namespace LambdicSql
{
    /// <summary>
    /// Represents a parameter information.
    /// </summary>
    public class DbParam : IDbParam
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

        internal DbParam Clone()=> (DbParam)MemberwiseClone();
    }

    /// <summary>
    /// Represents a parameter information.
    /// </summary>
    /// <typeparam name="T">The type that the parameter represents</typeparam>
    public class DbParam<T> : DbParam
    {
        /// <summary>
        /// Converter for converting to T
        /// </summary>
        /// <param name="src">Source.</param>
        public static implicit operator T(DbParam<T> src) { throw new InvalitContextException("new DbParameter<T>"); }
    }
}
