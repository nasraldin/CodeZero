//  <copyright file="IDynamicApiClient.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.WebApi.Controllers.Dynamic.Clients
{
    /// <summary>
    /// Defines interface of a client to use a remote Web Api service.
    /// </summary>
    /// <typeparam name="TService">Service type</typeparam>
    public interface IDynamicApiClient<out TService>
    {
        /// <summary>
        /// Url of the service.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// The service object.
        /// </summary>
        TService Service { get; }
    }
}
