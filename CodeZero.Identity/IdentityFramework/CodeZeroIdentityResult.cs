//  <copyright file="CodeZeroIdentityResult.cs" project="CodeZero.Identity" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace CodeZero.IdentityFramework
{
    public class CodeZeroIdentityResult : IdentityResult
    {
        public CodeZeroIdentityResult()
        {
            
        }

        public CodeZeroIdentityResult(IEnumerable<string> errors)
            : base(errors)
        {
            
        }

        public CodeZeroIdentityResult(params string[] errors)
            :base(errors)
        {
            
        }

        public static CodeZeroIdentityResult Failed(params string[] errors)
        {
            return new CodeZeroIdentityResult(errors);
        }
    }
}