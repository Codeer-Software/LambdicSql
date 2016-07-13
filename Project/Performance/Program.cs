using System;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            CompareOne();
        }

        static void Profile()
        {
            SelectTime.IsProfile = true;
            Console.ReadLine();
            SelectTime.CheckLambdicSqlCondition();
        }

        static void CompareOne()
        {
            SelectTime.IsProfile = false;
            Console.ReadLine();
            Console.WriteLine("Dapper");
            SelectTime.CheckDapperCondition();
            Console.WriteLine("Lambdic");
            SelectTime.CheckLambdicSqlCondition();
            Console.WriteLine("Finish");
            Console.ReadLine();
        }
    }
}
