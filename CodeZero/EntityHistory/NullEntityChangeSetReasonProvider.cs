//  <copyright file="NullEntityChangeSetReasonProvider.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Runtime.Remoting;

namespace CodeZero.EntityHistory
{
    /// <summary>
    /// Implements null object pattern for <see cref="IEntityChangeSetReasonProvider"/>.
    /// </summary>
    public class NullEntityChangeSetReasonProvider : EntityChangeSetReasonProviderBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullEntityChangeSetReasonProvider Instance { get; } = new NullEntityChangeSetReasonProvider();

        /// <inheritdoc/>
        public override string Reason => null;

        private NullEntityChangeSetReasonProvider()
            : base(
                  new DataContextAmbientScopeProvider<ReasonOverride>(new AsyncLocalAmbientDataContext())
            )
        {

        }
    }
}
