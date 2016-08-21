using LambdicSql.SqlBase;

namespace LambdicSql
{
    [SqlSyntax]
    public enum DateTiemElement
    {
        Year,
        Quarter,
        Month,
        Dayofyear,
        Day,
        Week,
        Weekday,
        Hour,
        Minute,
        Second,
        Millisecond,
        Microsecond,
        Nanosecond,
        ISO_WEEK,
    }
}
