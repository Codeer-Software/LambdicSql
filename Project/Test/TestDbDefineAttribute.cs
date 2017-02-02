using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class TestDbDefineAttribute
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

        [Table("tbl_staff")]
        public class StaffX
        {
            [Column("id")]
            public int idX { get; set; }
            [Column("name")]
            public string nameX { get; set; }
        }

        [Table("tbl_remuneration")]
        public class RemunerationX
        {
            [Column("id")]
            public int idX { get; set; }
            [Column("staff_id")]
            public int staff_idX { get; set; }
            [Column("payment_date")]
            public DateTime payment_dateX { get; set; }
            [Column("money")]
            public decimal moneyX { get; set; }
        }
        
        public class DBX
        {
            public StaffX tbl_staffX { get; set; }
            public RemunerationX tbl_remunerationX { get; set; }
        }

        [Table("dbo.tbl_staff")]
        public class StaffY
        {
            [Column("id")]
            public int idY { get; set; }
            [Column("name")]
            public string nameY { get; set; }
        }


        [Table("dbo.tbl_remuneration")]
        public class RemunerationY
        {
            [Column("id")]
            public int idY { get; set; }
            [Column("staff_id")]
            public int staff_idY { get; set; }
            [Column("payment_date")]
            public DateTime payment_dateY { get; set; }
            [Column("money")]
            public decimal moneyY { get; set; }
        }

        public class DBY
        {
            public StaffY tbl_staffY { get; set; }
            public RemunerationY tbl_remunerationY { get; set; }
        }

        public class SelectData
        {
            public string name { get; set; }
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Normal()
        {
            var query = Db<DBX>.Sql(db =>
                Select(new SelectData
                {
                    name = db.tbl_staffX.nameX,
                    payment_date = db.tbl_remunerationX.payment_dateX,
                    money = db.tbl_remunerationX.moneyX
                }).
                From(db.tbl_remunerationX).
                Join(db.tbl_staffX, db.tbl_staffX.idX == db.tbl_remunerationX.staff_idX));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	tbl_staff.name AS name,
	tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_staff.id) = (tbl_remuneration.staff_id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Schema()
        {
            var name = _connection.GetType().Name;
            if (name != "SqlConnection") return;

            var query = Db<DBY>.Sql(db =>
                Select(new SelectData
                {
                    name = db.tbl_staffY.nameY,
                    payment_date = db.tbl_remunerationY.payment_dateY,
                    money = db.tbl_remunerationY.moneyY
                }).
                From(db.tbl_remunerationY).
                Join(db.tbl_staffY, db.tbl_staffY.idY == db.tbl_remunerationY.staff_idY));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	dbo.tbl_staff.name AS name,
	dbo.tbl_remuneration.payment_date AS payment_date,
	dbo.tbl_remuneration.money AS money
FROM dbo.tbl_remuneration
	JOIN dbo.tbl_staff ON (dbo.tbl_staff.id) = (dbo.tbl_remuneration.staff_id)");
        }
    }
}
