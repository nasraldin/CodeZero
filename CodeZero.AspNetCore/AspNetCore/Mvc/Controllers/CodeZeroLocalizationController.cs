//  <copyright file="CodeZeroLocalizationController.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.AspNetCore.Mvc.Extensions;
using CodeZero.Auditing;
using CodeZero.Configuration;
using CodeZero.Localization;
using CodeZero.Runtime.Session;
using CodeZero.Timing;
using CodeZero.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace CodeZero.AspNetCore.Mvc.Controllers
{
    public class CodeZeroLocalizationController : CodeZeroController
    {
        [DisableAuditing]
        public virtual ActionResult ChangeCulture(string cultureName, string returnUrl = "")
        {
            if (!GlobalizationHelper.IsValidCultureCode(cultureName))
            {
                throw new CodeZeroException("Unknown language: " + cultureName + ". It must be a valid culture!");
            }

            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName, cultureName));

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                cookieValue,
                new CookieOptions
                {
                    Expires = Clock.Now.AddYears(2),
                    HttpOnly = true 
                }
            );

            if (CodeZeroSession.UserId.HasValue)
            {
                SettingManager.ChangeSettingForUser(
                    CodeZeroSession.ToUserIdentifier(),
                    LocalizationSettingNames.DefaultLanguage,
                    cultureName
                );
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new AjaxResponse());
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && CodeZeroUrlHelper.IsLocalUrl(Request, returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/"); //TODO: Go to app root
        }
    }
}
