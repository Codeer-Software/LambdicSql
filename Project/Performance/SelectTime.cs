using Dapper;
using LambdicSql;
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
        //1万件取得
        [TestMethod]
        public void CheckLambdicSql()
        {
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < 10; i++)
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

        [TestMethod]
        public void CheckDapper()
        {
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < 10; i++)
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

        //以下はwhereで1件に絞ったもの
        [TestMethod]
        public void CheckLambdicSqlCondition()
        {
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < 10 * 3; i++)
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    var datas = Sql.Query<DB>().SelectFrom(db => db.TableValues).Where((db, p) => db.TableValues.IntVal == p._0, new Parameters() { _0 = 1 }).ToExecutor(connection).Read().ToList();
                    watch.Stop();
                    times.Add(watch.Elapsed.TotalMilliseconds);
                }
            }
            ShowTime(times);
        }

        [TestMethod]
        public void CheckDapperCondition()
        {
            var times = new List<double>();
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                connection.Open();
                for (int i = 0; i < 10 * 3; i++)
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

        static void ShowTime(List<double> times)
        {
            MessageBox.Show(string.Join(Environment.NewLine, times.Select(e => e.ToString())) +
                Environment.NewLine + times.Average().ToString());
        }
    }
}
