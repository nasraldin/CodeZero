//  <copyright file="UserManagementConfig.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Collections;

namespace CodeZero.Identity.Configuration
{
    public class UserManagementConfig : IUserManagementConfig
    {
        public ITypeList<object> ExternalAuthenticationSources { get; set; }

        public UserManagementConfig()
        {
            ExternalAuthenticationSources = new TypeList();
        }
    }
}