using LambdicSql;
using LambdicSql.SqlBase;
using System;
using System.Data;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    class Assert
    {
        internal static void AreEqual(object lhs, object rhs)
        {
            if (lhs == null && rhs == null) return;
            if (lhs == null || rhs == null) throw new InvalidProgramException();
            if (!lhs.Equals(rhs)) throw new InvalidProgramException();
        }

        internal static void AreNotEqual(object lhs, object rhs)
        {
            if (lhs == null && rhs == null) throw new InvalidProgramException();
            if (lhs == null || rhs == null) return;
            if (lhs.Equals(rhs)) throw new InvalidProgramException();
        }

        internal static void IsTrue(bool condition)
        {
            if (!condition) throw new InvalidProgramException();
        }

        internal static void IsFalse(bool condition)
        {
            if (condition) throw new InvalidProgramException();
        }
    }

    public enum DataAccessMethod
    {
        Sequential
    }
    public class TestContext { }
    public class TestClassAttribute : Attribute { }
    public class TestInitializeAttribute : Attribute { }
    public class TestCleanupAttribute : Attribute { }
    public class TestMethodAttribute : Attribute { }
    public class DataSourceAttribute : Attribute
    {
        internal DataSourceAttribute(string providerInvariantName, string connectionString, string tableName, DataAccessMethod dataAccessMethod) { }
    }
}

