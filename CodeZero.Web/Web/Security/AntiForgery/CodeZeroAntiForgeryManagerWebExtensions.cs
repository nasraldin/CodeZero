//  <copyright file="CodeZeroAntiForgeryManagerWebExtensions.cs" project="CodeZero.Web" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Reflection;
using CodeZero.Reflection;

namespace CodeZero.Web.Security.AntiForgery
{
    public static class CodeZeroAntiForgeryManagerWebExtensions
    {
        public static bool ShouldValidate(
            this ICodeZeroAntiForgeryManager manager,
            ICodeZeroAntiForgeryWebConfiguration antiForgeryWebConfiguration,
            MethodInfo methodInfo, 
            HttpVerb httpVerb, 
            bool defaultValue)
        {
            if (!antiForgeryWebConfiguration.IsEnabled)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(ValidateCodeZeroAntiForgeryTokenAttribute), true))
            {
                return true;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableCodeZeroAntiForgeryTokenValidationAttribute>(methodInfo) != null)
            {
                return false;
            }

            if (antiForgeryWebConfiguration.IgnoredHttpVerbs.Contains(httpVerb))
            {
                return false;
            }

            if (methodInfo.DeclaringType?.IsDefined(typeof(ValidateCodeZeroAntiForgeryTokenAttribute), true) ?? false)
            {
                return true;
            }

            return defaultValue;
        }
    }
}
