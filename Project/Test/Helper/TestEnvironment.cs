using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using IBM.Data.DB2;
using System.Text;

namespace Test.Helper
{
    static class TestEnvironment
    {
        internal static string SqlServerConnectionString => File.ReadAllText(FindNearFile("db.txt")).Trim();
        internal static string EFConnectionString => File.ReadAllText(FindNearFile("ef.txt")).Trim();
        internal static string PostgresConnectionStringForDBCreate => File.ReadAllText(FindNearFile("postgres.txt")).Trim();
        internal static string PostgresConnectionString => PostgresConnectionStringForDBCreate + "Database=lambdicsqltest1;";
        internal static string SQLiteTest1Path => Path.GetFullPath("../../../SQLiteTest1.db");

        static string FindNearFile(string fileName)
        {
            var path = typeof(TestEnvironment).Assembly.Location;
            while (true)
            {
                var filePath = Path.Combine(path, fileName);
                if (File.Exists(filePath))
                {
                    return filePath;
                }
                path = Path.GetDirectoryName(path);
            }
            throw new NotSupportedException();
        }

        internal static IDbConnection CreateConnection(TestContext context)
            => CreateConnection(context.DataRow[0]);

        internal static IDbConnection CreateConnection(object db)
        {
            var str = "User Id=system; Password=codeer; Data Source=" +
                "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
                "(CONNECT_DATA =(SERVER = DEDICATED)))";


            StringBuilder sb = new StringBuilder();
            sb.Append("Server=127.0.0.1:50000;");
            sb.Append("User Id=db2admin;");
            sb.Append("Password=codeer;");
            sb.Append("Database=sample");


            switch (db.ToString())
            {
                case "SQLServer": return new SqlConnection(SqlServerConnectionString);
                case "SQLite": return new SQLiteConnection("Data Source=" + SQLiteTest1Path);
                case "Postgres": return new NpgsqlConnection(PostgresConnectionString);
                case "MySQL": return new MySqlConnection("Server=localhost;Database=lambdicsqltest;Uid=root;Pwd=codeer");
                case "Oracle": return new OracleConnection(str);
                case "DB2": return new DB2Connection(sb.ToString());
            }
            throw new NotSupportedException();
        }
    }

  //  [TestClass]
    public class Initializer
    {
        [TestMethod]
        public void CreateSQLiteTable()
        {
            var path = TestEnvironment.SQLiteTest1Path;
            File.Delete(path);
            CreateTable(new SQLiteConnection("Data Source=" + path));
        }

        [TestMethod]
        public void CreatePostgresTable()
        {
            using (var connection = new NpgsqlConnection(TestEnvironment.PostgresConnectionStringForDBCreate))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DROP DATABASE lambdicsqltest1;";
                    command.ExecuteNonQuery();
                }
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE DATABASE lambdicsqltest1;";
                    command.ExecuteNonQuery();
                }
            }

            CreateTable(new NpgsqlConnection(TestEnvironment.PostgresConnectionString));
        }

        [TestMethod]
        public void CreateMySqlTable()
        {
            using (var connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=codeer"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DROP DATABASE lambdicsqltest;";
                    command.ExecuteNonQuery();
                }
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE DATABASE lambdicsqltest;";
                    command.ExecuteNonQuery();
                }
            }

            CreateTable(new MySqlConnection("Server=localhost;Database=lambdicsqltest;Uid=root;Pwd=codeer"));
        }

        [TestMethod]
        public void CreateDb2Table()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Server=127.0.0.1:50000;");
            sb.Append("User Id=db2admin;");
            sb.Append("Password=codeer;");
            sb.Append("Database=sample");

            var connString = sb.ToString();
            CreateTable(new DB2Connection(connString));
        }

        [TestMethod]
        public void CreateOracleTable()
        {
            var str = "User Id=system; Password=codeer; Data Source=" +
                "(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))"+
                "(CONNECT_DATA =(SERVER = DEDICATED)))";
            CreateTable(new OracleConnection(str));
        }

        void CreateTable(IDbConnection connection)
        { 
            using (connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = Resources.CreateTableRemuneration;
                    command.ExecuteNonQuery();
                    command.CommandText = Resources.CreateTableStaff;
                    command.ExecuteNonQuery();
                    command.CommandText = Resources.CreateTableDataChange;
                    command.ExecuteNonQuery();
                }
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        var staff = new object[][]
                        {
                            new object[] {1, "'Emma'"},
                            new object[] {2, "'Noah'"},
                            new object[] {3, "'Olivia'"},
                            new object[] {4, "'Jackson'" }
                        };
                        foreach (var e in staff)
                        {
                            command.CommandText = string.Format(Resources.InsertTableStaff, e[0], e[1]);
                            command.ExecuteNonQuery();
                        }

                        var remuneration = new object[][]
                        {
                            new object[] {1, 1, "'2016/1/1'", (decimal)3000.0000},
                            new object[] {2, 1, "'2016/2/1'", (decimal)3000.0000 },
                            new object[] {3, 2, "'2016/1/1'", (decimal)2000.0000 },
                            new object[] {4, 2, "'2016/2/1'", (decimal)2500.0000 },
                            new object[] {5, 3, "'2016/1/1'", (decimal)4000.0000 },
                            new object[] {6, 3, "'2016/2/1'", (decimal)4000.0000 },
                            new object[] {7, 4, "'2016/1/1'", (decimal)3500.0000 },
                            new object[] {8, 5, "'2016/2/1'", (decimal)3500.0000 }
                        };
                        foreach (var e in remuneration)
                        {
                            command.CommandText = string.Format(Resources.InsertTableRemuneration, e[0], e[1], e[2], e[3]);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
        }
    }
}
