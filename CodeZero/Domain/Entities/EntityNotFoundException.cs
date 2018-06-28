//  <copyright file="EntityNotFoundException.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Runtime.Serialization;

namespace CodeZero.Domain.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// This exception is thrown if an entity excepted to be found but not found.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : CodeZeroException
    {
        /// <summary>
        /// Type of the entity.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Id of the Entity.
        /// </summary>
        public object Id { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException()
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public EntityNotFoundException(string message)
            : base(message)
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:CodeZero.Domain.Entities.EntityNotFoundException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
