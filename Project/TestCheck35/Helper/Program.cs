using System.Linq;
using System;
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
            DapperApaptExtensionsForTest.Query = QueryForTest;
            DapperApaptExtensionsForTest.Execute = ExecuteForTest;

            foreach (var type in typeof(Program).Assembly.GetTypes().Where(e => e.IsDefined(typeof(TestClassAttribute), false)))
            {
                Console.WriteLine(type.Name);

                var test = Activator.CreateInstance(type);
                var init = type.GetMethods().Where(e => e.IsDefined(typeof(TestInitializeAttribute), false)).FirstOrDefault();
                init?.Invoke(test, new object[0]);
                foreach (var m in type.GetMethods().Where(e => e.IsDefined(typeof(TestMethodAttribute), false)))
                {
#if DEBUG
                    Execute(test, m);
#else
                    else ExecuteCatch(test, m);
#endif
                }
                var cleanup = type.GetMethods().Where(e => e.IsDefined(typeof(TestCleanupAttribute), false)).FirstOrDefault();
                cleanup?.Invoke(test, new object[0]);
            }
            Console.ReadKey();
        }

        static void Execute(object test, MethodInfo method)
        {
            method.Invoke(test, new object[0]);
            Console.WriteLine("\tOK - " + method.Name);
        }

        static void ExecuteCatch(object test, MethodInfo method)
        {
            try
            {
                method.Invoke(test, new object[0]);
                Console.WriteLine("\tOK - " + method.Name);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tNG - " + method.Name);
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
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
            bool openNow = false;
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
                openNow = true;
            }
            try
            {
                using (var com = cnn.CreateCommand())
                {
                    com.CommandText = info.SqlText;
                    com.Connection = cnn;
                    foreach (var obj in info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray())
                    {
                        com.Parameters.Add(obj);
                    }
                    int count = 0;
                    using (var sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            count++;
                        }
                        return count;
                    }
                }
            }
            finally
            {
                if (openNow)
                {
                    cnn.Close();
                }
            }
        }

        static int ExecuteForTest(IDbConnection cnn, SqlInfo info)
        {
            bool openNow = false;
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
                openNow = true;
            }
            try
            {
                using (var com = cnn.CreateCommand())
                {
                    com.CommandText = info.SqlText;
                    com.Connection = cnn;
                    foreach (var obj in info.DbParams.Select(e => CreateParameter(com, e.Key, e.Value)).ToArray())
                    {
                        com.Parameters.Add(obj);
                    }
                    return com.ExecuteNonQuery();
                }
            }
            finally
            {
                if (openNow)
                {
                    cnn.Close();
                }
            }
        }

        static IDbDataParameter CreateParameter(IDbCommand com, string name, DbParam src)
        {
            var dst = com.CreateParameter();
            dst.ParameterName = name;
            dst.Value = src.Value;

            if (src.DbType != null) dst.DbType = src.DbType.Value;
            if (src.Direction != null) dst.Direction = src.Direction.Value;
            if (src.SourceColumn != null) dst.SourceColumn = src.SourceColumn;
            if (src.SourceVersion != null) dst.SourceVersion = src.SourceVersion.Value;
            if (src.Precision != null) dst.Precision = src.Precision.Value;
            if (src.Scale != null) dst.Scale = src.Scale.Value;
            if (src.Size != null) dst.Size = src.Size.Value;

            return dst;
        }
    }
}
