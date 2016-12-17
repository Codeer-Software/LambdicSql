using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Inside
{
    class CustomizeColumnOnly : ISqlTextCustomizer
    {
        public SqlText Custom(SqlText src)
        {
            var col = src as DbColumnText;
            if (col == null) return src;
            return new DbColumnText(col.Info, true);
        }
    }
}
