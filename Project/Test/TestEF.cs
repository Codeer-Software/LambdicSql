using System.Data;
using System.Linq;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.EntityFramework;
using static LambdicSql.Symbol;
using TestCheck35;
using Test.Model;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class TestEF
    {
        public TestContext TestContext { get; set; }
        public IDbConnection _connection;

        [TestInitialize]
        public void TestInitialize()
        {
            _connection = TestEnvironment.CreateConnection(TestContext);
            _connection.Open();
        }

        [TestCleanup]
        public void TestCleanup() => _connection.Dispose();

        public class SelectData
        {
            public string name { get; set; }
            public string payment_date { get; set; }
            public decimal? money { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection") return;

            var query = Db<ModelLambdicSqlTestDB>.Sql(db =>
                Select(new SelectData
                {
                    name = db.tbl_staff.T().name,
                    payment_date = db.tbl_remuneration.T().payment_date,
                    money = db.tbl_remuneration.T().money
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_staff.T().id == db.tbl_remuneration.T().staff_id));

            EFAdapter.Log = e => Debug.Print(e);

            var datas = query.SqlQuery(new ModelLambdicSqlTestDB()).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	tbl_staff.name AS name,
	tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_staff.id) = (tbl_remuneration.staff_id)");
        }

        void XXX()
        {
            //cout → int
            //avg → double

            var db = new ModelLambdicSqlTestDB();
            var sum = db.tbl_remuneration.Sum(e => e.money);
        }
    }
}
