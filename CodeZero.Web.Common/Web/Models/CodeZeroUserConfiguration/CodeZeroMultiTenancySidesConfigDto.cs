//  <copyright file="CodeZeroMultiTenancySidesConfigDto.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.MultiTenancy;

namespace CodeZero.Web.Models.CodeZeroUserConfiguration
{
    public class CodeZeroMultiTenancySidesConfigDto
    {
        public MultiTenancySides Host { get; private set; }

        public MultiTenancySides Tenant { get; private set; }

        public CodeZeroMultiTenancySidesConfigDto()
        {
            Host = MultiTenancySides.Host;
            Tenant = MultiTenancySides.Tenant;
        }
    }
}