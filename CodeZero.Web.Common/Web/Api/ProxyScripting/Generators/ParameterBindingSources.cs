//  <copyright file="ParameterBindingSources.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Web.Api.ProxyScripting.Generators
{
    public static class ParameterBindingSources
    {
        public const string ModelBinding = "ModelBinding";
        public const string Query = "Query";
        public const string Body = "Body";
        public const string Path = "Path";
        public const string Form = "Form";
        public const string Header = "Header";
        public const string Custom = "Custom";
        public const string Services = "Services";
    }
}