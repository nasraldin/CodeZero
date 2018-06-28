//  <copyright file="AlertMessage.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using JetBrains.Annotations;

namespace CodeZero.Web.Mvc.Alerts
{
    public class AlertMessage
    {
        [NotNull]
        public string Text
        {
            get => _text;
            set => _text = Check.NotNullOrWhiteSpace(value, nameof(value));
        }
        private string _text;

        public AlertType Type { get; set; }

        [CanBeNull]
        public string Title { get; set; }

        public bool Dismissible { get; set; }

        public AlertMessage(AlertType type, [NotNull] string text, string title = null, bool dismissible = true)
        {
            Type = type;
            Text = Check.NotNullOrWhiteSpace(text, nameof(text));
            Title = title;
            Dismissible = dismissible;
        }
    }
}