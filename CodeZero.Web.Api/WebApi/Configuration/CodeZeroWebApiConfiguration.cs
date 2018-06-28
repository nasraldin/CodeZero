//  <copyright file="CodeZeroWebApiConfiguration.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Web.Http;
using CodeZero.Domain.Uow;
using CodeZero.Web.Models;
using CodeZero.WebApi.Controllers.Dynamic.Builders;

namespace CodeZero.WebApi.Configuration
{
    internal class CodeZeroWebApiConfiguration : ICodeZeroWebApiConfiguration
    {
        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public WrapResultAttribute DefaultDynamicApiWrapResultAttribute { get; }

        public List<string> ResultWrappingIgnoreUrls { get; }

        public HttpConfiguration HttpConfiguration { get; set; }

        public bool IsValidationEnabledForControllers { get; set; }

        public bool IsAutomaticAntiForgeryValidationEnabled { get; set; }

        public bool SetNoCacheForAllResponses { get; set; }

        public IDynamicApiControllerBuilder DynamicApiControllerBuilder { get; }

        public CodeZeroWebApiConfiguration(IDynamicApiControllerBuilder dynamicApiControllerBuilder)
        {
            DynamicApiControllerBuilder = dynamicApiControllerBuilder;

            HttpConfiguration = GlobalConfiguration.Configuration;
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            DefaultWrapResultAttribute = new WrapResultAttribute(false);
            DefaultDynamicApiWrapResultAttribute = new WrapResultAttribute();
            ResultWrappingIgnoreUrls = new List<string>();
            IsValidationEnabledForControllers = true;
            IsAutomaticAntiForgeryValidationEnabled = true;
            SetNoCacheForAllResponses = true;
        }
    }
}