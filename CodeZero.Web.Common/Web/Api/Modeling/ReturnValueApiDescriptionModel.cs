//  <copyright file="ReturnValueApiDescriptionModel.cs" project="CodeZero.Web.Common" solution="CodeZero">
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
    public class ReturnValueApiDescriptionModel
    {
        public Type Type { get; }
        public string TypeAsString { get; }

        public ReturnValueApiDescriptionModel(Type type)
        {
            Type = type;
            TypeAsString = type.FullName;
        }
    }
}