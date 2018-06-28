//  <copyright file="AlertList.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;

namespace CodeZero.Web.Mvc.Alerts
{
    public class AlertList : List<AlertMessage>
    {
        public void Add(AlertType type, string text, string title = null, bool dismissible = true)
        {
            Add(new AlertMessage(type, text, title, dismissible));
        }

        public void Info(string text, string title = null, bool dismissible = true)
        {
            Add(new AlertMessage(AlertType.Info, text, title, dismissible));
        }

        public void Warning(string text, string title = null, bool dismissible = true)
        {
            Add(new AlertMessage(AlertType.Warning, text, title, dismissible));
        }

        public void Danger(string text, string title = null, bool dismissible = true)
        {
            Add(new AlertMessage(AlertType.Danger, text, title, dismissible));
        }

        public void Success(string text, string title = null, bool dismissible = true)
        {
            Add(new AlertMessage(AlertType.Success, text, title, dismissible));
        }
    }
}