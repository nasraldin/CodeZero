//  <copyright file="ParameterApiDescriptionModel.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Web.Api.Modeling
{
    [Serializable]
    public class ParameterApiDescriptionModel
    {
        public string NameOnMethod { get; }

        public string Name { get; }

        public Type Type { get; }

        public string TypeAsString { get; }

        public bool IsOptional { get;  }

        public object DefaultValue { get;  }

        public string[] ConstraintTypes { get; }

        public string BindingSourceId { get; }

        private ParameterApiDescriptionModel()
        {
            
        }

        public ParameterApiDescriptionModel(string name, string nameOnMethod, Type type, bool isOptional = false, object defaultValue = null, string[] constraintTypes = null, string bindingSourceId = null)
        {
            Name = name;
            NameOnMethod = nameOnMethod;
            Type = type;
            TypeAsString = type.FullName;
            IsOptional = isOptional;
            DefaultValue = defaultValue;
            ConstraintTypes = constraintTypes;
            BindingSourceId = bindingSourceId;
        }
    }
}