//  <copyright file="DynamicHttpControllerDescriptor.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Collections.ObjectModel;
using System.Web.Http.Filters;
using CodeZero.Collections.Extensions;

namespace CodeZero.WebApi.Controllers.Dynamic.Selectors
{
    /// <summary>
    /// This class is used to extend the default controller descriptor to add the action filters dynamically.
    /// </summary>
    public class DynamicHttpControllerDescriptor : HttpControllerDescriptor
    {
        /// <summary>
        /// The Dynamic Controller Action filters.
        /// </summary>
        private readonly DynamicApiControllerInfo _controllerInfo;

        private readonly object[] _attributes;
        private readonly object[] _declaredOnlyAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicHttpControllerDescriptor"/> class. Add the argument for action filters to the controller.
        /// </summary>
        /// <param name="configuration">The Http Configuration.</param>
        /// <param name="controllerInfo">Controller info</param>
        public DynamicHttpControllerDescriptor(HttpConfiguration configuration, DynamicApiControllerInfo controllerInfo)
            : base(configuration, controllerInfo.ServiceName, controllerInfo.ApiControllerType)
        {
            _controllerInfo = controllerInfo;

            _attributes = controllerInfo.ServiceInterfaceType.GetCustomAttributes(true);
            _declaredOnlyAttributes = controllerInfo.ServiceInterfaceType.GetCustomAttributes(false);
        }

        /// <summary>
        /// The overrides the GetFilters for the controller and adds the Dynamic Controller filters.
        /// </summary>
        /// <returns> The Collection of filters.</returns>
        public override Collection<IFilter> GetFilters()
        {
            if (_controllerInfo.Filters.IsNullOrEmpty())
            {
                return base.GetFilters();
            }

            var actionFilters = new Collection<IFilter>();

            foreach (var filter in _controllerInfo.Filters)
            {
                actionFilters.Add(filter);
            }

            foreach (var baseFilter in base.GetFilters())
            {
                actionFilters.Add(baseFilter);
            }

            return actionFilters;
        }

        public override Collection<T> GetCustomAttributes<T>(bool inherit)
        {
            var attributes = inherit ? _attributes : _declaredOnlyAttributes;
            return new Collection<T>(DynamicApiDescriptorHelper.FilterType<T>(attributes));
        }
    }
}
