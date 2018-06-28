//  <copyright file="OnlineClientManager.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CodeZero.Collections.Extensions;
using CodeZero.Dependency;
using CodeZero.Extensions;
using JetBrains.Annotations;

namespace CodeZero.RealTime
{
    public class OnlineClientManager<T> : OnlineClientManager, IOnlineClientManager<T>
    {

    }

    /// <summary>
    /// Implements <see cref="IOnlineClientManager"/>.
    /// </summary>
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        public event EventHandler<OnlineClientEventArgs> ClientConnected;
        public event EventHandler<OnlineClientEventArgs> ClientDisconnected;
        public event EventHandler<OnlineUserEventArgs> UserConnected;
        public event EventHandler<OnlineUserEventArgs> UserDisconnected;

        /// <summary>
        /// Online clients.
        /// </summary>
        protected ConcurrentDictionary<string, IOnlineClient> Clients { get; }

        protected readonly object SyncObj = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClientManager"/> class.
        /// </summary>
        public OnlineClientManager()
        {
            Clients = new ConcurrentDictionary<string, IOnlineClient>();
        }

        public virtual void Add(IOnlineClient client)
        {
            lock (SyncObj)
            {
                var userWasAlreadyOnline = false;
                var user = client.ToUserIdentifierOrNull();

                if (user != null)
                {
                    userWasAlreadyOnline = this.IsOnline(user);
                }

                Clients[client.ConnectionId] = client;

                ClientConnected.InvokeSafely(this, new OnlineClientEventArgs(client));

                if (user != null && !userWasAlreadyOnline)
                {
                    UserConnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                }
            }
        }

        public virtual bool Remove(string connectionId)
        {
            lock (SyncObj)
            {
                IOnlineClient client;
                var isRemoved = Clients.TryRemove(connectionId, out client);

                if (isRemoved)
                {
                    var user = client.ToUserIdentifierOrNull();

                    if (user != null && !this.IsOnline(user))
                    {
                        UserDisconnected.InvokeSafely(this, new OnlineUserEventArgs(user, client));
                    }

                    ClientDisconnected.InvokeSafely(this, new OnlineClientEventArgs(client));
                }

                return isRemoved;
            }
        }

        public virtual IOnlineClient GetByConnectionIdOrNull(string connectionId)
        {
            lock (SyncObj)
            {
                return Clients.GetOrDefault(connectionId);
            }
        }
        
        public virtual IReadOnlyList<IOnlineClient> GetAllClients()
        {
            lock (SyncObj)
            {
                return Clients.Values.ToImmutableList();
            }
        }

        [NotNull]
        public virtual IReadOnlyList<IOnlineClient> GetAllByUserId([NotNull] IUserIdentifier user)
        {
            Check.NotNull(user, nameof(user));

            return GetAllClients()
                 .Where(c => (c.UserId == user.UserId && c.TenantId == user.TenantId))
                 .ToImmutableList();
        }
    }
}