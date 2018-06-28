//  <copyright file="NotificationConfiguration.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Collections;

namespace CodeZero.Notifications
{
    internal class NotificationConfiguration : INotificationConfiguration
    {
        public ITypeList<NotificationProvider> Providers { get; private set; }

        public NotificationConfiguration()
        {
            Providers = new TypeList<NotificationProvider>();
        }
    }
}