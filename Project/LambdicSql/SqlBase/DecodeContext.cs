namespace LambdicSql.SqlBase
{
    public class DecodeContext
    {
        public ObjectCreateInfo SelectClauseInfo { get; internal set; }
        public DbInfo DbInfo { get; }
        public PrepareParameters Parameters { get; }

        public DecodeContext(DbInfo dbInfo, string prefix)
        {
            DbInfo = dbInfo;
            Parameters = new PrepareParameters(prefix);
        }
    }
}
