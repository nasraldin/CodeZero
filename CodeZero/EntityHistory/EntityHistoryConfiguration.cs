//  <copyright file="EntityHistoryConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;

namespace CodeZero.EntityHistory
{
    internal class EntityHistoryConfiguration : IEntityHistoryConfiguration
    {
        public bool IsEnabled { get; set; }

        public bool IsEnabledForAnonymousUsers { get; set; }

        public IEntityHistorySelectorList Selectors { get; }

        public List<Type> IgnoredTypes { get; }

        public EntityHistoryConfiguration()
        {
            IsEnabled = true;
            Selectors = new EntityHistorySelectorList();
            IgnoredTypes = new List<Type>()
            {
                typeof(EntityChangeSet),
                typeof(EntityChange),
                typeof(EntityPropertyChange)
            };
        }
    }
}
