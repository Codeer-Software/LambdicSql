﻿using System.Data;
using System.Linq;
using Test.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Test.Helper.DBProviderInfo;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Symbol;
using System;

namespace Test
{
    [TestClass]
    public class TestDbDefine
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
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Normal()
        {
            var query = Db<DB>.Sql(db =>
                Select(new SelectData
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money
                }).
                From(db.tbl_remuneration).
                Join(db.tbl_staff, db.tbl_staff.id == db.tbl_remuneration.staff_id));

            query.Gen(_connection);

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

            var query = Db<DBEX>.Sql(db =>
                Select(new SelectData
                {
                    name = db.dbo.tbl_staff.name,
                    payment_date = db.dbo.tbl_remuneration.payment_date,
                    money = db.dbo.tbl_remuneration.money
                }).
                From(db.dbo.tbl_remuneration).
                Join(db.dbo.tbl_staff, db.dbo.tbl_staff.id == db.dbo.tbl_remuneration.staff_id));

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