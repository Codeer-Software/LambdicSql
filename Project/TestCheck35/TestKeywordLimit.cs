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
using System.Diagnostics;

namespace TestCheck35
{
    public class TestKeywordLimit
    {
        public IDbConnection _connection;

        public void TestInitialize(string testName, IDbConnection connection)
        {
            _connection = connection;
        }

        public void Test_Limit_Offset()
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
OFFSET @p_1");
        }

        public void Test_OffsetRows_FetchNext()
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
FETCH NEXT @p_1 ROWS ONLY");
        }
        
        public void Test_Continue_Limit_Offset()
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
OFFSET @p_1");
        }

        public void Test_Continue_OffsetRows_FetchNext()
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
FETCH NEXT @p_1 ROWS ONLY");
        }

        public void Test_RowNum()
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
            query.Gen(_connection);
        }
    }
}
