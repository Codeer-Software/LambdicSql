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
