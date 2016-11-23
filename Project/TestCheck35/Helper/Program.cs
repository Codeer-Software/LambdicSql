using System.Linq;
using System;
using System.Data.SqlClient;
using LambdicSql.feat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdicSql.feat.Dapper;
using System.Reflection;
using LambdicSql;
using System.Data;

namespace TestCheck35
{
    static class Program
    {
        static void Main(string[] args)
        {
            Test35();
        }
        
        static void Test35()
        {
            DapperApaptExtensionsForTest.Query = QueryForTest;
            DapperApaptExtensionsForTest.Execute = ExecuteForTest;

            foreach (var type in typeof(Program).Assembly.GetTypes().Where(e=>e.IsDefined(typeof(TestClassAttribute), false)))
            {
                var test = Activator.CreateInstance(type);
                var init = type.GetMethods().Where(e => e.IsDefined(typeof(TestInitializeAttribute), false)).FirstOrDefault();
                init?.Invoke(test, new object[0]);
                foreach (var m in type.GetMethods().Where(e => e.IsDefined(typeof(TestMethodAttribute), false)))
                {
                    try
                    {
                        m.Invoke(test, new object[0]);
                        Console.WriteLine("OK - " + m.Name);
                    }
                    catch(Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("NG - " + m.Name);
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                }
                var cleanup = type.GetMethods().Where(e => e.IsDefined(typeof(TestCleanupAttribute), false)).FirstOrDefault();
                cleanup?.Invoke(test, new object[0]);
            }
            Console.ReadKey();
        }

        static string GetMessage(this Exception e)
        {
            while (true)
            {
                if (e.InnerException == null) return e.Message;
                e = e.InnerException;
            }
        }

        static int QueryForTest(IDbConnection cnn, SqlInfo info)
        {
            //TODO
            return 1;
        }

        static int ExecuteForTest(IDbConnection cnn, SqlInfo info)
        {
            //TODO
            return 1;
        }
    }
}
