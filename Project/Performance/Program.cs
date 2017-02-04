using LambdicSql;
using LambdicSql.ConverterServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.Symbol;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCreateAndBuild();
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

        static void TestFormatAttribute()
        {
            Console.ReadKey();
            Console.WriteLine("Start");
            BuildedSql info = null;
            var times = new List<double>();
            var watch = new Stopwatch();

            int a = 0;
            int b = 1;

            for (int i = 0; i < 10000; i++)
            {
                watch.Start();

                var query = Db<Data>.Sql(db =>
                    Values(0, a, 2, b)
                    );

                info = query.Build(typeof(SqlConnection));

                watch.Stop();
                if (i != 0)
                {
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
                watch.Reset();
            }

            times = times.Skip(1).ToList();
            times.Select(e => e.ToString()).ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine(times.Average().ToString());
            Console.ReadKey();
        }

        static void TestFormatAttributeForProfiler()
        {
            int a = 0;
            int b = 1;

            for (int i = 0; i < 10000; i++)
            {
                var query = Db<Data>.Sql(db =>
                    Values(0, a, 2, b)
                    );

                query.Build(typeof(SqlConnection));
            }
        }

        static void TestCreateAndBuild()
        {
            Console.ReadKey();
            Console.WriteLine("Start");
            BuildedSql info = null;
            var times = new List<double>();
            var watch = new Stopwatch();

            for (int i = 0; i < 10000; i++)
            {
                watch.Start();

                var query = Db<Data>.Sql(db =>
                    Select(new SelectedData()
                    {
                        name = db.tbl_staff.name,
                        payment_date = db.tbl_remuneration.payment_date,
                        money = db.tbl_remuneration.money,
                    }).
                    From(db.tbl_remuneration).
                        Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                    Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

                info = query.Build(typeof(SqlConnection));

                watch.Stop();
                if (i != 0)
                {
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
                watch.Reset();
            }

            times = times.Skip(1).ToList();
            times.Select(e => e.ToString()).ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine(times.Average().ToString());
            Console.ReadKey();
        }

        static void TestCreateAndBuildForProfiler()
        {
            for (int i = 0; i < 10000; i++)
            {
                var query = Db<Data>.Sql(db =>

                    Select(new SelectedData()
                    {
                        name = db.tbl_staff.name,
                        payment_date = db.tbl_remuneration.payment_date,
                        money = db.tbl_remuneration.money,
                    }).
                    From(db.tbl_remuneration).
                        Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                    Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

                query.Build(typeof(SqlConnection));
            }
        }

        static void TestBuild()
        {
            Console.ReadKey();
            Console.WriteLine("Start");
            BuildedSql info = null;
            var times = new List<double>();
            var watch = new Stopwatch();

            var query = Db<Data>.Sql(db =>

                Select(new SelectedData()
                {
                    name = db.tbl_staff.name,
                    payment_date = db.tbl_remuneration.payment_date,
                    money = db.tbl_remuneration.money,
                }).
                From(db.tbl_remuneration).
                    Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

            for (int i = 0; i < 10000; i++)
            {
                watch.Start();

                info = query.Build(typeof(SqlConnection));

                watch.Stop();
                if (i != 0)
                {
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
                watch.Reset();
            }

            times = times.Skip(1).ToList();
            times.Select(e => e.ToString()).ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine(times.Average().ToString());
            Console.ReadKey();
        }

        static void TestGenerateExpression()
        {
            Console.ReadKey();
            Console.WriteLine("Start");
            var times = new List<double>();
            var watch = new Stopwatch();

            for (int i = 0; i < 10000; i++)
            {
                watch.Start();

                DbEmpty<Data>.Sql(db =>
                    Select(new SelectedData()
                    {
                        name = db.tbl_staff.name,
                        payment_date = db.tbl_remuneration.payment_date,
                        money = db.tbl_remuneration.money,
                    }).
                    From(db.tbl_remuneration).
                        Join(db.tbl_staff, db.tbl_remuneration.staff_id == db.tbl_staff.id).
                    Where(3000 < db.tbl_remuneration.money && db.tbl_remuneration.money < 4000));

                watch.Stop();
                if (i != 0)
                {
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
                watch.Reset();
            }

            times = times.Skip(1).ToList();
            times.Select(e => e.ToString()).ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine(times.Average().ToString());
            Console.ReadKey();
        }

        public class DbEmpty<T> where T : class
        {
            public static Expression e;
            public static void Sql<TSelected>(Expression<Func<T, Clause<TSelected>>> expression)
            {
                e = expression;
            }
        }
    }
}

