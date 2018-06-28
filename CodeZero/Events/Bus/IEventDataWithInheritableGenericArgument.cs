//  <copyright file="IEventDataWithInheritableGenericArgument.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Events.Bus
{
    /// <summary>
    /// This interface must be implemented by event data classes that
    /// has a single generic argument and this argument will be used by inheritance. 
    /// 
    /// For example;
    /// Assume that Student inherits From Person. When trigger an EntityCreatedEventData{Student},
    /// EntityCreatedEventData{Person} is also triggered if EntityCreatedEventData implements
    /// this interface.
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// Gets arguments to create this class since a new instance of this class is created.
        /// </summary>
        /// <returns>Constructor arguments</returns>
        object[] GetConstructorArgs();
    }
}