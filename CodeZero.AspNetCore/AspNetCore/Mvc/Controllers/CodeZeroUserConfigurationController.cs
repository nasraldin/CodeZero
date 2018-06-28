//  <copyright file="CodeZeroUserConfigurationController.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Web.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CodeZero.AspNetCore.Mvc.Controllers
{
    public class CodeZeroUserConfigurationController: CodeZeroController
    {
        private readonly CodeZeroUserConfigurationBuilder _CodeZeroUserConfigurationBuilder;

        public CodeZeroUserConfigurationController(CodeZeroUserConfigurationBuilder CodeZeroUserConfigurationBuilder)
        {
            _CodeZeroUserConfigurationBuilder = CodeZeroUserConfigurationBuilder;
        }

        public async Task<JsonResult> GetAll()
        {
            var userConfig = await _CodeZeroUserConfigurationBuilder.GetAll();
            return Json(userConfig);
        }
    }
}