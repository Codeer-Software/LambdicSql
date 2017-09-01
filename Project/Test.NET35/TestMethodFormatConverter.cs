using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql;
using LambdicSql.feat.Dapper;
using static Test.Symbol;
using static Test.Helper.DBProviderInfo;
using Test.Helper;
using LambdicSql.ConverterServices.SymbolConverters;

namespace Test
{

    [TestClass]
    public class TestMethodFormatConverter
    {
        [TestMethod]
        public void TestEscape()
        {
            var sql = Db<DB>.Sql(db => Escape(1));
            Assert.AreEqual(sql.Build(typeof(SqlConnection)).Text, "ESCAPE[@p_0]");
        }

        class A { }

        [TestMethod]
        public void TestVariableName()
        {
            Staff starff = null;
            var sql = Db<DB>.Sql(db => Variable(starff));
            Assert.AreEqual(sql.Build(typeof(SqlConnection)).Text, "starff");
        }

        [MethodFormatConverter(Format = "ESCAPE&left;[0]&right;")]
        static int Escape(int val) => 0;

        [MethodFormatConverter(Format = "[*0]")]
        static int Variable(object variable) => 0;
    }
}
