using System;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectTime.CheckDapperCondition();
            SelectTime.CheckLambdicSqlCondition();
            Console.ReadLine();
        }
    }
}
