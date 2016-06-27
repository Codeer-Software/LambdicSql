using LambdicSql;
using System;
using System.Diagnostics;
using System.Linq;

namespace TestCheck35
{
    class Program
    {
        static void Main(string[] args)
        {
            var max = 4000;

            //log for debug.
            Sql.Log = l => Debug.Print(l);

            //make sql.
            var q1 = Sql.Using(() => new
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
                    money = default(decimal)
                }
            });
            var q2 = q1.Select(db => new
            {
                name = db.tbl_staff.name,
                payment_date = db.tbl_remuneration.payment_date,
                money = db.tbl_remuneration.money,
            });
            var q3 = q2.From(db => db.tbl_remuneration).
                Join(db => db.tbl_staff, db => db.tbl_remuneration.staff_id == db.tbl_staff.id);

            var q4 = q3.Where(db => 3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < max).
                OrderBy().ASC(db => db.tbl_staff.name);

            //execute.
            var datas = q4.ToExecutor("Data Source=DESKTOP-IBN02LQ;Initial Catalog=LambdicSqlTest;User ID=sa;Password=codeer;").Read();

            foreach (var e in datas)
            {
                Debug.Print("{0}, {1}, {2}", e.name, e.payment_date, e.money);
            }

        }
    }
}
