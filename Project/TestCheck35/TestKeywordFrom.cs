using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;

namespace TestCheck35
{
    [TestClass]
    public class TestKeywordFrom
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
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From2()
        {
            var sub = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var query = Sql<DB>.Create(db =>
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
        public void Test_From3()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "NpgsqlConnection") return;
            if (name == "MySqlConnection") return;

            var query = Sql<DB>.Create(db =>
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
            var query = Sql<DB>.Create(db =>
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
FROM tbl_remuneration,tbl_staff
WHERE (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_From5()
        {
            var expRemuneration = Sql<DB>.Create(db => db.tbl_remuneration);
            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var query = Sql<DB>.Create(db =>
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
FROM tbl_remuneration expRemuneration,tbl_staff expStaff
WHERE (expRemuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Join2()
        {
            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var exp2 = Sql<DB>.Create(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(expStaff, exp2));
            
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_LeftJoin1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    LeftJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_LeftJoin2()
        {
            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var exp2 = Sql<DB>.Create(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    LeftJoin(expStaff, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
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

            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    RightJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
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

            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var exp2 = Sql<DB>.Create(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    RightJoin(expStaff, exp2));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_CrossJoin1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    CrossJoin(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
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

            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    CrossJoin(expStaff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff expStaff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Join1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Join2()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var exp2 = Sql<DB>.Create(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var target = Sql<DB>.Create(db => Join(expStaff, exp2));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_LeftJoin1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => LeftJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	LEFT JOIN tbl_staff ON (tbl_remuneration.staff_id) = (tbl_staff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_LeftJoin2()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var exp2 = Sql<DB>.Create(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var target = Sql<DB>.Create(db => LeftJoin(expStaff, exp2));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
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

            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => RightJoin(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
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

            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var expStaff = Sql<DB>.Create(db => db.tbl_staff);
            var exp2 = Sql<DB>.Create(db => db.tbl_remuneration.staff_id == expStaff.Body.id);
            var target = Sql<DB>.Create(db => RightJoin(expStaff, exp2));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	RIGHT JOIN tbl_staff expStaff ON (tbl_remuneration.staff_id) = (expStaff.id)");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_CrossJoin1()
        {
            if (_connection.GetType().Name == "SQLiteConnection") return;

            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => CrossJoin(db.tbl_staff));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
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

            var exp = Sql<DB>.Create(db => db.tbl_staff);
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var target = Sql<DB>.Create(db => CrossJoin(exp));
            query = query.Concat(target);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            Assert.AreEqual(query.ToSqlInfo(_connection.GetType()).SqlText,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration
	CROSS JOIN tbl_staff exp");
        }
    }
}
//TODO Body に関するテストはやっておく必要があるが、掛け算でなくてもよいはず。そのような実装にしておくべき！→これをりファクタで強化