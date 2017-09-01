using LambdicSql;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.feat.Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using static Test.Symbol;

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



        public class TableExAttribute : TableAttribute
        {
            public TableExAttribute(string name) : base(name)
            {
            }
        }

        public class ColumnExAttribute : ColumnAttribute
        {
            public ColumnExAttribute(string name) : base(name)
            {
            }
        }

        [TableEx("tbl_staff")]
        public class StaffZ
        {
            [ColumnEx("id")]
            public int idZ { get; set; }
            [ColumnEx("name")]
            public string nameZ { get; set; }
        }

        [TableEx("tbl_remuneration")]
        public class RemunerationZ
        {
            [ColumnEx("id")]
            public int idZ { get; set; }
            [ColumnEx("staff_id")]
            public int staff_idZ { get; set; }
            [ColumnEx("payment_date")]
            public DateTime payment_dateZ { get; set; }
            [ColumnEx("money")]
            public decimal moneyZ { get; set; }
        }

        public class DBZ
        {
            public StaffZ tbl_staffZ { get; set; }
            public RemunerationZ tbl_remunerationZ { get; set; }
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
            var sql = Db<DBX>.Sql(db =>
                Select(new SelectData
                {
                    name = db.tbl_staffX.nameX,
                    payment_date = db.tbl_remunerationX.payment_dateX,
                    money = db.tbl_remunerationX.moneyX
                }).
                From(db.tbl_remunerationX).
                Join(db.tbl_staffX, db.tbl_staffX.idX == db.tbl_remunerationX.staff_idX));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.name AS name,
	tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money
FROM tbl_remuneration
	JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Schema()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;

            var sql = Db<DBY>.Sql(db =>
                Select(new SelectData
                {
                    name = db.tbl_staffY.nameY,
                    payment_date = db.tbl_remunerationY.payment_dateY,
                    money = db.tbl_remunerationY.moneyY
                }).
                From(db.tbl_remunerationY).
                Join(db.tbl_staffY, db.tbl_staffY.idY == db.tbl_remunerationY.staff_idY));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	dbo.tbl_staff.name AS name,
	dbo.tbl_remuneration.payment_date AS payment_date,
	dbo.tbl_remuneration.money AS money
FROM dbo.tbl_remuneration
	JOIN dbo.tbl_staff ON dbo.tbl_staff.id = dbo.tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_AttributeInheritance()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer)) return;

            var sql = Db<DBZ>.Sql(db =>
                Select(new SelectData
                {
                    name = db.tbl_staffZ.nameZ,
                    payment_date = db.tbl_remunerationZ.payment_dateZ,
                    money = db.tbl_remunerationZ.moneyZ
                }).
                From(db.tbl_remunerationZ).
                Join(db.tbl_staffZ, db.tbl_staffZ.idZ == db.tbl_remunerationZ.staff_idZ));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
 @"SELECT
	tbl_staff.name AS name,
	tbl_remuneration.payment_date AS payment_date,
	tbl_remuneration.money AS money
FROM tbl_remuneration
	JOIN tbl_staff ON tbl_staff.id = tbl_remuneration.staff_id");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_SysTable()
        {
            var sql1 = Db<DB>.Sql(db => Sys.X);
            AssertEx.AreEqual(sql1, _connection, @"X");
            var sql2 = Db<DB>.Sql(db => Sys.X.id);
            AssertEx.AreEqual(sql2, _connection, @"X.id");
            var sql3 = Db<DB>.Sql(db => Sys.X2);
            AssertEx.AreEqual(sql3, _connection, @"System");
            var sql4 = Db<DB>.Sql(db => Sys.X2.id);
            AssertEx.AreEqual(sql4, _connection, @"System.id");

            var sql = Db<DB>.Sql(db =>
            Select(sql3.Body.id).From(sql3));
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	sql3.id
FROM System sql3");
        }

        SysDefine Sys2 => null;

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_SysTable_2()
        {
            var sql1 = Db<DB>.Sql(db => Sys2.X);
            AssertEx.AreEqual(sql1, _connection, @"X");
            var sql2 = Db<DB>.Sql(db => Sys2.X.id);
            AssertEx.AreEqual(sql2, _connection, @"X.id");
            var sql3 = Db<DB>.Sql(db => Sys2.X2);
            AssertEx.AreEqual(sql3, _connection, @"System");
            var sql4 = Db<DB>.Sql(db => Sys2.X2.id);
            AssertEx.AreEqual(sql4, _connection, @"System.id");

            var sql = Db<DB>.Sql(db =>
            Select(sql3.Body.id).From(sql3));
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	sql3.id
FROM System sql3");
        }
    }

    public class X
    {
        public int id { get; set; }
    }

    public static class Sys
    {
        [MemberTableConverter]
        public static X X { get; set; }
        [MemberTableConverter(Name = "System")]
        public static X X2 { get; set; }
    }

    public class SysDefine
    {
        [MemberTableConverter]
        public X X { get; set; }

        [MemberTableConverter(Name = "System")]
        public X X2 { get; set; }
    }

}
