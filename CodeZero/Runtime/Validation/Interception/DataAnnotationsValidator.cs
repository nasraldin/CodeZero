//  <copyright file="DataAnnotationsValidator.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CodeZero.Collections.Extensions;

namespace CodeZero.Runtime.Validation.Interception
{
    public class DataAnnotationsValidator : IMethodParameterValidator
    {
        public virtual IReadOnlyList<ValidationResult> Validate(object validatingObject)
        {
            return GetDataAnnotationAttributeErrors(validatingObject);
        }

        /// <summary>
        /// Checks all properties for DataAnnotations attributes.
        /// </summary>
        protected virtual List<ValidationResult> GetDataAnnotationAttributeErrors(object validatingObject)
        {
            var validationErrors = new List<ValidationResult>();

            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject)
                {
                    DisplayName = property.DisplayName,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result != null)
                    {
                        validationErrors.Add(result);
                    }
                }
            }

            return validationErrors;
        }
    }
}
