//  <copyright file="UnitOfWorkMiddlewareOptions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.Domain.Uow;
using Microsoft.AspNetCore.Http;

namespace CodeZero.AspNetCore.Uow
{
    public class UnitOfWorkMiddlewareOptions
    {
        public Func<HttpContext, bool> Filter { get; set; } = context => true;

        public Func<HttpContext, UnitOfWorkOptions> OptionsFactory { get; set; } = context => new UnitOfWorkOptions();
    }
}
