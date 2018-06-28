//  <copyright file="HttpVerbHelper.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Web
{
    public static class HttpVerbHelper
    {
        public static HttpVerb Create(string httpMethod)
        {
            switch (httpMethod.ToUpperInvariant())
            {
                case "GET":
                    return HttpVerb.Get;
                case "POST":
                    return HttpVerb.Post;
                case "PUT":
                    return HttpVerb.Put;
                case "DELETE":
                    return HttpVerb.Delete;
                case "OPTIONS":
                    return HttpVerb.Options;
                case "TRACE":
                    return HttpVerb.Trace;
                case "HEAD":
                    return HttpVerb.Head;
                case "PATCH":
                    return HttpVerb.Patch;
                default:
                    throw new CodeZeroException("Unknown HTTP METHOD: " + httpMethod);
            }
        }
    }
}