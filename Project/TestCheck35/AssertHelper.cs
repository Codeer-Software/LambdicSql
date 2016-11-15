using System;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    public class Assert
    {
        public static void AreEqual(object lhs, object rhs)
        {
            if (lhs == null && rhs == null) return;
            if (lhs == null || rhs == null) throw new InvalidProgramException();
            if (!lhs.Equals(rhs)) throw new InvalidProgramException();
        }

        public static void AreNotEqual(object lhs, object rhs)
        {
            if (lhs == null && rhs == null) throw new InvalidProgramException();
            if (lhs == null || rhs == null) return;
            if (lhs.Equals(rhs)) throw new InvalidProgramException();
        }

        public static void IsTrue(bool condition)
        {
            if (!condition) throw new InvalidProgramException();
        }

        public static void IsFalse(bool condition)
        {
            if (condition) throw new InvalidProgramException();
        }
    }
}
