//  <copyright file="IAuditInfoProvider.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Auditing
{
    /// <summary>
    /// Provides an interface to provide audit informations in the upper layers.
    /// </summary>
    public interface IAuditInfoProvider
    {
        /// <summary>
        /// Called to fill needed properties.
        /// </summary>
        /// <param name="auditInfo">Audit info that is partially filled</param>
        void Fill(AuditInfo auditInfo);
    }
}