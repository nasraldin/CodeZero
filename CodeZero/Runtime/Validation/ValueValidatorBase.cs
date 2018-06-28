//  <copyright file="ValueValidatorBase.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeZero.Collections.Extensions;

namespace CodeZero.Runtime.Validation
{
    [Serializable]
    public abstract class ValueValidatorBase : IValueValidator
    {
        public virtual string Name
        {
            get
            {
                var type = GetType().GetTypeInfo();
                if (type.IsDefined(typeof(ValidatorAttribute)))
                {
                    return type.GetCustomAttributes(typeof(ValidatorAttribute)).Cast<ValidatorAttribute>().First().Name;
                }

                return type.Name;
            }
        }

        /// <summary>
        /// Gets/sets arbitrary objects related to this object.
        /// Gets null if given key does not exists.
        /// </summary>
        /// <param name="key">Key</param>
        public object this[string key]
        {
            get { return Attributes.GetOrDefault(key); }
            set { Attributes[key] = value; }
        }

        /// <summary>
        /// Arbitrary objects related to this object.
        /// </summary>
        public IDictionary<string, object> Attributes { get; private set; }

        public abstract bool IsValid(object value);

        protected ValueValidatorBase()
        {
            Attributes = new Dictionary<string, object>();
        }
    }
}