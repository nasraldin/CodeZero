//  <copyright file="CodeZeroLocalizationController.cs" project="CodeZero.Web.Mvc" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using CodeZero.Auditing;
using CodeZero.Configuration;
using CodeZero.Localization;
using CodeZero.Runtime.Session;
using CodeZero.Timing;
using CodeZero.Web.Configuration;
using CodeZero.Web.Models;

namespace CodeZero.Web.Mvc.Controllers.Localization
{
    public class CodeZeroLocalizationController : CodeZeroController
    {
        private readonly ICodeZeroWebLocalizationConfiguration _webLocalizationConfiguration;

        public CodeZeroLocalizationController(ICodeZeroWebLocalizationConfiguration webLocalizationConfiguration)
        {
            _webLocalizationConfiguration = webLocalizationConfiguration;
        }

        [DisableAuditing]
        public virtual ActionResult ChangeCulture(string cultureName, string returnUrl = "")
        {
            if (!GlobalizationHelper.IsValidCultureCode(cultureName))
            {
                throw new CodeZeroException("Unknown language: " + cultureName + ". It must be a valid culture!");
            }

            Response.Cookies.Add(
                new HttpCookie(_webLocalizationConfiguration.CookieName, cultureName)
                {
                    Expires = Clock.Now.AddYears(2),
                    Path = Request.ApplicationPath
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
                return Json(new AjaxResponse(), JsonRequestBehavior.AllowGet);
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && Request.Url != null && CodeZeroUrlHelper.IsLocalUrl(Request.Url, returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(Request.ApplicationPath);
        }
    }
}
