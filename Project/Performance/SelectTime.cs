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
            var executor = Sql.Query<DB>().SelectFrom(db => db.TableValues).ToExecutor(new SqlServerAdapter(TestEnvironment.ConnectionString));

            var times = new List<long>();
            for (int i = 0; i < 10; i++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var datas = executor.Read().ToList();
                watch.Stop();
                times.Add(watch.ElapsedMilliseconds);
            }
            MessageBox.Show(string.Join(Environment.NewLine, times.Select(e=>e.ToString())) + Environment.NewLine + times.Average().ToString());
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
                MessageBox.Show(string.Join(Environment.NewLine, times.Select(e => e.ToString())) + Environment.NewLine + times.Average().ToString());
            }
        }
    }
}
