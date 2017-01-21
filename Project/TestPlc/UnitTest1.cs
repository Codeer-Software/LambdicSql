using LambdicSql;
using static LambdicSql.Symbol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TestPlc
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sql = Db<DB>.Sql(db =>

                Select(Asterisk()).
                From(db.tbl_staff)
            
            );

            BuildedSql x = null;
            IDbParam y = null;

            var ret = sql.Build(GetType());
            Debug.Print(ret.Text);
        }
    }
}
