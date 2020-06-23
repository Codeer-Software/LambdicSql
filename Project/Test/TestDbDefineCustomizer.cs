using LambdicSql;
using LambdicSql.ConverterServices.SymbolConverters;
using LambdicSql.feat.Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Test.Helper;
using static Test.Helper.DBProviderInfo;
using static Test.Symbol;

namespace Test
{
    [TestClass]
    public class TestDbDefineCustomizer
    {
        public class Staff
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class DB1
        {
            public Staff TblStaff { get; set; }
        }
        public class DB2
        {
            public Staff TblStaff { get; set; }
        }

        [TestMethod]
        public void Test1()
        {
            Db<DB1>.DefinitionCustomizer = p => null;

            var sql = Db<DB1>.Sql(db =>
                Select(new
                {
                    name = db.TblStaff.Name,
                }).
                From(db.TblStaff));

            var txt = sql.Build(typeof(NpgsqlConnection)).Text;
            var expected = @"SELECT
	TblStaff.Name AS name
FROM TblStaff";
            Assert.AreEqual(expected, txt);
        }

        [TestMethod]
        public void Test2()
        {
            Db<DB2>.DefinitionCustomizer = p =>
            {
                var input = p.Name;
                if (string.IsNullOrEmpty(input)) { return input; }
                var startUnderscores = Regex.Match(input, @"^_+");
                return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
            };

            var sql = Db<DB2>.Sql(db =>
                Select(new
                {
                    name = db.TblStaff.Name,
                }).
                From(db.TblStaff));

            var txt = sql.Build(typeof(NpgsqlConnection)).Text;
            var expected = @"SELECT
	tbl_staff.Name AS name
FROM tbl_staff";
            Assert.AreEqual(expected, txt);
        }

    }
}
