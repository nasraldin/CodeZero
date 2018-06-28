//  <copyright file="IMayHaveOrganizationUnit.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Organizations
{
    /// <summary>
    /// This interface is implemented entities those may have an <see cref="OrganizationUnit"/>.
    /// </summary>
    public interface IMayHaveOrganizationUnit
    {
        /// <summary>
        /// <see cref="OrganizationUnit"/>'s Id which this entity belongs to.
        /// Can be null if this entity is not related to any <see cref="OrganizationUnit"/>.
        /// </summary>
        long? OrganizationUnitId { get; set; }
    }
}