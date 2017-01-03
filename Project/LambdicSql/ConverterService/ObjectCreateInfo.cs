﻿using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.ConverterService
{
    /// <summary>
    /// Object create info.
    /// It is generated information in the SELECT clause.
    /// </summary>
    public class ObjectCreateInfo
    {
        /// <summary>
        /// Object create member.
        /// </summary>
        public ObjectCreateMemberInfo[] Members { get; }

        /// <summary>
        /// Expression.
        /// </summary>
        public Expression Expression { get; }

        internal ObjectCreateInfo(IEnumerable<ObjectCreateMemberInfo> members, Expression expression)
        {
            Members = members.ToArray();
            Expression = expression;
        }
    }
}