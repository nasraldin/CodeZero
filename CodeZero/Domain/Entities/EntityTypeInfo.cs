//  <copyright file="EntityTypeInfo.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Domain.Entities
{
    public class EntityTypeInfo
    {
        /// <summary>
        /// Type of the entity.
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// DbContext type that has DbSet property.
        /// </summary>
        public Type DeclaringType { get; private set; }

        public EntityTypeInfo(Type entityType, Type declaringType)
        {
            EntityType = entityType;
            DeclaringType = declaringType;
        }
    }
}