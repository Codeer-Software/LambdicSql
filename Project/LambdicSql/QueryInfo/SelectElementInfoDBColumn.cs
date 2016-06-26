namespace LambdicSql.QueryInfo
{
    public class SelectElementInfoDBColumn : ISelectElementInfo
    {
        public string DbColumn { get; }

        public SelectElementInfoDBColumn(string dbColumn)
        {
            DbColumn = dbColumn;
        }
    }
}
