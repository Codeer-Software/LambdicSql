﻿using System;
using System.Data;
using System.Linq;
using Test.Helper;
using static Test.Helper.DBProviderInfo;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using static LambdicSql.Funcs;
using static LambdicSql.Utils;
using LambdicSql.SqlBase;
using System.Linq.Expressions;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCheck35
{
    [TestClass]
    public class TestDbParam
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
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Param()
        {
            var text = new DbParam<string>() { Value = "xxx" };
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    Name = db.tbl_staff.name + text,
                }).
                From(db.tbl_staff));

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	(tbl_staff.name) " + _connection.GetStringAddExp() + @" (@text) AS Name
FROM tbl_staff",
 new DbParams() { { "@text", new DbParam() { Value = "xxx" } } });
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Param_Direct()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData
                {
                    Name = db.tbl_staff.name
                        + new DbParam<string>() { Value = "xxx", DbType = DbType.AnsiStringFixedLength, Size = 10 },
                }).
                From(db.tbl_staff));


            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	(tbl_staff.name) " + _connection.GetStringAddExp() + @" (@p_0) AS Name
FROM tbl_staff",
 new DbParams() { { "@p_0", new DbParam() { Value = "xxx", DbType = DbType.AnsiStringFixedLength, Size = 10 } } });
        }

        //TODO 時間はいるよね

        //TODO あれ？正しくDBに伝わったことってどうやってみる？→設計を変更するか？

        /*
    public void Test_Param_Detail()
    {
        var a = TestParamCore("a", DbType.AnsiString).DbParams.First().Value;
        Assert.AreEqual(a.DbType, DbType.AnsiString);
        Assert.AreEqual(a.Value, "a");
        var b = TestParamCore("b", DbType.String).DbParams.First().Value;
        Assert.AreEqual(b.DbType, DbType.String);
        Assert.AreEqual(b.Value, "b");
    }

    public SqlInfo TestParamCore(string value, DbType type)
    {
        var query = Sql<Data>.Create(db => new DbParam<string>() { Value = value, DbType = type });
        var info = query.ToSqlInfo(typeof(SqlConnection));//いやいや。
        Debug.Print(info.SqlText);
        return info;
    }*/
    }
}
