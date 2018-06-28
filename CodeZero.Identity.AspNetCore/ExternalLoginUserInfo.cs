//  <copyright file="ExternalLoginUserInfo.cs" project="CodeZero.Identity.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Extensions;
using Microsoft.AspNet.Identity;

namespace CodeZero.Identity.AspNetCore
{
    public class ExternalLoginUserInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }

        public UserLoginInfo LoginInfo { get; set; }

        public bool HasAllNonEmpty()
        {
            return !Name.IsNullOrEmpty() &&
                   !Surname.IsNullOrEmpty() &&
                   !EmailAddress.IsNullOrEmpty();
        }
    }
}