//  <copyright file="CodeZeroDateTimeModelBinderProvider.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Timing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace CodeZero.AspNetCore.Mvc.ModelBinding
{
    public class CodeZeroDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(DateTime) &&
                context.Metadata.ModelType != typeof(DateTime?))
                return null;

            if (context.Metadata.ContainerType == null) return null;

            var dateNormalizationDisabledForClass =
                context.Metadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);
            // ReSharper disable once PossibleNullReferenceException
            var dateNormalizationDisabledForProperty = context.Metadata.ContainerType
                .GetProperty(context.Metadata.PropertyName)
                .IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);

            if (!dateNormalizationDisabledForClass && !dateNormalizationDisabledForProperty)
                return new CodeZeroDateTimeModelBinder(context.Metadata.ModelType);

            return null;
        }
    }
}