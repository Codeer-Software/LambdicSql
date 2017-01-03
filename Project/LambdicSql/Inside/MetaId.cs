using System.Reflection;

namespace LambdicSql.Inside
{
    class MetaId
    {
        readonly string _moduleFullyQualifiedName;
        readonly long _core;

        public MetaId(MemberInfo member) : this(member.Module.FullyQualifiedName, member.MetadataToken) { }

        public MetaId(string moduleFullyQualifiedName, int member)
        {
            _moduleFullyQualifiedName = moduleFullyQualifiedName;
            _core = ((long)moduleFullyQualifiedName.GetHashCode() << 32) + member;
        }

        public override bool Equals(object obj)
        {
            var target = obj as MetaId;
            if (target == null) return false;
            return _core == target._core && _moduleFullyQualifiedName == target._moduleFullyQualifiedName;
        }

        public override int GetHashCode() => _core.GetHashCode();

        public static bool operator ==(MetaId lhs, MetaId rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            return lhs._core == rhs._core;
        }

        public static bool operator !=(MetaId lhs, MetaId rhs) => !(lhs == rhs);
    }
}
