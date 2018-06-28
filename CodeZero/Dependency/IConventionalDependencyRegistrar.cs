//  <copyright file="IConventionalDependencyRegistrar.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Dependency
{
    /// <summary>
    /// This interface is used to register dependencies by conventions. 
    /// </summary>
    /// <remarks>
    /// Implement this interface and register to <see cref="IocManager.AddConventionalRegistrar"/> method to be able
    /// to register classes by your own conventions.
    /// </remarks>
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// Registers types of given assembly by convention.
        /// </summary>
        /// <param name="context">Registration context</param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}