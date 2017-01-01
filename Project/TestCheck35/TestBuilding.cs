using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using System;

namespace TestCheck35
{
    [TestClass]
    public class TestBuilding
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
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        public static readonly SqlExpression<SelectData> Select = Sql<DB>.Of(db =>
            Select(new SelectData
            {
                PaymentDate = db.tbl_remuneration.payment_date,
                Money = db.tbl_remuneration.money,
            }));

        public static readonly ISqlExpression From = Sql<DB>.Of(db =>
            From(db.tbl_remuneration));

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test()
        {
            var query = Select.Concat(From);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }
    }
}
