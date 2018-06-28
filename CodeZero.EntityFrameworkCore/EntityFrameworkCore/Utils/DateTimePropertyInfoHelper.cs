//  <copyright file="DateTimePropertyInfoHelper.cs" project="CodeZero.EntityFrameworkCore" solution="CodeZero">
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using CodeZero.Reflection;
using CodeZero.Timing;

namespace CodeZero.EntityFrameworkCore.Utils
{
    internal static class DateTimePropertyInfoHelper
    {
        /// <summary>
        /// Key: Entity type
        /// Value: DateTime property infos
        /// </summary>
        private static readonly ConcurrentDictionary<Type, EntityDateTimePropertiesInfo> DateTimeProperties;

        static DateTimePropertyInfoHelper()
        {
            DateTimeProperties = new ConcurrentDictionary<Type, EntityDateTimePropertiesInfo>();
        }

        public static EntityDateTimePropertiesInfo GetDatePropertyInfos(Type entityType)
        {
            return DateTimeProperties.GetOrAdd(
                       entityType,
                       key => FindDatePropertyInfosForType(entityType)
                   );
        }

        public static void NormalizeDatePropertyKinds(object entity, Type entityType)
        {
            var dateTimePropertyInfos = GetDatePropertyInfos(entityType);

            dateTimePropertyInfos.DateTimePropertyInfos.ForEach(property =>
            {
                var dateTime = (DateTime?)property.GetValue(entity);
                if (dateTime.HasValue)
                {
                    property.SetValue(entity, Clock.Normalize(dateTime.Value));
                }
            });

            dateTimePropertyInfos.ComplexTypePropertyPaths.ForEach(propertPath =>
            {
                var dateTime = (DateTime?)ReflectionHelper.GetValueByPath(entity, entityType, propertPath);
                if (dateTime.HasValue)
                {
                    ReflectionHelper.SetValueByPath(entity, entityType, propertPath, Clock.Normalize(dateTime.Value));
                }
            });
        }

        private static EntityDateTimePropertiesInfo FindDatePropertyInfosForType(Type entityType)
        {
            var datetimeProperties = entityType.GetProperties()
                                     .Where(property =>
                                         (property.PropertyType == typeof(DateTime) ||
                                         property.PropertyType == typeof(DateTime?)) &&
                                         property.CanWrite
                                     ).ToList();

            var complexTypeProperties = entityType.GetProperties()
                                                   .Where(p => p.PropertyType.GetTypeInfo().IsDefined(typeof(ComplexTypeAttribute), true))
                                                   .ToList();

            var complexTypeDateTimePropertyPaths = new List<string>();
            foreach (var complexTypeProperty in complexTypeProperties)
            {
                AddComplexTypeDateTimePropertyPaths(entityType.FullName + "." + complexTypeProperty.Name, complexTypeProperty, complexTypeDateTimePropertyPaths);
            }

            return new EntityDateTimePropertiesInfo
            {
                DateTimePropertyInfos = datetimeProperties,
                ComplexTypePropertyPaths = complexTypeDateTimePropertyPaths
            };
        }

        private static void AddComplexTypeDateTimePropertyPaths(string pathPrefix, PropertyInfo complexProperty, List<string> complexTypeDateTimePropertyPaths)
        {
            if (!complexProperty.PropertyType.GetTypeInfo().IsDefined(typeof(ComplexTypeAttribute), true))
            {
                return;
            }

            var complexTypeDateProperties = complexProperty.PropertyType
                                                            .GetProperties()
                                                            .Where(property =>
                                                                property.PropertyType == typeof(DateTime) ||
                                                                property.PropertyType == typeof(DateTime?)
                                                            ).Select(p => pathPrefix + "." + p.Name).ToList();

            complexTypeDateTimePropertyPaths.AddRange(complexTypeDateProperties);

            var complexTypeProperties = complexProperty.PropertyType.GetProperties()
                                                  .Where(p => p.PropertyType.GetTypeInfo().IsDefined(typeof(ComplexTypeAttribute), true))
                                                  .ToList();

            if (!complexTypeProperties.Any())
            {
                return;
            }

            foreach (var complexTypeProperty in complexTypeProperties)
            {
                AddComplexTypeDateTimePropertyPaths(pathPrefix + "." + complexTypeProperty.Name, complexTypeProperty, complexTypeDateTimePropertyPaths);
            }
        }
    }
}
