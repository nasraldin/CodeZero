//  <copyright file="CodeZeroMvcDateTimeBinder.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Web.Mvc;
using CodeZero.Timing;

namespace CodeZero.Web.Mvc.ModelBinding.Binders
{
    public class CodeZeroMvcDateTimeBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var date = base.BindModel(controllerContext, bindingContext) as DateTime?;
            if (date == null)
            {
                return null;
            }

            if (bindingContext.ModelMetadata.ContainerType != null)
            {
                if (bindingContext.ModelMetadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
                {
                    return date.Value;
                }

                var property = bindingContext.ModelMetadata.ContainerType.GetProperty(bindingContext.ModelName);

                if (property != null && property.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true))
                {
                    return date.Value;
                }
            }


            // Note: currently DisableDateTimeNormalizationAttribute is not supported for MVC action parameters.
            return Clock.Normalize(date.Value);
        }
    }
}
