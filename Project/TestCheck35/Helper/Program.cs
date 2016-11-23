using TestCore;
using System.Linq;
using System;
using System.Data.SqlClient;
using LambdicSql.feat;

namespace TestCheck35
{
    class Program
    {
        static void Main(string[] args)
        {
            Test35();
        }
        
        static void Test35()
        { 
            /*
            var samples = new Samples();
            foreach (var m in samples.GetType().GetMethods().
                Where(e => e.DeclaringType == samples.GetType()).
                Where(e => e.GetParameters().Length == 0))
            {
                try
                {
                    using (var con = new SqlConnection(TestEnvironment.ConnectionString))
                    {
                        con.Open();
                        samples.TestInitialize(m.Name, con);
                        try
                        {
                            m.Invoke(samples, new object[0]);
                        }
                        catch (Exception e)
                        {
                            if (!(e.InnerException is PackageIsNotInstalledException))
                            {
                                throw e.InnerException;
                            }
                        }
                        Console.WriteLine("OK - " + m.Name);
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("NG - " + m.Name);
                    Console.ResetColor();
                }
            }*/
            Console.ReadKey();
        }
    }
}
