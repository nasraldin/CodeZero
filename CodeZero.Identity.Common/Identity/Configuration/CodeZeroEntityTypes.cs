//  <copyright file="CodeZeroEntityTypes.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;
using CodeZero.Authorization.Roles;
using CodeZero.Authorization.Users;
using CodeZero.MultiTenancy;

namespace CodeZero.Identity.Configuration
{
    public class CodeZeroEntityTypes : ICodeZeroEntityTypes
    {
        public Type User
        {
            get { return _user; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof (CodeZeroUserBase).IsAssignableFrom(value))
                {
                    throw new CodeZeroException(value.AssemblyQualifiedName + " should be derived from " + typeof(CodeZeroUserBase).AssemblyQualifiedName);
                }

                _user = value;
            }
        }
        private Type _user;

        public Type Role
        {
            get { return _role; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof(CodeZeroRoleBase).IsAssignableFrom(value))
                {
                    throw new CodeZeroException(value.AssemblyQualifiedName + " should be derived from " + typeof(CodeZeroRoleBase).AssemblyQualifiedName);
                }

                _role = value;
            }
        }
        private Type _role;

        public Type Tenant
        {
            get { return _tenant; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof(CodeZeroTenantBase).IsAssignableFrom(value))
                {
                    throw new CodeZeroException(value.AssemblyQualifiedName + " should be derived from " + typeof(CodeZeroTenantBase).AssemblyQualifiedName);
                }

                _tenant = value;
            }
        }
        private Type _tenant;
    }
}