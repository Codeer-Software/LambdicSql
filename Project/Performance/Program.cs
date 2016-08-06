using LambdicSql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        static void Profile()
        {
            SelectTime.IsProfile = true;
            Console.ReadLine();
            SelectTime.CheckLambdicSqlCondition();
        }

        static void CompareOne()
        {
            SelectTime.IsProfile = false;
            Console.ReadLine();
            Console.WriteLine("Dapper");
            SelectTime.CheckDapperCondition();
            Console.WriteLine("Lambdic");
            SelectTime.CheckLambdicSqlCondition();
            Console.WriteLine("Finish");
            Console.ReadLine();
        }

        public class SelectedData
        {
            public string name { get; set; }
            public DateTime payment_date { get; set; }
            public decimal money { get; set; }
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
            public Staff tbl_staff { get; set; }
            public Remuneration tbl_remuneration { get; set; }
        }
        static void Test()
        {

            Console.ReadKey();
            Console.WriteLine("Start");
            SqlInfo info = null;
            var times = new List<double>();
            var watch = new Stopwatch();
            for (int i = 0; i < 10000; i++)
            {
                watch.Start();
                var query = Sql<Data>.Create((db, x) =>
                    x.
                    Select(new SelectedData()
                    {
                        name = db.tbl_staff.name,
                        payment_date = db.tbl_remuneration.payment_date,
                        money = db.tbl_remuneration.money,
                    }).
                    From(db.tbl_remuneration).
                        Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                    Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

                info = query.ToSqlInfo(typeof(SqlConnection));

                watch.Stop();
                times.Add(watch.Elapsed.TotalMilliseconds);
                watch.Reset();
            }

            times = times.Skip(1).ToList();
            times.Select(e => e.ToString()).ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine(times.Average().ToString());

            Console.ReadKey();

            //  var con = new SqlConnection(TestEnvironment.SqlServerConnectionString);
            // var data = con.Query(query);

            //   var ps = new DynamicParameters();
            //  info.Parameters.ToList().ForEach(e => ps.Add(e.Key, e.Value));
            //   var datas = new SqlConnection(TestEnvironment.SqlServerConnectionString).Query<SelectedData>(info.SqlText, info.Parameters).ToList();

        }
    }
}
