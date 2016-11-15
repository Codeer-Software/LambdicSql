﻿using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using LambdicSql.SqlBase;
using System.Linq.Expressions;
using System.Diagnostics;

namespace TestCheck35
{
    public class TestKeywordFrom
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

        public class Staff
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Remuneration
        {
            public int id { get; set; }
            public int staff_id { get; set; }
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public int val1 { get; set; }
            public string val2 { get; set; }
        }

        public class DB
        {
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
            public Data tbl_data { get; set; }
        }

        public class SelectData
        {
            public DateTime PaymentDate { get; set; }
            public decimal Money { get; set; }
        }

        public void Test_Join()
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

        public void Test_LeftJoin()
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

        public void Test_RightJoin()
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

        public void Test_CrossJoin()
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
        
        public void Test_Continue_Join()
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

        public void Test_Continue_LeftJoin()
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

        public void Test_Continue_RightJoin()
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

        public void Test_Continue_CrossJoin()
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
    }
}
