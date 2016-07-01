using LambdicSql;
using LambdicSql.QueryBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class TestUsingLambda
    {
        [TestMethod]
        public void SupportedType()
        {
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = default(string),
                    col2 = default(bool),
                    col3 = default(byte),
                    col4 = default(short),
                    col5 = default(int),
                    col6 = default(long),
                    col7 = default(float),
                    col8 = default(double),
                    col9 = default(decimal),
                    col10 = default(DateTime)
                }
            });

            var data = new TestResult();
            data["table1@col1"] = "abc";
            data["table1@col2"] = true;
            data["table1@col3"] = (byte)3;
            data["table1@col4"] = (short)4;
            data["table1@col5"] = (int)5;
            data["table1@col6"] = (long)6;
            data["table1@col7"] = (float)7;
            data["table1@col8"] = (double)8;
            data["table1@col9"] = (decimal)9;
            data["table1@col10"] = new DateTime(1999, 12, 31);

            var obj = data.Create(query);

            Assert.AreEqual(obj.table1.col1, "abc");
            Assert.AreEqual(obj.table1.col2, true);
            Assert.AreEqual(obj.table1.col3, (byte)3);
            Assert.AreEqual(obj.table1.col4, (short)4);
            Assert.AreEqual(obj.table1.col5, 5);
            Assert.AreEqual(obj.table1.col6, 6);
            Assert.AreEqual(obj.table1.col7, 7);
            Assert.AreEqual(obj.table1.col8, 8);
            Assert.AreEqual(obj.table1.col9, 9);
            Assert.AreEqual(obj.table1.col10, new DateTime(1999, 12, 31));
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
            var info = query as IQuery;

            Assert.AreEqual(info.Db.GetLambdaNameAndColumn().Count, 4);
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["table1.col1"].LambdaFullName, "table1.col1");
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["table1.col2"].LambdaFullName, "table1.col2");
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["table2.col3"].LambdaFullName, "table2.col3");
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["table2.col4"].LambdaFullName, "table2.col4");
        }

        [TestMethod]
        public void Schema()
        {
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = "",
                    col2 = 0
                },
                dbo = new
                {
                    table2 = new
                    {
                        col3 = "",
                        col4 = 0
                    }
                }
            });
            var info = query as IQuery;

            Assert.AreEqual(info.Db.GetLambdaNameAndColumn().Count, 4);
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["table1.col1"].LambdaFullName, "table1.col1");
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["table1.col2"].LambdaFullName, "table1.col2");
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["dbo.table2.col3"].LambdaFullName, "dbo.table2.col3");
            Assert.AreEqual(info.Db.GetLambdaNameAndColumn()["dbo.table2.col4"].LambdaFullName, "dbo.table2.col4");
        }
        
        [TestMethod]
        public void CreateTableOnly()
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

            var data = new TestResult();
            data["table1@col1"] = "abc";
            data["table1@col2"] = 100;
            data["table2@col3"] = "def";
            data["table2@col4"] = 200;
            var obj = data.Create(query);
            Assert.AreEqual(obj.table1.col1, "abc");
            Assert.AreEqual(obj.table1.col2, 100);
            Assert.AreEqual(obj.table2.col3, "def");
            Assert.AreEqual(obj.table2.col4, 200);
        }

        [TestMethod]
        public void CreateSchema()
        {
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = "",
                    col2 = 0
                },
                dbo = new
                {
                    table2 = new
                    {
                        col3 = "",
                        col4 = 0
                    }
                }
            });

            var data = new TestResult();
            data["table1@col1"] = "abc";
            data["table1@col2"] = 100;
            data["dbo@table2@col3"] = "def";
            data["dbo@table2@col4"] = 200;
            var obj = data.Create(query);
            Assert.AreEqual(obj.table1.col1, "abc");
            Assert.AreEqual(obj.table1.col2, 100);
            Assert.AreEqual(obj.dbo.table2.col3, "def");
            Assert.AreEqual(obj.dbo.table2.col4, 200);
        }
    }
}
