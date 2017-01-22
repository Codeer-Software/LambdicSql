using LambdicSql;
using static LambdicSql.Symbol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System;

namespace TestPlc
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var x = new { a = 1, b = 2 };

            
            int id = 2;
            var sql = Db<DB>.Sql(db =>

                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(db.tbl_staff.id == id || db.tbl_staff.id == 4 || db.tbl_staff.id < id)

            );

            using (var con = TestEnvironment.CreateConnection(TestContext))
            {

                var ret = sql.Build(con.GetType());
                //ret.GetParamValues()
                var y = new object[] { 2, 4 };
                var z = ret.GetParamValues();
                var list = con.Query<Staff>(ret.Text, ret.GetParamValues());

                Debug.Print(ret.Text);
            }
        }

        class GetterCore<T0> { }
    }
}
