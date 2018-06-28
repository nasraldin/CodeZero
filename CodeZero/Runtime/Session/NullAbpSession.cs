//  <copyright file="NullAbpSession.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;
using CodeZero.MultiTenancy;
using CodeZero.Runtime.Remoting;

namespace CodeZero.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="ICodeZeroSession"/>.
    /// </summary>
    public class NullCodeZeroSession : CodeZeroSessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullCodeZeroSession Instance { get; } = new NullCodeZeroSession();

        /// <inheritdoc/>
        public override long? UserId => null;

        /// <inheritdoc/>
        public override int? TenantId => null;

        public override MultiTenancySides MultiTenancySide => MultiTenancySides.Tenant;

        public override long? ImpersonatorUserId => null;

        public override int? ImpersonatorTenantId => null;

        private NullCodeZeroSession() 
            : base(
                  new MultiTenancyConfig(), 
                  new DataContextAmbientScopeProvider<SessionOverride>(new AsyncLocalAmbientDataContext())
            )
        {

        }
    }
}