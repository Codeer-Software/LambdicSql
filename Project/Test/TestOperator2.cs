using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using static Test.Symbol;

namespace Test
{
    [TestClass]
    public class TestOperator2
    {
        static Sql<Staff> SelectStaffAll => Db<DB>.Sql(db =>
                 Select(Asterisk(db.tbl_staff)));

        [TestMethod]
        public void Test()
        {
            var sql = Db<DB>.Sql(db =>
            SelectStaffAll +
            From(db.tbl_staff));
        }

        [TestMethod]
        public void Success()
        {
            var from = Db<DB>.Sql(db =>From(db.tbl_staff));

            var sql = SelectStaffAll + from;

        }
    }
}
