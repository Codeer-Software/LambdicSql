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
    }
}
