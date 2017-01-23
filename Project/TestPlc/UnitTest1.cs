﻿using LambdicSql;
using static LambdicSql.Symbol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using LambdicSql.feat.SqLiteNetPcl;

namespace TestPlc
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var x = new { a = 1, b = 2 };


            int id = 2;
            var sql = Db<DB>.Sql(db =>

                Select(Asterisk(db.tbl_staff)).
                From(db.tbl_staff).
                Where(db.tbl_staff.id == id || db.tbl_staff.id == 4 || db.tbl_staff.id < id)

            );

            using (var con = TestEnvironment.CreateConnection(TestContext))
            {
                SqLiteNetPclAdapter.Log = e => Debug.Print(e);
                var list = con.Query(sql);
            }
        }

        [TestMethod]
        public void Test_Delete_All()
        {
            var deleteAll = Db<DB>.Sql(db =>
                Delete().
                From(db.tbl_data));

            var insert = Db<DB>.Sql(db =>
                   InsertInto(db.tbl_data, db.tbl_data.id, db.tbl_data.val2).Values(1, "val2"));


            using (var con = TestEnvironment.CreateConnection(TestContext))
            {
                SqLiteNetPclAdapter.Log = e => Debug.Print(e);
                var countDel = con.Execute(deleteAll);
                var countInsert = con.Execute(insert);
            }
        }

    }

    class GetterCore<T0> { }
}
