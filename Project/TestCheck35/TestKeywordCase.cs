using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//important
using LambdicSql;
using LambdicSql.feat.Dapper;
using static LambdicSql.Keywords;
using LambdicSql.SqlBase;
using System.Linq.Expressions;

namespace TestCheck35
{
    public class TestKeywordCase : ITest
    {
        public IDbConnection _connection;

        public void TestInitialize(IDbConnection connection)
        {
            _connection = connection;
        }

        public class SelectData
        {
            public string Type { get; set; }
        }

        public void Test_Case1()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Type = Case().
                                When(db.tbl_staff.id == 3).Then("x").
                                When(db.tbl_staff.id == 4).Then("y").
                                Else("z").
                            End()
                }).
                From(db.tbl_staff));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT
	CASE
		WHEN (tbl_staff.id) = (@p_0) THEN @p_1
		WHEN (tbl_staff.id) = (@p_2) THEN @p_3
		ELSE @p_4
	END AS Type
FROM tbl_staff");
        }

        public void Test_Case2()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Type = Case().
                                When(db.tbl_staff.id == 3).Then("x").
                                When(db.tbl_staff.id == 4).Then("y").
                            End()
                }).
                From(db.tbl_staff));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CASE
		WHEN (tbl_staff.id) = (@p_0) THEN @p_1
		WHEN (tbl_staff.id) = (@p_2) THEN @p_3
	END AS Type
FROM tbl_staff");
        }

        public void Test_Case3()
        {
            var query = Sql<DB>.Create(db =>
                Select(new SelectData()
                {
                    Type = Case(db.tbl_staff.id).
                                When(3).Then("x").
                                When(4).Then("y").
                                Else("z").
                            End()
                }).
                From(db.tbl_staff));
            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT
	CASE tbl_staff.id
		WHEN @p_0 THEN @p_1
		WHEN @p_2 THEN @p_3
		ELSE @p_4
	END AS Type
FROM tbl_staff");
        }
    }
}
