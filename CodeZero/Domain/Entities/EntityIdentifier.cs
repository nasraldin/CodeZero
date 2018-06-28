//  <copyright file="EntityIdentifier.cs" project="CodeZero" solution="CodeZero">
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
    /// <summary>
    /// Used to identify an entity.
    /// Can be used to store an entity <see cref="Type"/> and <see cref="Id"/>.
    /// </summary>
    [Serializable]
    public class EntityIdentifier
    {
        /// <summary>
        /// Entity Type.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Entity's Id.
        /// </summary>
        public object Id { get; private set; }

        /// <summary>
        /// Added for serialization purposes.
        /// </summary>
        private EntityIdentifier()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityIdentifier"/> class.
        /// </summary>
        /// <param name="type">Entity type.</param>
        /// <param name="id">Id of the entity.</param>
        public EntityIdentifier(Type type, object id)
        {
            //if (type == null)
            //{
            //    throw new ArgumentNullException(nameof(type));
            //}

            //if (id == null)
            //{
            //    throw new ArgumentNullException(nameof(id));
            //}

            Type = type ?? throw new ArgumentNullException(nameof(type));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}