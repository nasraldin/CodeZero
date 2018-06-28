//  <copyright file="JsonExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Newtonsoft.Json;

namespace CodeZero.Json
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CodeZeroCamelCasePropertyNamesContractResolver();
            }
            else
            {
                options.ContractResolver = new CodeZeroContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }
            
            return JsonConvert.SerializeObject(obj, options);
        }
    }
}
