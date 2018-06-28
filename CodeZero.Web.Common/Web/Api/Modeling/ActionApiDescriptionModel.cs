//  <copyright file="ActionApiDescriptionModel.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;

namespace CodeZero.Web.Api.Modeling
{
    [Serializable]
    public class ActionApiDescriptionModel
    {
        public string Name { get; }

        public string HttpMethod { get; }

        public string Url { get; }

        public IList<ParameterApiDescriptionModel> Parameters { get; }

        public ReturnValueApiDescriptionModel ReturnValue { get; }

        private ActionApiDescriptionModel()
        {

        }

        public ActionApiDescriptionModel(string name, ReturnValueApiDescriptionModel returnValue, string url, string httpMethod = null)
        {
            Name = name;
            ReturnValue = returnValue;
            Url = url;
            HttpMethod = httpMethod;

            Parameters = new List<ParameterApiDescriptionModel>();
        }

        public ParameterApiDescriptionModel AddParameter(ParameterApiDescriptionModel parameter)
        {
            Parameters.Add(parameter);
            return parameter;
        }
    }
}