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
        static void CheckTimeCore(Action<SqlConnection> action)
        {
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < 1000; i++)
                {
               //     var watch = new Stopwatch();
               //     watch.Start();
                    action(connection);
                //    watch.Stop();
                //    times.Add(watch.Elapsed.TotalMilliseconds);
                }
            }
       //     times = times.Skip(1).ToList();
       //     times.Select(e => e.ToString()).ToList().ForEach(e => Console.WriteLine(e));
       //     Console.WriteLine(times.Average().ToString());
        }

        internal static void CheckLambdicSql()
        {
            CheckTimeCore(connection =>
            {
                var datas = Sql.Query<DB>().SelectFrom(db => db.TableValues).ToExecutor(connection).Read().ToList();
            });
        }

        internal static void CheckDapper()
        {
            CheckTimeCore(connection =>
            {
                var datas = connection.Query<TableValues>("select IntVal, FloatVal, DoubleVal, DecimalVal, StringVal from TableValues;").ToList();
            });
        }

        internal static void CheckLambdicSqlCondition()
        {
            CheckTimeCore(connection =>
            {
                int x = 1;
                var datas = Sql.Query<DB>().SelectFrom(db => db.TableValues).
                       Where(db => db.TableValues.IntVal == x).ToExecutor(connection).Read().ToList();
            });
        }

        internal static void CheckDapperCondition()
        {
            CheckTimeCore(connection =>
            {
                var datas = connection.Query<TableValues>("select IntVal, FloatVal, DoubleVal, DecimalVal, StringVal from TableValues  where IntVal = @Id;", new { Id = 1 }).ToList();
            });
        }
    }
}
