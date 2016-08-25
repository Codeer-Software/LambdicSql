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

        static void NotRoadTest()
        {
            var samples = new Samples();
            try
            {
                using (var con = new SqlConnection(TestEnvironment.ConnectionString))
                {
                    con.Open();
                    samples.TestInitialize(nameof(Samples.TestStandard), con);
                    samples.TestStandard();
                }
            }
            catch (PackageIsNotInstalledException)
            {
                Console.WriteLine("OK");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("NG");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        static void Test35()
        { 
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
            }
            Console.ReadKey();
        }
    }
}
