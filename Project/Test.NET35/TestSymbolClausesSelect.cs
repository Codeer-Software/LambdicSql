using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using System.Data.SqlClient;
using static Test.TestSynatax;

namespace Test
{
    [TestClass]
    public class TestSymbolClausesSelect
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
            var query = Db<DB>.Sql(db =>
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

            var query = Db<DB>.Sql(db =>
                Select(Top(1), new SelectData
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

            var exp1 = Db<DB>.Sql(db => 1);
            var exp2 = Db<DB>.Sql(db => Top(exp1));
            var query = Db<DB>.Sql(db =>
                Select(exp2.Body, new SelectData
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
            var query = Db<DB>.Sql(db =>
                Select(Asterisk()).
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
            var exp = Db<DB>.Sql(db => Asterisk());
            var query = Db<DB>.Sql(db =>
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
            var query = Db<DB>.Sql(db =>
                Select(Asterisk<Remuneration>()).
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
            var exp = Db<DB>.Sql(db => Asterisk<Remuneration>());

            var query = Db<DB>.Sql(db =>
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
            var query = Db<DB>.Sql(db =>
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
            var exp = Db<DB>.Sql(db => Asterisk(db.tbl_remuneration));

            var query = Db<DB>.Sql(db =>
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

            var query = Db<DB>.Sql(db =>
                Select(Top(2), Asterisk()).
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

            var query = Db<DB>.Sql(db =>
                Select(Top(2), Asterisk<Remuneration>()).
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
            var query = Db<DB>.Sql(db =>
                Select(All(), new SelectData
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
            var query = Db<DB>.Sql(db =>
                Select(Distinct(), new SelectData
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
            var query = Db<DB>.Sql(db =>
                Select(All(), Asterisk()).
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
            var query = Db<DB>.Sql(db =>
                Select(Distinct(), Asterisk()).
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
            var query = Db<DB>.Sql(db =>
                Select(All(), Asterisk(db.tbl_remuneration)).
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
            var query = Db<DB>.Sql(db =>
                Select(Distinct(), Asterisk(db.tbl_remuneration)).
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

            var query = Db<DB>.Sql(db =>
                Select(All(), Top(2), Asterisk()).
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

            var query = Db<DB>.Sql(db =>
                Select(Distinct(), Top(2), Asterisk()).
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

            var query = Db<DB>.Sql(db =>
                Select(All(), Top(2), Asterisk(db.tbl_remuneration)).
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

            var query = Db<DB>.Sql(db =>
                Select(Distinct(), Top(2), Asterisk(db.tbl_remuneration)).
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
            var query = Db<DB>.Sql(db =>
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

            var query = Db<DB>.Sql(db =>
                Empty().Select(Top(1), new SelectData
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(Asterisk()).
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(Asterisk<Remuneration>()).
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
            var query = Db<DB>.Sql(db =>
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

            var query = Db<DB>.Sql(db =>
                Empty().Select(Top(2), Asterisk()).
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

            var query = Db<DB>.Sql(db =>
                Empty().Select(Top(2), Asterisk<Remuneration>()).
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(All(), new SelectData
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(Distinct(), new SelectData
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(All(), Asterisk()).
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(Distinct(), Asterisk()).
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(All(), Asterisk(db.tbl_remuneration)).
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
            var query = Db<DB>.Sql(db =>
                Empty().Select(Distinct(), Asterisk(db.tbl_remuneration)).
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

            var query = Db<DB>.Sql(db =>
                Empty().Select(All(), Top(2), Asterisk()).
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

            var query = Db<DB>.Sql(db =>
                Empty().Select(Distinct(), Top(2), Asterisk()).
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

            var query = Db<DB>.Sql(db =>
                Empty().Select(All(), Top(2), Asterisk(db.tbl_remuneration)).
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

            var query = Db<DB>.Sql(db =>
                Empty().Select(Distinct(), Top(2), Asterisk(db.tbl_remuneration)).
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
            var sub = Db<DB>.Sql(db =>
                Select(Sum(db.tbl_remuneration.money)).
                From(db.tbl_remuneration));

            var exp = Db<DB>.Sql(db => db.tbl_remuneration.payment_date);
            
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    PaymentDate = exp,
                    Money = sub
                }).
                From(db.tbl_remuneration));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_remuneration.payment_date AS PaymentDate,
	(SELECT
		SUM(tbl_remuneration.money)
	FROM tbl_remuneration) AS Money
FROM tbl_remuneration");
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Single1()
        {
            var query = Db<DB>.Sql(db => Select(db.tbl_staff.id).From(db.tbl_staff));
            
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	tbl_staff.id
FROM tbl_staff");
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Single2()
        {
            var query = Db<DB>.Sql(db => Select(Count(Asterisk())).From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	COUNT(*)
FROM tbl_staff");
        }
    }
}
