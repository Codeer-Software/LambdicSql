using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;

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
        public void Test_From1()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From2()
        {
            var sub = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = sub.Body.payment_date,
                    Money = sub.Body.money,
                }).
                From(sub));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	sub.payment_date AS PaymentDate,
	sub.money AS Money
FROM
	(SELECT *
	FROM tbl_remuneration) sub");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From2_2()
        {
            var sub = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = sub.Body.payment_date,
                    Money = sub.Body.money,
                }).
                From(sub.Body));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            query.Gen(_connection);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	sub.payment_date AS PaymentDate,
	sub.money AS Money
FROM
	(SELECT *
	FROM tbl_remuneration) sub");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From3()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "NpgsqlConnection") return;
            if (name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(
                    Select(Asterisk(db.tbl_remuneration)).
                        From(db.tbl_remuneration)
                    ));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM
	(SELECT *
	FROM tbl_remuneration)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From4()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = db.tbl_staff.name,
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration, db.tbl_staff).
                Where(db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.name AS Name,
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration, tbl_staff
WHERE (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From5()
        {
            var expRemuneration = Db<DB>.Sql(db => db.tbl_remuneration);
            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    Name = expStaff.Body.name,
                    PaymentDate = expRemuneration.Body.payment_date,
                    Money = expRemuneration.Body.money,
                }).
                From(expRemuneration, expStaff).
                Where(expRemuneration.Body.staff_id == expStaff.Body.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	expStaff.name AS Name,
	expRemuneration.payment_date AS PaymentDate,
	expRemuneration.money AS Money
FROM tbl_remuneration expRemuneration, tbl_staff expStaff
WHERE (expRemuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join1()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join2()
        {
            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(expStaff, exp2));
            
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_LeftJoin1()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    LeftJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_LeftJoin2()
        {
            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    LeftJoin(expStaff, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RightJoin1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    RightJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RightJoin2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    RightJoin(expStaff, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }


        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Full_Join1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    FullJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	FULL JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Full_Join2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    FullJoin(expStaff, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	FULL JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Join1()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CrossJoin1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    CrossJoin(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CrossJoin2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    CrossJoin(expStaff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff expStaff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Join2()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var target = Db<DB>.Sql(db => Join(expStaff, exp2));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_LeftJoin1()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => LeftJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_LeftJoin2()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var target = Db<DB>.Sql(db => LeftJoin(expStaff, exp2));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_RightJoin1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => RightJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_RightJoin2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var target = Db<DB>.Sql(db => RightJoin(expStaff, exp2));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_FullJoin1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => FullJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	FULL JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_FullJoin2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;
            if (_connection.GetType().Name == "MySqlConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var expStaff = Db<DB>.Sql(db => db.tbl_staff);
            var exp2 = Db<DB>.Sql(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var target = Db<DB>.Sql(db => FullJoin(expStaff, exp2));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	FULL JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_CrossJoin1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => CrossJoin(db.tbl_staff));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_CrossJoin2()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var exp = Db<DB>.Sql(db => db.tbl_staff);
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Db<DB>.Sql(db => CrossJoin(exp));
            query = query + target;

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff exp");
        }
    }
}
//TODO Body に関するテストはやっておく必要があるが、掛け算でなくてもよいはず。そのような実装にしておくべき！→これをりファクタで強化