using System;
using System.Reflection;

namespace LambdicSql.ConverterServices.Inside
{
    class MetaId
    {
        MemberInfo _member;
        Type _declaringType;
        readonly int _memberToken;

        public MetaId(MemberInfo member)
        {
            _member = member;
            _declaringType = member.DeclaringType;
            _memberToken = member.MetadataToken;
        }

        public override bool Equals(object obj)
        {
            var target = obj as MetaId;
            if (target == null) return false;
            if (ReferenceEquals(_member, target._member)) return true;
            if (_memberToken != target._memberToken) return false;
            return _memberToken == target._memberToken && _declaringType == target._declaringType;
        }

        public override int GetHashCode() => _memberToken;

        public static bool operator == (MetaId lhs, MetaId rhs)
        {
            if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            return lhs._memberToken == rhs._memberToken && lhs._declaringType == rhs._declaringType;
        }

        public static bool operator != (MetaId lhs, MetaId rhs) => !(lhs == rhs);
    }
}
