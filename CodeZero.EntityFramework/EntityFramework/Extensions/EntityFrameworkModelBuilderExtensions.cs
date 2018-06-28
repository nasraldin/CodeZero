//  <copyright file="EntityFrameworkModelBuilderExtensions.cs" project="CodeZero.EntityFramework" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace CodeZero.EntityFramework.Extensions
{
    //TODO: We can create simpler extension methods to create indexes
    //TODO: Check https://github.com/mj1856/EntityFramework.IndexingExtensions for example
    public static class EntityFrameworkModelBuilderExtensions
    {
        public static PrimitivePropertyConfiguration CreateIndex(this PrimitivePropertyConfiguration propertyConfiguration)
        {
            return propertyConfiguration.HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute()
                    )
                );
        }

        public static PrimitivePropertyConfiguration CreateIndex(this PrimitivePropertyConfiguration propertyConfiguration, string name)
        {
            return propertyConfiguration.HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute(name)
                    )
                );
        }

        public static PrimitivePropertyConfiguration CreateIndex(this PrimitivePropertyConfiguration propertyConfiguration, string name, int order)
        {
            return propertyConfiguration.HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute(name, order)
                    )
                );
        }
    }
}