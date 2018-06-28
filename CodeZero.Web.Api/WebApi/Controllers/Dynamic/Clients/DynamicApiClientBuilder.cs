//  <copyright file="DynamicApiClientBuilder.cs" project="CodeZero.Web.Api" solution="CodeZero">
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
    /// TODO: This class and namespace is being developed. See https://github.com/aspnetboilerplate/aspnetboilerplate/issues/66 
    /// </summary>
    public static class DynamicApiClientBuilder
    {
        public static IApiClientBuilder<TService> For<TService>(string url)
        {
            return new ApiClientBuilder<TService>(url);
        }
    }
}