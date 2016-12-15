﻿using System.Data;
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
    public class TestKeywordLimit
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

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Limit1()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "OracleConnection") return;
            if (name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 Limit(1, 3)
                 );

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0, @p_1",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Limit2()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "OracleConnection") return;
            if (name == "NpgsqlConnection") return;

            var exp1 = Sql<DB>.Create(db => (long)1);
            var exp2 = Sql<DB>.Create(db => (long)3);
            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 Limit(exp1, exp2)
                 );

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0, @p_1",
(long)1, (long)3);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Limit_Offset1()
        {
            var name = _connection.GetType().FullName;
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 Limit(1).
                 Offset(3)
                 );

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0
OFFSET @p_1",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Limit_Offset2()
        {
            var name = _connection.GetType().FullName;
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var exp1 = Sql<DB>.Create(db =>(long)1);
            var exp2 = Sql<DB>.Create(db => (long)3);
            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 Limit(exp1).
                 Offset(exp2)
                 );

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0
OFFSET @p_1",
(long)1, (long)3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OffsetRows_FetchNext1()
        {
            var name = _connection.GetType().FullName;
            if (name != "System.Data.SqlClient.SqlConnection" &&
                name != "IBM.Data.DB2.DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 OffsetRows(1).
                 FetchNextRowsOnly(3)
                 );

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
OFFSET @p_0 ROWS
FETCH NEXT @p_1 ROWS ONLY",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_OffsetRows_FetchNext2()
        {
            var name = _connection.GetType().FullName;
            if (name != "System.Data.SqlClient.SqlConnection" &&
                name != "IBM.Data.DB2.DB2Connection") return;

            var exp1 = Sql<DB>.Create(db => 1);
            var exp2 = Sql<DB>.Create(db => 3);
            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)).
                 OffsetRows(exp1).
                 FetchNextRowsOnly(exp2)
                 );

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
OFFSET @p_0 ROWS
FETCH NEXT @p_1 ROWS ONLY",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Limit1()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "OracleConnection") return;
            if (name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)));
            var limit = Sql<DB>.Create(db => Limit(1, 3));
            query = query.Concat(limit);

            query.Gen(_connection);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0, @p_1",
1, 3);
        }
        
        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Limit2()
        {
            var name = _connection.GetType().Name;
            if (name == "SqlConnection") return;
            if (name == "OracleConnection") return;
            if (name == "NpgsqlConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)));

            var exp1 = Sql<DB>.Create(db => (long)1);
            var exp2 = Sql<DB>.Create(db => (long)3);
            var limit = Sql<DB>.Create(db => Limit(exp1, exp2));
            query = query.Concat(limit);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0, @p_1",
(long)1, (long)3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Limit_Offset1()
        {
            var name = _connection.GetType().FullName;
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)));
            var limit = Sql<DB>.Create(db => Limit(1));
            var offset = Sql<DB>.Create(db => Offset(3));
            query = query.Concat(limit).Concat(offset);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0
OFFSET @p_1",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_Limit_Offset2()
        {
            var name = _connection.GetType().FullName;
            if (name == "System.Data.SqlClient.SqlConnection") return;
            if (name == "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var exp1 = Sql<DB>.Create(db => 1);
            var exp2 = Sql<DB>.Create(db => 3);
            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)));
            var limit = Sql<DB>.Create(db => Limit(exp1));
            var offset = Sql<DB>.Create(db => Offset(exp2));
            query = query.Concat(limit).Concat(offset);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
LIMIT @p_0
OFFSET @p_1",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_OffsetRows_FetchNext1()
        {
            var name = _connection.GetType().FullName;
            if (name != "System.Data.SqlClient.SqlConnection" &&
                name != "IBM.Data.DB2.DB2Connection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)));
            var offsetRows = Sql<DB>.Create(db => OffsetRows(1));
            var fetchNextRowOnly = Sql<DB>.Create(db => FetchNextRowsOnly(3));
            query = query.Concat(offsetRows).Concat(fetchNextRowOnly);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
OFFSET @p_0 ROWS
FETCH NEXT @p_1 ROWS ONLY",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_Continue_OffsetRows_FetchNext2()
        {
            var name = _connection.GetType().FullName;
            if (name != "System.Data.SqlClient.SqlConnection" &&
                name != "IBM.Data.DB2.DB2Connection") return;

            var exp1 = Sql<DB>.Create(db => (long)1);
            var exp2 = Sql<DB>.Create(db => (long)3);
            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 OrderBy(new Asc(db.tbl_remuneration.id)));
            var offsetRows = Sql<DB>.Create(db => OffsetRows(exp1));
            var fetchNextRowOnly = Sql<DB>.Create(db => FetchNextRowsOnly(exp2));
            query = query.Concat(offsetRows).Concat(fetchNextRowOnly);

            var datas = _connection.Query(query).ToList();
            Assert.IsTrue(0 < datas.Count);
            AssertEx.AreEqual(query, _connection,
@"SELECT *
FROM tbl_remuneration
ORDER BY
	tbl_remuneration.id ASC
OFFSET @p_0 ROWS
FETCH NEXT @p_1 ROWS ONLY",
(long)1, (long)3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RowNum1()
        {
            var name = _connection.GetType().FullName;
            if (name != "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 Where(Between(RowNum, 1, 3)).
                 OrderBy(new Asc(db.tbl_remuneration.id))
                 );

            var datas = _connection.Query(query).ToList();
            Assert.AreEqual(3, datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT *
FROM tbl_remuneration
WHERE ROWNUM BETWEEN :p_0 AND :p_1
ORDER BY
	tbl_remuneration.id ASC",
1, 3);
        }

        [TestMethod, DataSource(Operation, Connection, Sheet, Method)]
        public void Test_RowNum2()
        {
            var name = _connection.GetType().FullName;
            if (name != "Oracle.ManagedDataAccess.Client.OracleConnection") return;

            var exp1 = Sql<DB>.Create(db => RowNum);
            var exp2 = Sql<DB>.Create(db => (long)1);
            var exp3 = Sql<DB>.Create(db => (long)3);
            var query = Sql<DB>.Create(db =>
                 Select(Asterisk(db.tbl_remuneration)).
                 From(db.tbl_remuneration).
                 Where(Between(exp1, exp2, exp3)).
                 OrderBy(new Asc(db.tbl_remuneration.id))
                 );

            var datas = _connection.Query(query).ToList();
            Assert.AreEqual(3, datas.Count);
            AssertEx.AreEqual(query, _connection,
 @"SELECT *
FROM tbl_remuneration
WHERE ROWNUM BETWEEN :p_0 AND :p_1
ORDER BY
	tbl_remuneration.id ASC",
(long)1, (long)3);
        }
    }
}
