//  <copyright file="EntityChangeReport.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Linq;

namespace CodeZero.Events.Bus.Entities
{
    public class EntityChangeReport
    {
        public List<EntityChangeEntry> ChangedEntities { get; }

        public List<DomainEventEntry> DomainEvents { get; }

        public EntityChangeReport()
        {
            ChangedEntities = new List<EntityChangeEntry>();
            DomainEvents = new List<DomainEventEntry>();
        }

        public bool IsEmpty()
        {
            return ChangedEntities.Count <= 0 && DomainEvents.Count <= 0;
        }

        public override string ToString()
        {
            return $"[EntityChangeReport] ChangedEntities: {ChangedEntities.Count}, DomainEvents: {DomainEvents.Count}";
        }
    }
}