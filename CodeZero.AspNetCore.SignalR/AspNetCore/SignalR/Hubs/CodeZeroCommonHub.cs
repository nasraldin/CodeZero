//  <copyright file="CodeZeroCommonHub.cs" project="CodeZero.AspNetCore.SignalR" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Auditing;
using CodeZero.RealTime;

namespace CodeZero.AspNetCore.SignalR.Hubs
{
    public class CodeZeroCommonHub : OnlineClientHubBase
    {
        public CodeZeroCommonHub(IOnlineClientManager onlineClientManager, IClientInfoProvider clientInfoProvider) 
            : base(onlineClientManager, clientInfoProvider)
        {
        }

        public void Register()
        {
            Logger.Debug("A client is registered: " + Context.ConnectionId);
        }
    }
}
