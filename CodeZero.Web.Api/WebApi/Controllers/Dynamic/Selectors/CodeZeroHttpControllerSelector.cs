//  <copyright file="CodeZeroHttpControllerSelector.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.WebApi.Controllers.Dynamic.Builders;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace CodeZero.WebApi.Controllers.Dynamic.Selectors
{
    /// <summary>
    /// This class is used to extend default controller selector to add dynamic api controller creation feature of CodeZero.
    /// It checks if requested controller is a dynamic api controller, if it is,
    /// returns <see cref="HttpControllerDescriptor"/> to ASP.NET system.
    /// </summary>
    public class CodeZeroHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly DynamicApiControllerManager _dynamicApiControllerManager;

        /// <summary>
        /// Creates a new <see cref="CodeZeroHttpControllerSelector"/> object.
        /// </summary>
        /// <param name="configuration">Http configuration</param>
        /// <param name="dynamicApiControllerManager"></param>
        public CodeZeroHttpControllerSelector(HttpConfiguration configuration, DynamicApiControllerManager dynamicApiControllerManager)
            : base(configuration)
        {
            _configuration = configuration;
            _dynamicApiControllerManager = dynamicApiControllerManager;
        }

        /// <summary>
        /// This method is called by Web API system to select the controller for this request.
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>The controller to be used</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //Get request and route data
            if (request == null)
            {
                return base.SelectController(null);
            }

            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                return base.SelectController(request);
            }

            //Get serviceNameWithAction from route
            if (!routeData.Values.TryGetValue("serviceNameWithAction", out object serviceNameWithAction))
            {
                return base.SelectController(request);
            }

            var convertedserviceNameWithAction = serviceNameWithAction.ToString();

            //Normalize serviceNameWithAction
            if (convertedserviceNameWithAction.EndsWith("/"))
            {
                serviceNameWithAction = convertedserviceNameWithAction.Substring(0, convertedserviceNameWithAction.Length - 1);
                routeData.Values["serviceNameWithAction"] = serviceNameWithAction;
            }

            //if (serviceNameWithAction.EndsWith("/"))
            //{
            //    serviceNameWithAction = serviceNameWithAction.Substring(0, serviceNameWithAction.Length - 1);
            //    routeData.Values["serviceNameWithAction"] = serviceNameWithAction;
            //}

            //Get the dynamic controller
            var hasActionName = false;
            var controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceNameWithAction.ToString());
            if (controllerInfo == null)
            {
                if (!DynamicApiServiceNameHelper.IsValidServiceNameWithAction(serviceNameWithAction.ToString()))
                {
                    return base.SelectController(request);
                }

                var serviceName = DynamicApiServiceNameHelper.GetServiceNameInServiceNameWithAction(serviceNameWithAction.ToString());
                controllerInfo = _dynamicApiControllerManager.FindOrNull(serviceName);
                if (controllerInfo == null)
                {
                    return base.SelectController(request);
                }

                hasActionName = true;
            }

            //Create the controller descriptor
            var controllerDescriptor = new DynamicHttpControllerDescriptor(_configuration, controllerInfo);
            controllerDescriptor.Properties["__CodeZeroDynamicApiControllerInfo"] = controllerInfo;
            controllerDescriptor.Properties["__CodeZeroDynamicApiHasActionName"] = hasActionName;
            return controllerDescriptor;
        }
    }
}