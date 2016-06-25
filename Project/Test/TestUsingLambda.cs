using LambdicSql;
using LambdicSql.QueryInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Test
{
    [TestClass]
    public class TestUsingLambda
    {
        [TestMethod]
        public void InvalidNoNameTable()
        {
            try
            {
                Sql.Using(() =>
                new
                {
                    col1 = ""
                });
                Assert.Fail();
            }
            catch (NotSupportedException) { }
        }

        [TestMethod]
        public void InvalidLevel4()
        {
            try
            {
                Sql.Using(() =>
                new
                {
                    col1 = ""
                });
                Assert.Fail();
            }
            catch (NotSupportedException) { }
        }

        [TestMethod]
        public void TableOnly()
        {
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = "",
                    col2 = 0
                },
                table2 = new
                {
                    col3 = "",
                    col4 = 0
                }
            });
            var info = query as IQueryInfo;

            info.Db.Children.Count.Is(1);

            var schema = info.Db.Children[""];
            schema.Children.Count.Is(2);

            var table1 = schema.Children["table1"];
            table1.Children.Count.Is(2);
            var col1 = table1.Children["col1"];
            col1.FullName.Is(new[] { "table1", "col1" });
            col1.FullNameText.Is("table1.col1");
            col1.Type.Is(typeof(string));
            var col2 = table1.Children["col2"];
            col2.FullNameText.Is("table1.col2");

            var table2 = schema.Children["table2"];
            table2.Children.Count.Is(2);
            table2.Children["col4"].Type.Is(typeof(int));
        }

        //schema

        //create
    }
}
