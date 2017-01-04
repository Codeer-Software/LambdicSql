using System.Reflection;

namespace LambdicSql.ConverterServices.Inside
{
    class MetaId
    {
        readonly string _moduleFullyQualifiedName;
        readonly int _memberToken;
        readonly int _hash;

        public MetaId(MemberInfo member) : this(member.Module.FullyQualifiedName, member.MetadataToken) { }

        public MetaId(string moduleFullyQualifiedName, int memberToken)
        {
            _moduleFullyQualifiedName = moduleFullyQualifiedName;
            _memberToken = memberToken;
            _hash = (((long)moduleFullyQualifiedName.GetHashCode() << 32) + memberToken).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var target = obj as MetaId;
            if (target == null) return false;
            return _memberToken == target._memberToken && _moduleFullyQualifiedName == target._moduleFullyQualifiedName;
        }

        public override int GetHashCode() => _hash;

        public static bool operator == (MetaId lhs, MetaId rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            return lhs._memberToken == rhs._memberToken && lhs._moduleFullyQualifiedName == rhs._moduleFullyQualifiedName;
        }

        public static bool operator != (MetaId lhs, MetaId rhs) => !(lhs == rhs);
    }
}
