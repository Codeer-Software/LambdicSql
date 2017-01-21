using LambdicSql;
using static LambdicSql.Symbol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TestPlc
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var sql = Db<DB>.Sql(db =>

                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff)
            
            );

            var con = TestEnvironment.CreateConnection(TestContext);

            var ret = sql.Build(con.GetType());

            var list = con.Query<Staff>(ret.Text);

            Debug.Print(ret.Text);
        }
    }
}
