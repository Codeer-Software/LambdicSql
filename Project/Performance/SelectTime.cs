using Dapper;
using LambdicSql;
using LambdicSql.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

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

    [TestClass]
    public class SelectTime
    {
        [TestMethod]
        public void CheckLambdicSql()
        {
            var adaptor = new SqlServerAdapter(TestEnvironment.ConnectionString);

            var times = new List<long>();
            for (int i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var datas = Sql.Query<DB>().SelectFrom(db => db.TableValues).ToExecutor(adaptor).Read().ToList();
                watch.Stop();
                times.Add(watch.ElapsedMilliseconds);
            }
            ShowTime(times);
        }

        [TestMethod]
        public void CheckDapper()
        {
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                var times = new List<long>();
                for (int i = 0; i < 10; i++)
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    var datas = connection.Query<TableValues>("select IntVal, FloatVal, DoubleVal, DecimalVal, StringVal from TableValues;").ToList();
                    watch.Stop();
                    times.Add(watch.ElapsedMilliseconds);
                }
                ShowTime(times);
            }
        }

        [TestMethod]
        public void CheckLambdicSqlCondition()
        {
            var adaptor = new SqlServerAdapter(TestEnvironment.ConnectionString);

            var times = new List<long>();
            for (int i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var datas = Sql.Query<DB>().SelectFrom(db => db.TableValues).Where(db=>db.TableValues.IntVal != -1 && db.TableValues.DoubleVal != -1).ToExecutor(adaptor).Read().ToList();
                watch.Stop();
                times.Add(watch.ElapsedMilliseconds);
            }
            ShowTime(times);
        }

        [TestMethod]
        public void CheckDapperCondition()
        {
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                var times = new List<long>();
                for (int i = 0; i < 10; i++)
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    var datas = connection.Query<TableValues>("select IntVal, FloatVal, DoubleVal, DecimalVal, StringVal from TableValues where IntVal <> -1 and DoubleVal <> -1;").ToList();
                    watch.Stop();
                    times.Add(watch.ElapsedMilliseconds);
                }
                ShowTime(times);
            }
        }

        static void ShowTime(List<long> times)
        {
            MessageBox.Show(string.Join(Environment.NewLine, times.Select(e => e.ToString())) +
                Environment.NewLine + times.Average().ToString() +
                Environment.NewLine + times.Skip(1).Average().ToString());
        }
    }
}

//不具合を直して→全ラムダでSelectFromを使ったらNG