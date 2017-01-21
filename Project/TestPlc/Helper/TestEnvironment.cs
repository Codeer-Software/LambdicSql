using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SQLite;

namespace TestPlc
{
    static class TestEnvironment
    {
        internal static string SQLiteTest1Path => Path.GetFullPath("../../../SQLiteTest1.db");

        internal static SQLiteConnection CreateConnection(TestContext context)
            => new SQLiteConnection(SQLiteTest1Path);
    }
}
