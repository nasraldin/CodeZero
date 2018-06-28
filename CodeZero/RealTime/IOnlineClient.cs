//  <copyright file="IOnlineClient.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;

namespace CodeZero.RealTime
{
    /// <summary>
    /// Represents an online client connected to the application.
    /// </summary>
    public interface IOnlineClient
    {
        /// <summary>
        /// Unique connection Id for this client.
        /// </summary>
        string ConnectionId { get; }

        /// <summary>
        /// IP address of this client.
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// Tenant Id.
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// User Id.
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// Connection establishment time for this client.
        /// </summary>
        DateTime ConnectTime { get; }

        /// <summary>
        /// Shortcut to set/get <see cref="Properties"/>.
        /// </summary>
        object this[string key] { get; set; }

        /// <summary>
        /// Can be used to add custom properties for this client.
        /// </summary>
        Dictionary<string, object> Properties { get; }
    }
}