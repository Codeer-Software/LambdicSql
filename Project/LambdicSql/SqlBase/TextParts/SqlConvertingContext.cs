namespace LambdicSql.SqlBase.TextParts
{
    /// <summary>
    /// Context of SQL conversion.
    /// </summary>
    public class SqlConvertingContext
    {
        /// <summary>
        /// Option
        /// </summary>
        public SqlConvertOption Option { get; }

        /// <summary>
        /// Parameter info.
        /// </summary>
        public ParameterInfo ParameterInfo { get; }

        /// <summary>
        /// Object creat info.
        /// </summary>
        public ObjectCreateInfo ObjectCreateInfo { get; internal set; }

        internal SqlConvertingContext(SqlConvertOption option)
        {
            Option = option;
            ParameterInfo = new ParameterInfo(option.ParameterPrefix);
        }
    }
}
