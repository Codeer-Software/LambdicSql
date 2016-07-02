﻿using LambdicSql;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using LambdicSql.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Test
{
    [TestClass]
    public class TestExpressionToStringcs
    {
        [TestMethod]
        public void TestDB()
        {
            var text = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(string)
                }
            }).ToSqlString(db => db.table1);
            Assert.AreEqual(text, "table1");
        }

        [TestMethod]
        public void TestColumn()
        {
            var text = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(string)
                }
            }).ToSqlString(db => db.table1.col1);
            Assert.AreEqual(text, "table1.col1");
        }

        [TestMethod]
        public void TestConstant()
        {
            var query = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(string)
                }
            });
            Assert.AreEqual(query.ToSqlString(db => 1), "1");
            Assert.AreEqual(query.ToSqlString(db => "xxx"), "'xxx'");
        }

        [TestMethod]
        public void TestNodeType()
        {
            var query = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int),
                    col2 = default(bool)
                }
            });

            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 == 1), "(table1.col1) = (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 != 1), "(table1.col1) <> (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 < 1), "(table1.col1) < (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 <= 1), "(table1.col1) <= (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 > 1), "(table1.col1) > (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 >= 1), "(table1.col1) >= (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 + 1), "(table1.col1) + (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 - 1), "(table1.col1) - (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 * 1), "(table1.col1) * (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 / 1), "(table1.col1) / (1)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 % 1), "(table1.col1) % (1)");
            Assert.AreEqual(query.ToSqlString(db => !db.table1.col2), "NOT (table1.col2)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col2 && false), "(table1.col2) AND (False)");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col2 || false), "(table1.col2) OR (False)");
        }

        [TestMethod]
        public void TestBinary()
        {
            var query = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int)
                }
            });
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 == 2 && (db.table1.col1 == 3 || db.table1.col1 == 4)),
                        "((table1.col1) = (2)) AND (((table1.col1) = (3)) OR ((table1.col1) = (4)))");
        }

        [TestMethod]
        public void TestDbFuncs()
        {
            var query = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int)
                }
            });
            Assert.AreEqual(query.ToSqlString((db, func) => func.Sum(1)), "Sum(1)");
        }

        [TestMethod]
        public void TestNormalFuncs()
        {
            var query = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int)
                }
            });

            try
            {
                query.ToSqlString((db, func) => MySum(db.table1.col1, 1));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "can't use column(table1.col1) in MySum");
            }

            try
            {
                query.ToSqlString((db, func) => MySum(MyDbToInt(db.table1), 1));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "can't use table(table1) in MySum");
            }

            try
            {
                query.ToSqlString((db, func) => MySum(query.Cast<int>(), 1));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "can't use sub query in MySum");
            }
        }

        public int IntValue => 10;
        public int MySum(int a, int b) => a + b;
        public int MyDbToInt(object o) => 0;

        [TestMethod]
        public void TestSubQuery()
        {
            var define = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int),
                    col2 = default(string)
                }
            });

            var sub = define.Select(db => new
            {
                col1 = db.table1.col1
            });

            var text = define.Select(db => new
            {
                col2 = sub.Cast<string>()
            }).Where(db=>db.table1.col1 == sub.Cast<int>()).ToQueryString();

            Debug.Print(text);


            var query = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int)
                }
            });
            Assert.AreEqual("SELECT\r\n\ttable1.col1 AS \"x\";", query.ToSqlString((db, func) => query.Select(db2 => new { x = db2.table1.col1 })));
            Assert.AreEqual("(SELECT table1.col1 AS \"x\")", query.ToSqlString((db, func) => query.Select(db2 => new { x = db2.table1.col1 }).Cast()));
        }

        [TestMethod]
        public void TestSubQueryInLambda()
        {
            var define = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int),
                    col2 = default(string)
                }
            });

            var text = define.Select(db => new
            {
                col2 = define.Select(db2 => new
                {
                    col1 = db2.table1.col1
                }).Cast<string>()
            }).ToQueryString();

            Debug.Print(text);
        }


        [TestMethod]
        public void TestWhereSubQuery()
        {
            var define = Sql.Query(() => new
            {
                table1 = new
                {
                    col1 = default(int),
                    col2 = default(string)
                }
            });

            var sub = define.Select(db => new
            {
                col1 = db.table1.col1
            });

            var text = define.Select(db => new
            {
                col2 = sub.Cast<string>()
            }).
            Where().
            Like(db => db.table1.col2, db=>sub.Cast<string>()).And().
            In(db => db.table1.col2, db=>sub.Cast<string>()).Or().Between(db => db.table1.col1, db=>sub.Cast<int>(), db=>sub.Cast<int>()).
            ToQueryString();

            Debug.Print(text);
        }
    }

    interface IFuncs : ISqlFunc{ }

    static class FuncsExtensions
    { 
        public static T Sum<T>(this IFuncs f, T t) { return default(T); }
    }

    static class ExpressionTestExtensions
    {
        public static string ToQueryString<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => TestAdaptor.ToSqlString(query as IQuery);

        internal static string ToSqlString<T, TRet>(this IQuery<T, T> query, Expression<Func<T, TRet>> exp)
            where T : class
        {
            var info = query as IQuery;
            return TestAdaptor.ToSqlString(info.Db, exp.Body);
        }

        internal static string ToSqlString<T, TRet>(this IQuery<T, T> query, Expression<Func<T, IFuncs, TRet>> exp)
            where T : class
        {
            var info = query as IQuery;
            return TestAdaptor.ToSqlString(info.Db, exp.Body);
        }
    }
}
