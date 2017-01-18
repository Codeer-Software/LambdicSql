using System.Reflection;

namespace LambdicSql.ConverterServices.Inside
{
    class MetaId
    {
        MemberInfo _member;
        readonly int _memberToken;

        public MetaId(MemberInfo member)
        {
            _member = member;
            _memberToken = _member.MetadataToken;
        }

        public override bool Equals(object obj)
        {
            var target = obj as MetaId;
            if (target == null) return false;
            if (_memberToken != target._memberToken) return false;
            return _memberToken == target._memberToken && _member.DeclaringType.FullName == target._member.DeclaringType.FullName;
        }

        public override int GetHashCode() => _memberToken;

        public static bool operator == (MetaId lhs, MetaId rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            return lhs._memberToken == rhs._memberToken && lhs._member.DeclaringType.FullName == rhs._member.DeclaringType.FullName;
        }

        public static bool operator != (MetaId lhs, MetaId rhs) => !(lhs == rhs);
    }
}
