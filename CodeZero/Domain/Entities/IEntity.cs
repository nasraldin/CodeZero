//  <copyright file="IEntity.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Domain.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// A shortcut of <see cref="T:CodeZero.Domain.Entities.IEntity`1" /> for most used primary key type (<see cref="T:System.Int32" />).
    /// </summary>
    public interface IEntity : IEntity<int>
    {

    }
}