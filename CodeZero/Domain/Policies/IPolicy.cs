//  <copyright file="IPolicy.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;

namespace CodeZero.Domain.Policies
{
    /// <summary>
    /// This interface can be implemented by all Policy classes/interfaces to identify them by convention.
    /// </summary>
    public interface IPolicy : ITransientDependency
    {

    }
}
