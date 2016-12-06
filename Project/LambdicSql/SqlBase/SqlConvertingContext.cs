namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Context of SQL conversion.
    /// </summary>
    public class SqlConvertingContext
    {
        /// <summary>
        /// Object creat info.
        /// </summary>
        public ObjectCreateInfo ObjectCreateInfo { get; internal set; }

        /// <summary>
        /// Data Base info.
        /// </summary>
        public DbInfo DbInfo { get; }

        /// <summary>
        /// Parameters.
        /// </summary>
        public ParameterInfo Parameters { get; }

        internal SqlConvertingContext(DbInfo dbInfo, string prefix)
        {
            DbInfo = dbInfo;
            Parameters = new ParameterInfo(prefix);
        }
    }
}
