using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;
using System.Collections.Generic;
using LambdicSql.SqlBase;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static TestCheck35.TestSynatax;

namespace TestCheck35
{
    public class TestKeywordSelect
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

        public class SelectData
        {
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

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

        public void Test_Select_Top()
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

        public void Test_Select_Asterisk_Non()
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

        public void Test_Select_Asterisk()
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

        public void Test_Select_Asterisk_Helper()
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
    }
}
