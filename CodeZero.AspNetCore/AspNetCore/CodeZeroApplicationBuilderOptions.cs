//  <copyright file="CodeZeroApplicationBuilderOptions.cs" project="CodeZero.AspNetCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.AspNetCore
{
    public class CodeZeroApplicationBuilderOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseCastleLoggerFactory { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseCodeZeroRequestLocalization { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseSecurityHeaders { get; set; }

        public CodeZeroApplicationBuilderOptions()
        {
            UseCastleLoggerFactory = true;
            UseCodeZeroRequestLocalization = true;
            UseSecurityHeaders = true;
        }
    }
}