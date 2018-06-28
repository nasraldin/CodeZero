//  <copyright file="CodeZeroOwinOptions.cs" project="CodeZero.Owin" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Owin
{
    public class CodeZeroOwinOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseEmbeddedFiles { get; set; }

        public CodeZeroOwinOptions()
        {
            UseEmbeddedFiles = true;
        }
    }
}