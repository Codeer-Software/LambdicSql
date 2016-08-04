namespace LambdicSql.QueryBase
{
    public class DecodeContext
    {
        public SelectClauseInfo SelectClauseInfo { get; internal set; }
        public DbInfo DbInfo { get; }
        public PrepareParameters Parameters { get; } = new PrepareParameters();

        public DecodeContext(DbInfo dbInfo)
        {
            DbInfo = dbInfo;
        }
    }
}
