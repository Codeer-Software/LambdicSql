namespace LambdicSql.SqlBase
{
    public class DecodeContext
    {
        public ObjectCreateInfo SelectClauseInfo { get; internal set; }
        public DbInfo DbInfo { get; }
        public PrepareParameters Parameters { get; } = new PrepareParameters();

        public DecodeContext(DbInfo dbInfo)
        {
            DbInfo = dbInfo;
        }
    }
}
