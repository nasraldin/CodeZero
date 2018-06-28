//  <copyright file="CodeZeroSignalRContractResolver.cs" project="CodeZero.Web.SignalR" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace CodeZero.Web.SignalR
{
    /// <summary>
    /// Uses CamelCasePropertyNamesContractResolver instead of DefaultContractResolver for SignalR communication. 
    /// </summary>
    public class CodeZeroSignalRContractResolver : IContractResolver
    {
        /// <summary>
        /// List of ignored assemblies.
        /// It contains only the SignalR's own assembly.
        /// If you don't want that your assembly's types are automatically camel cased while sending to the client, then add it to this list.
        /// </summary>
        public static List<Assembly> IgnoredAssemblies { get; private set; }

        private readonly IContractResolver _camelCaseContractResolver;
        private readonly IContractResolver _defaultContractSerializer;

        static CodeZeroSignalRContractResolver()
        {
            IgnoredAssemblies = new List<Assembly>
            {
                typeof (Connection).Assembly
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeZeroSignalRContractResolver"/> class.
        /// </summary>
        public CodeZeroSignalRContractResolver()
        {
            _defaultContractSerializer = new DefaultContractResolver();
            _camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public JsonContract ResolveContract(Type type)
        {
            if (IgnoredAssemblies.Contains(type.Assembly))
            {
                return _defaultContractSerializer.ResolveContract(type);
            }

            return _camelCaseContractResolver.ResolveContract(type);
        }
    }
}
