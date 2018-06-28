//  <copyright file="IBatchApiControllerBuilder.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Web.Http.Filters;
using CodeZero.Web;

namespace CodeZero.WebApi.Controllers.Dynamic.Builders
{
    /// <summary>
    /// This interface is used to define a dynamic api controllers.
    /// </summary>
    /// <typeparam name="T">Type of the proxied object</typeparam>
    public interface IBatchApiControllerBuilder<T>
    {
        /// <summary>
        /// Used to filter types.
        /// </summary>
        /// <param name="predicate">Predicate to filter types</param>
        IBatchApiControllerBuilder<T> Where(Func<Type, bool> predicate);

        /// <summary>
        /// Adds filters for dynamic controllers.
        /// </summary>
        /// <param name="filters"> The filters. </param>
        /// <returns>The current Controller Builder </returns>
        IBatchApiControllerBuilder<T> WithFilters(params IFilter[] filters);

        /// <summary>
        /// Enables/Disables API Explorer for dynamic controllers.
        /// </summary>
        IBatchApiControllerBuilder<T> WithApiExplorer(bool isEnabled);

        /// <summary>
        /// Enables/Disables proxy scripting for dynamic controllers.
        /// It's enabled by default.
        /// </summary>
        IBatchApiControllerBuilder<T> WithProxyScripts(bool isEnabled);

        /// <summary>
        /// Sets service name for controllers.
        /// </summary>
        /// <param name="serviceNameSelector">Service name selector</param>
        /// <returns></returns>
        IBatchApiControllerBuilder<T> WithServiceName(Func<Type, string> serviceNameSelector);

        /// <summary>
        /// Used to perform actions for each method of all dynamic api controllers.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The current Controller Builder</returns>
        IBatchApiControllerBuilder<T> ForMethods(Action<IApiControllerActionBuilder> action);

        /// <summary>
        /// Use conventional Http Verbs by method names.
        /// By default, it uses <see cref="HttpVerb.Post"/> for all actions.
        /// </summary>
        /// <returns>The current Controller Builder</returns>
        IBatchApiControllerBuilder<T> WithConventionalVerbs();

        /// <summary>
        /// Builds the controller.
        /// This method must be called at last of the build operation.
        /// </summary>
        void Build();
    }
}