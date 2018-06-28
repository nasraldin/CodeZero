//  <copyright file="NotificationData.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using CodeZero.Collections.Extensions;
using CodeZero.Json;

namespace CodeZero.Notifications
{
    /// <summary>
    /// Used to store data for a notification.
    /// It can be directly used or can be derived.
    /// </summary>
    [Serializable]
    public class NotificationData
    {
        /// <summary>
        /// Gets notification data type name.
        /// It returns the full class name by default.
        /// </summary>
        public virtual string Type => GetType().FullName;

        /// <summary>
        /// Shortcut to set/get <see cref="Properties"/>.
        /// </summary>
        public object this[string key]
        {
            get { return Properties.GetOrDefault(key); }
            set { Properties[key] = value; }
        }

        /// <summary>
        /// Can be used to add custom properties to this notification.
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                /* Not assign value, but add dictionary items. This is required for backward compability. */
                foreach (var keyValue in value)
                {
                    if (!_properties.ContainsKey(keyValue.Key))
                    {
                        _properties[keyValue.Key] = keyValue.Value;
                    }
                }
            }
        }
        private readonly Dictionary<string, object> _properties;

        /// <summary>
        /// Createa a new NotificationData object.
        /// </summary>
        public NotificationData()
        {
            _properties = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}