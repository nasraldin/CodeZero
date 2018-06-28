//  <copyright file="CodeZeroMvcAntiForgeryTokenManager.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CodeZero.Web.Security.AntiForgery;

namespace CodeZero.Web.Mvc.Security.AntiForgery
{
    public class CodeZeroMvcAntiForgeryManager : CodeZeroAntiForgeryManager
    {
        private static readonly Lazy<object> AntiForgeryWorkerObject = new Lazy<object>(() =>
        {
            var antiForgeryWorkerField = typeof(System.Web.Helpers.AntiForgery).GetField("_worker", BindingFlags.NonPublic | BindingFlags.Static);
            if (antiForgeryWorkerField == null)
            {
                throw new CodeZeroException("Can not get _worker field of System.Web.Helpers.AntiForgery class. It's internal implementation might be changed. Please create an issue on GitHub repository to solve this.");
            }

            return antiForgeryWorkerField.GetValue(null);
        });

        private static readonly Lazy<MethodInfo> GetFormInputElementMethod = new Lazy<MethodInfo>(() =>
        {
            return AntiForgeryWorkerObject.Value
                .GetType()
                .GetMethod("GetFormInputElement", BindingFlags.Public | BindingFlags.Instance);
        });

        public CodeZeroMvcAntiForgeryManager(ICodeZeroAntiForgeryConfiguration configuration)
            : base(configuration)
        {

        }

        public override string GenerateToken()
        {
            /* Getting Token from input element, like done in views.
             * We are using reflection because some types/methods are internal!
             */

            var tagBuilder = (TagBuilder)GetFormInputElementMethod.Value.Invoke(
                AntiForgeryWorkerObject.Value,
                new object[]
                {
                    new HttpContextWrapper(HttpContext.Current)
                }
            );

            return tagBuilder.Attributes["value"];
        }

        public override bool IsValid(string cookieValue, string tokenValue)
        {
            try
            {
                System.Web.Helpers.AntiForgery.Validate(
                    HttpContext.Current.Request.Cookies[AntiForgeryConfig.CookieName]?.Value ?? cookieValue,
                    tokenValue
                    );

                return true;
            }
            catch (HttpAntiForgeryException ex)
            {
                Logger.Warn(ex.Message);
                return false;
            }
        }
    }
}
