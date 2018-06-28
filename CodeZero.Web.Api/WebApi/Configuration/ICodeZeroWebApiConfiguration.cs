//  <copyright file="ICodeZeroWebApiConfiguration.cs" project="CodeZero.Web.Api" solution="CodeZero">
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
    /// <summary>
    /// Used to configure CodeZero WebApi module.
    /// </summary>
    public interface ICodeZeroWebApiConfiguration
    {
        /// <summary>
        /// Default UnitOfWorkAttribute for all actions.
        /// </summary>
        UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        /// <summary>
        /// Default WrapResultAttribute for all actions.
        /// </summary>
        WrapResultAttribute DefaultWrapResultAttribute { get; }

        /// <summary>
        /// Default WrapResultAttribute for all dynamic web api actions.
        /// </summary>
        WrapResultAttribute DefaultDynamicApiWrapResultAttribute { get; }

        /// <summary>
        /// List of URLs to ignore on result wrapping.
        /// </summary>
        List<string> ResultWrappingIgnoreUrls { get; }

        /// <summary>
        /// Gets/sets <see cref="HttpConfiguration"/>.
        /// </summary>
        HttpConfiguration HttpConfiguration { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsAutomaticAntiForgeryValidationEnabled { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool SetNoCacheForAllResponses { get; set; }

        /// <summary>
        /// Used to configure dynamic Web API controllers.
        /// </summary>
        IDynamicApiControllerBuilder DynamicApiControllerBuilder { get; }
    }
}