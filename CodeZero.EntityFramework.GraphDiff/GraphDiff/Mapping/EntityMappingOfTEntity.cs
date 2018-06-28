//  <copyright file="EntityMappingOfTEntity.cs" project="CodeZero.EntityFramework.GraphDiff" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.EntityFramework.GraphDiff.Mapping
{
    public class EntityMapping
    {
        public Type EntityType;

        public object MappingExpression { get; set; }

        public EntityMapping(Type type, object mappingExpression)
        {
            EntityType = type;
            MappingExpression = mappingExpression;
        }
    }
}