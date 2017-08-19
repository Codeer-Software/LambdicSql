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
        public void Test()
        {
            var sql = Db<DB>.Sql(db => Escape(1));
            Assert.AreEqual(sql.Build(typeof(SqlConnection)).Text, "ESCAPE[@p_0]");
        }

        [MethodFormatConverter(Format = "ESCAPE&left;[0]&right;")]
        static int Escape(int val) => 0;
    }
}
