using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using System.Data.SqlClient;
using static TestCheck35.TestSynatax;

namespace TestCheck35
{
    [TestClass]
    public class TestKeywordSelect
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select()
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
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Top1()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Select(new Top(1), new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT TOP 1
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Top2()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var exp1 = Sql<DB>.Create(db => 1);
            var exp2 = Sql<DB>.Create(db => new Top(exp1));
            var query = Sql<DB>.Create(db =>
                Select(exp2, new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT TOP 1
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Asterisk_Non1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Asterisk_Non2()
        {
            var exp = Sql<DB>.Create(db => new Asterisk());
            var query = Sql<DB>.Create(db =>
                Select(exp.Body).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Asterisk1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new Asterisk<Remuneration>()).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Asterisk2()
        {
            var exp = Sql<DB>.Create(db => new Asterisk<Remuneration>());

            //TODO あれ？これSelect句の中で頑張ったきがする
            var query = Sql<DB>.Create(db =>
                Select(exp.Body).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Asterisk_Helper1()
        {
            var query = Sql<DB>.Create(db =>
                Select(Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Asterisk_Helper2()
        {
            var exp = Sql<DB>.Create(db => Asterisk(db.tbl_remuneration));

            var query = Sql<DB>.Create(db =>
                Select(exp.Body).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Top_Asterisk_Non()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Select(new Top(2), new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Top_Asterisk()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Select(new Top(2), new Asterisk<Remuneration>()).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_All()
        {
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.All, new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Distinct()
        {
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.Distinct, new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_All_Asterisk_Non()
        {
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.All, new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Asterisk_Non()
        {
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.Distinct, new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_All_Asterisk()
        {
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.All, Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Asterisk()
        {
            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.Distinct, Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_All_Top_Asterisk_Non()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.All, new Top(2), new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Top_Asterisk_Non()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.Distinct, new Top(2), new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_All_Top_Asterisk()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.All, new Top(2), Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Select_Distinct_Top_Asterisk()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Select(AggregatePredicate.Distinct, new Top(2), Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Top()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Empty().Select(new Top(1), new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT TOP 1
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Asterisk_Non()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Asterisk()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(new Asterisk<Remuneration>()).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Asterisk_Helper()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Top_Asterisk_Non()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Empty().Select(new Top(2), new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Top_Asterisk()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Empty().Select(new Top(2), new Asterisk<Remuneration>()).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_All()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.All, new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.Distinct, new SelectData
                {
                    PaymentDate = db.tbl_remuneration.payment_date,
                    Money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT
	tbl_remuneration.payment_date AS PaymentDate,
	tbl_remuneration.money AS Money
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Asterisk_Non()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.All, new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Asterisk_Non()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.Distinct, new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Asterisk()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.All, Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Asterisk()
        {
            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.Distinct, Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Top_Asterisk_Non()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.All, new Top(2), new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Top_Asterisk_Non()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.Distinct, new Top(2), new Asterisk()).
                From(db.tbl_remuneration));

            var datas = _connection.Query<Remuneration>(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_All_Top_Asterisk()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.All, new Top(2), Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT ALL TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Select_Distinct_Top_Asterisk()
        {
            if (_connection.GetType() != typeof(SqlConnection)) return;

            var query = Sql<DB>.Create(db =>
                Empty().Select(AggregatePredicate.Distinct, new Top(2), Asterisk(db.tbl_remuneration)).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT DISTINCT TOP 2 *
FROM tbl_remuneration");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_SubQuery_Exp()
        {
            var sub = Sql<DB>.Create(db =>
                Select(new { Total = Sum(db.tbl_remuneration.money) }).
                From(db.tbl_remuneration));

            var exp = Sql<DB>.Create(db => db.tbl_remuneration.payment_date);

            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    PaymentDate = exp,
                    Money = sub.Cast<decimal>()
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	(SELECT
		SUM(tbl_remuneration.money) AS Total
	FROM tbl_remuneration) AS Money
FROM tbl_remuneration");
        }
    }
}
