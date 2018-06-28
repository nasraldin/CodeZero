//  <copyright file="CustomValidator.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeZero.Dependency;

namespace CodeZero.Runtime.Validation.Interception
{
    public class CustomValidator : IMethodParameterValidator
    {
        private readonly IIocResolver _iocResolver;

        public CustomValidator(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IReadOnlyList<ValidationResult> Validate(object validatingObject)
        {
            var validationErrors = new List<ValidationResult>();

            if (validatingObject is ICustomValidate customValidateObject)
            {
                var context = new CustomValidationContext(validationErrors, _iocResolver);
                customValidateObject.AddValidationErrors(context);
            }

            return validationErrors;
        }
    }
}
