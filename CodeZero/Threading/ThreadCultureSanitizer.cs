//  <copyright file="ThreadCultureSanitizer.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Globalization;
using System.Threading;

namespace CodeZero.Threading
{
    /// <summary>
    /// This class is copied from here:
    /// http://www.zpqrtbnk.net/posts/appdomains-threads-cultureinfos-and-paracetamol
    /// It's a workaround for application startup problem.
    /// </summary>
    public static class ThreadCultureSanitizer
    {
        public static void Sanitize()
        {
            var currentCulture = CultureInfo.CurrentCulture;

            // at the top of any culture should be the invariant culture,
            // find it doing an .Equals comparison ensure that we will
            // find it and not loop endlessly
            var invariantCulture = currentCulture;
            while (invariantCulture.Equals(CultureInfo.InvariantCulture) == false)
            {
                invariantCulture = invariantCulture.Parent;
            }

            if (ReferenceEquals(invariantCulture, CultureInfo.InvariantCulture))
            {
                return;
            }

            var thread = Thread.CurrentThread;
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(thread.CurrentCulture.Name);
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(thread.CurrentUICulture.Name);
        }
    }
}
