//  <copyright file="OnlineClientHubBase.cs" project="CodeZero.AspNetCore.SignalR" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Threading.Tasks;
using CodeZero.Auditing;
using CodeZero.Dependency;
using CodeZero.RealTime;
using CodeZero.Runtime.Session;
using Castle.Core.Logging;

namespace CodeZero.AspNetCore.SignalR.Hubs
{
    public abstract class OnlineClientHubBase : CodeZeroHubBase, ITransientDependency
    {
        protected IOnlineClientManager OnlineClientManager { get; }
        protected IClientInfoProvider ClientInfoProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeZeroCommonHub"/> class.
        /// </summary>
        protected OnlineClientHubBase(
            IOnlineClientManager onlineClientManager,
            IClientInfoProvider clientInfoProvider)
        {
            OnlineClientManager = onlineClientManager;
            ClientInfoProvider = clientInfoProvider;

            Logger = NullLogger.Instance;
            CodeZeroSession = NullCodeZeroSession.Instance;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var client = CreateClientForCurrentConnection();

            Logger.Debug("A client is connected: " + client);

            OnlineClientManager.Add(client);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            Logger.Debug("A client is disconnected: " + Context.ConnectionId);

            try
            {
                OnlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        protected virtual IOnlineClient CreateClientForCurrentConnection()
        {
            return new OnlineClient(
                Context.ConnectionId,
                GetIpAddressOfClient(),
                CodeZeroSession.TenantId,
                CodeZeroSession.UserId
            );
        }

        protected virtual string GetIpAddressOfClient()
        {
            try
            {
                return ClientInfoProvider.ClientIpAddress;
            }
            catch (Exception ex)
            {
                Logger.Error("Can not find IP address of the client! connectionId: " + Context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }
    }
}