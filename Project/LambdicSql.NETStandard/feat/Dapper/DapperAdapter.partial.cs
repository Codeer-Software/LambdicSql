namespace LambdicSql.feat.Dapper
{
    public static partial class DapperAdapter
    {
        const string AssemblyInitializeComment = "using System.Reflection;\r\nDapperAdapter.Assembly = typeof(Dapper.SqlMapper).GetTypeInfo().Assembly;";
    }
}
