using LambdicSql;
using LambdicSql.Inside;
using LambdicSql.QueryInfo;
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
            var text = Sql.Using(() => new
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
            var text = Sql.Using(() => new
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
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = default(string)
                }
            });
            Assert.AreEqual(query.ToSqlString(db => 1), "'1'");
            Assert.AreEqual(query.ToSqlString(db => "xxx"), "'xxx'");
        }

        [TestMethod]
        public void TestNodeType()
        {
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = default(int),
                    col2 = default(bool)
                }
            });
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 == 1), "(table1.col1) = ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 != 1), "(table1.col1) != ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 < 1), "(table1.col1) < ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 <= 1), "(table1.col1) <= ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 > 1), "(table1.col1) > ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 >= 1), "(table1.col1) >= ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 + 1), "(table1.col1) + ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 - 1), "(table1.col1) - ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 * 1), "(table1.col1) * ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 / 1), "(table1.col1) / ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 % 1), "(table1.col1) % ('1')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col2 && false), "(table1.col2) AND ('False')");
            Assert.AreEqual(query.ToSqlString(db => db.table1.col2 || false), "(table1.col2) OR ('False')");
        }

        [TestMethod]
        public void TestBinary()
        {
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = default(int)
                }
            });
            Assert.AreEqual(query.ToSqlString(db => db.table1.col1 == 2 && (db.table1.col1 == 3 || db.table1.col1 == 4)),
                        "((table1.col1) = ('2')) AND (((table1.col1) = ('3')) OR ((table1.col1) = ('4')))");
        }

        [TestMethod]
        public void TestMethod()
        {
            var query = Sql.Using(() => new
            {
                table1 = new
                {
                    col1 = default(int)
                }
            });
            Assert.AreEqual(query.ToSqlString((db, func) => func.Sum(1)), "Sum('1')");
        }

        [TestMethod]
        public void TestSubQuery()
        {
            var define = Sql.Using(() => new
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
                col2 = sub.ToSubQuery<string>()
            }).Where(db=>db.table1.col1 == sub.ToSubQuery<int>()).ToQueryString();

            Debug.Print(text);
        }

        [TestMethod]
        public void TestSubQueryInLambda()
        {
            var define = Sql.Using(() => new
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
                }).ToSubQuery<string>()
            }).ToQueryString();

            Debug.Print(text);
        }


        [TestMethod]
        public void TestWhereSubQuery()
        {
            var define = Sql.Using(() => new
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
                col2 = sub.ToSubQuery<string>()
            }).
            Where().
            Like(db => db.table1.col2, db=>sub.ToSubQuery<string>()).And().
            In(db => db.table1.col2, db=>sub.ToSubQuery<string>()).Or().Between(db => db.table1.col1, db=>sub.ToSubQuery<int>(), db=>sub.ToSubQuery<int>()).
            ToQueryString();

            Debug.Print(text);
        }
    }

    interface IFuncs { }

    static class FuncsExtensions
    { 
        public static T Sum<T>(this IFuncs f, T t) { return default(T); }
    }

    static class ExpressionTestExtensions
    {
        internal static string ToSqlString<T, TRet>(this IQuery<T, T> query, Expression<Func<T, TRet>> exp)
            where T : class
        {
            var info = query as IQueryInfo;
            return TestAdaptor.ToSqlString(info.Db, exp.Body);
        }

        internal static string ToSqlString<T, TRet>(this IQuery<T, T> query, Expression<Func<T, IFuncs, TRet>> exp)
            where T : class
        {
            var info = query as IQueryInfo;
            return TestAdaptor.ToSqlString(info.Db, exp.Body);
        }
    }
}
