using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Helper
{
    public static class DBProviderInfo
    {
        public const string Operation = "System.Data.OleDB";
        public const string Connection = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Params.xlsx; Extended Properties='Excel 12.0;HDR=yes';";
        public const string Sheet = "DB$";
        public const DataAccessMethod Method = DataAccessMethod.Sequential;
    }
}
