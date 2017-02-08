using System;
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
    public class TestSymbolClausesFrom
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
            public string Name { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From_Start()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }) +
                From(db.tbl_remuneration));

            sql.Gen(_connection);

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From_Multi()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration, db.tbl_staff).
                Where(db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_staff.name AS Name,
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration, tbl_staff
WHERE (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join_Start()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration) +
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join_Vanish()
        {
            var tbl_staff = new Sql<Staff>();
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(tbl_staff, db.tbl_remuneration.staff_id == tbl_staff.Body.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join_Vanish_Start()
        {
            var tbl_staff = new Sql<Staff>();
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration) +
                    Join(tbl_staff, db.tbl_remuneration.staff_id == tbl_staff.Body.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_LeftJoin()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    LeftJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_LeftJoin_Start()
        {
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration) +
                    LeftJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RightJoin()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    RightJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RightJoin_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.MySQL, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration) +
                    RightJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Full_Join()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    FullJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	FULL JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Full_Join_Start()
        {
            if (!_connection.IsTarget(TargetDB.SqlServer, TargetDB.Oracle, TargetDB.Postgre, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration) + 
                    FullJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	FULL JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CrossJoin()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    CrossJoin(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CrossJoin_Start()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration) + 
                    CrossJoin(db.tbl_staff));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From_SubQuery_1()
        {
            var sub = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = sub.Body.payment_date,
                    Money = sub.Body.money,
                }).
                From(sub.Body));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            sql.Gen(_connection);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	sub.payment_date AS PaymentDate,
	sub.money AS Money
FROM
	(SELECT *
	FROM tbl_remuneration) sub");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From_SubQuery_2()
        {
            if (!_connection.IsTarget(TargetDB.SQLite, TargetDB.Oracle, TargetDB.DB2)) return;

            var sql = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(
                    Select(Asterisk(db.tbl_remuneration)).
                        From(db.tbl_remuneration)
                    ));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT *
FROM
	(SELECT *
	FROM tbl_remuneration)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From_Aliase()
        {
            var expRemuneration = Db<DB>.Sql(db => db.tbl_remuneration);
            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = expStaff.Body.name,
                    PaymentDate = expRemuneration.Body.payment_date,
                    Money = expRemuneration.Body.money,
                }).
                From(expRemuneration, expStaff).
                Where(expRemuneration.Body.staff_id == expStaff.Body.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	expStaff.name AS Name,
	expRemuneration.payment_date AS PaymentDate,
	expRemuneration.money AS Money
FROM tbl_remuneration expRemuneration, tbl_staff expStaff
WHERE (expRemuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join_Aliase()
        {
            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = expStaff.Body.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(expStaff, exp2));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	expStaff.name AS Name,
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join_SubQuery()
        {
            var sub = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var sql = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = sub.Body.payment_date,
                    Money = sub.Body.money,
                }).
                From(db.tbl_staff).
                Join(sub, sub.Body.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(sql).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(sql, _connection,
@"SELECT
	sub.payment_date AS PaymentDate,
	sub.money AS Money
FROM tbl_staff
	JOIN
		(SELECT *
		FROM tbl_remuneration) sub
		ON
		(sub.staff_id) = (tbl_staff.id)");
        }
    }
}
