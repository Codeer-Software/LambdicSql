using LambdicSql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class TestSqlRead
    {
        [TestMethod]
        public void Test()
        {
            var query = Sql.Using(() => new
            {
                tbl_staff = new
                {
                    id = 0,
                    name = ""
                },
                tbl_remuneration = new
                {
                    id = 0,
                    staff_id = 0,
                    payment_date = default(DateTime),
                    money = default(Decimal)
                }
            }).
            Select(db => new
            {
                name = db.tbl_staff.name,
                payment_date = db.tbl_remuneration.payment_date,
                mony = db.tbl_remuneration.money,
            }).
            From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id).
            Where(db => 2000 < db.tbl_remuneration.money).
                OrderBy().ASC(db => db.tbl_staff.name);

            var text = query.ToQueryString(typeof(SqlConnection));
            Debug.Print(text);

            using (var con = new SqlConnection(TestEnvironment.ConnectionString))
            {
                foreach (var e in query.Connect(con).Read())
                {
                    Debug.Print("{0}", e.name, e.payment_date, e.mony);
                }
            }
        }
    }
}
