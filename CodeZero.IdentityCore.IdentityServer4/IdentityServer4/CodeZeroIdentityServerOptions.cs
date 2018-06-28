//  <copyright file="CodeZeroIdentityServerOptions.cs" project="CodeZero.IdentityCore.IdentityServer4" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.IdentityModel.Tokens.Jwt;
using CodeZero.Runtime.Security;

namespace CodeZero.IdentityServer4
{
    public class CodeZeroIdentityServerOptions
    {
        /// <summary>
        /// Updates <see cref="JwtSecurityTokenHandler.DefaultInboundClaimTypeMap"/> to be compatible with identity server claims.
        /// Default: true.
        /// </summary>
        public bool UpdateJwtSecurityTokenHandlerDefaultInboundClaimTypeMap { get; set; } = true;

        /// <summary>
        /// Updates <see cref="CodeZeroClaimTypes"/> to be compatible with identity server claims.
        /// Default: true.
        /// </summary>
        public bool UpdateCodeZeroClaimTypes { get; set; } = true;
    }
}