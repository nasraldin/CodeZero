//  <copyright file="ISettingClientVisibilityProvider.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Threading.Tasks;
using CodeZero.Dependency;

namespace CodeZero.Configuration
{
    public interface ISettingClientVisibilityProvider
    {
        Task<bool> CheckVisible(IScopedIocResolver scope);
    }
}