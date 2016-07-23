using TestCore;
using System.Linq;
using System;
using System.Data.SqlClient;

namespace TestCheck35
{
    class Program
    {
        static void Main(string[] args)
        {
            Test35();
        }

        static void Errors()
        {
            var samples = new Samples();
            using (var con = new SqlConnection(TestEnvironment.ConnectionString))
            {
                con.Open();
                samples.TestInitialize(nameof(samples.Like), con);
                samples.Like();

                samples.TestInitialize(nameof(samples.In), con);
                samples.In();

                samples.TestInitialize(nameof(samples.Between), con);
                samples.Between();

                samples.TestInitialize(nameof(samples.FromSubQuery), con);
                samples.FromSubQuery();
            }
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
                        m.Invoke(samples, new object[0]);
                        Console.WriteLine("OK - " + m.Name);
                    }
                }
                catch(Exception e)
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
