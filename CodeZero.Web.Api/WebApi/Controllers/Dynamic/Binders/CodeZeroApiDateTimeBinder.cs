//  <copyright file="CodeZeroApiDateTimeBinder.cs" project="CodeZero.Web.Api" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Globalization;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using CodeZero.Timing;

namespace CodeZero.WebApi.Controllers.Dynamic.Binders
{
    /// <summary>
    /// Binds datetime values from api requests to model
    /// </summary>
    public class CodeZeroApiDateTimeBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var date = value?.ConvertTo(typeof(DateTime?), CultureInfo.CurrentCulture) as DateTime?;
            if (date == null)
            {
                return true;
            }

            if (bindingContext.ModelMetadata.ContainerType != null)
            {
                if (bindingContext.ModelMetadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
                {
                    bindingContext.Model = date.Value;
                    return true;
                }

                var property = bindingContext.ModelMetadata.ContainerType.GetProperty(bindingContext.ModelName);

                if (property != null && property.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
                {
                    bindingContext.Model = date.Value;
                    return true;
                }
            }

            var parameter = actionContext.ActionDescriptor.GetParameters().FirstOrDefault(p => p.ParameterName == bindingContext.ModelName);
            if (parameter != null && parameter.GetCustomAttributes<DisableDateTimeNormalizationAttribute>().Count > 0)
            {
                bindingContext.Model = date.Value;
                return true;
            }

            bindingContext.Model = Clock.Normalize(date.Value);
            return true;
        }
    }
}
