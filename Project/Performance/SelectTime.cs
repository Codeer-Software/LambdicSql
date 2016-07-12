using Dapper;
using LambdicSql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace Performance
{
    class TableValues
    {
        public int IntVal { get; set; }
        public float FloatVal { get; set; }
        public double DoubleVal { get; set; }
        public decimal DecimalVal { get; set; }
        public string StringVal { get; set; }
    }
    class DB
    {
        public TableValues TableValues { get; set; }
    }
    
    static class SelectTime
    {
        static int _count = 100;

        internal static void CheckLambdicSql()
        {
            Console.WriteLine(nameof(CheckLambdicSql));
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < _count; i++)
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    var datas = Sql.Query<DB>().SelectFrom(db => db.TableValues).ToExecutor(connection).Read().ToList();
                    watch.Stop();
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
            }
            ShowTime(times);
        }

        internal static void CheckDapper()
        {
            Console.WriteLine(nameof(CheckDapper));
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < _count; i++)
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    var datas = connection.Query<TableValues>("select IntVal, FloatVal, DoubleVal, DecimalVal, StringVal from TableValues;").ToList();
                    watch.Stop();
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
            }
            ShowTime(times);
        }

        internal static void CheckLambdicSqlCondition()
        {
            Console.WriteLine(nameof(CheckLambdicSqlCondition));
            int x = 1;
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < _count; i++)
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    var datas = Sql.Query<DB>().SelectFrom(db => db.TableValues).
                       Where(db => db.TableValues.IntVal == x).ToExecutor(connection).Read().ToList();
                    watch.Stop();
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
            }
            ShowTime(times);
        }

        internal static void CheckDapperCondition()
        {
            Console.WriteLine(nameof(CheckDapperCondition));
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < _count; i++)
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    var datas = connection.Query<TableValues>("select IntVal, FloatVal, DoubleVal, DecimalVal, StringVal from TableValues  where IntVal = @Id;", new { Id = 1 }).ToList();
                    watch.Stop();
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
            }
            ShowTime(times);
        }

        static void ShowTime(IEnumerable<double> times)
        {
            times = times.Skip(1);
            times.Select(e => e.ToString()).ToList().ForEach(e => Console.WriteLine(e));
            Console.WriteLine(times.Average().ToString());
        }
    }
}
