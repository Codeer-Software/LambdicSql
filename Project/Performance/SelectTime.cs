using Dapper;
using LambdicSql;
using LambdicSql.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    [TestClass]
    public class SelectTime
    {
        [TestMethod]
        public void CheckLambdicSql()
        {
            var executor = Sql.Query<DB>().Select(db => new TableValues()
            {
                IntVal = db.TableValues.IntVal,
                FloatVal = db.TableValues.FloatVal,
                DoubleVal = db.TableValues.DoubleVal,
                DecimalVal = db.TableValues.DecimalVal,
                StringVal = db.TableValues.StringVal

            }).From(db => db.TableValues).ToExecutor(new SqlServerAdapter(TestEnvironment.ConnectionString));

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var datas = executor.Read();
            watch.Stop();
        }

        [TestMethod]
        public void CheckDapper()
        {
            using (var connection = new SqlConnection(TestEnvironment.ConnectionString))
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                var datas = connection.Query<TableValues>("select IntVal, FloatVal, DoubleVal, DecimalVal, StringVal from TableValues;").ToList();
                watch.Stop();
            }
        }
    }
}
