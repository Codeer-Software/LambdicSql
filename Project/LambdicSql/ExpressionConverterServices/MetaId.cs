using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.Inside
{
    class MetaId
    {
        readonly long _core;

        public MetaId(MemberInfo member) : this(member.Module.MetadataToken, member.MetadataToken) { }

        public MetaId(int modeule, int member)
        {
            _core = ((long)modeule << 32) + member;
        }

        public override bool Equals(object obj)
        {
            var target = obj as MetaId;
            if (target == null) return false;
            return _core.Equals(target._core);
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
