using System.Reflection;

namespace LambdicSql.ConverterServices.Inside
{
    class MetaId
    {
        MemberInfo _member;
        int _hash;

        public MetaId(MemberInfo member)
        {
            _member = member;
            _hash = member.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var target = obj as MetaId;
            if (target == null) return false;
            if (ReferenceEquals(_member, target._member)) return true;
            return _member.Equals(target._member);
        }

        public override int GetHashCode() => _hash;

        public static bool operator == (MetaId lhs, MetaId rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            if (ReferenceEquals(lhs._member, rhs._member)) return true;
            return lhs._member.Equals(rhs._member);
        }

        public static bool operator != (MetaId lhs, MetaId rhs) => !(lhs == rhs);
    }
}
