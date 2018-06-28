//  <copyright file="ICodeZeroAntiForgeryValidator.cs" project="CodeZero.Web.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Web.Security.AntiForgery
{
    /// <summary>
    /// This interface is internally used by CodeZero framework and normally should not be used by applications.
    /// If it's needed, use 
    /// <see cref="ICodeZeroAntiForgeryManager"/> and cast to 
    /// <see cref="ICodeZeroAntiForgeryValidator"/> to use 
    /// <see cref="IsValid"/> method.
    /// </summary>
    public interface ICodeZeroAntiForgeryValidator
    {
        bool IsValid(string cookieValue, string tokenValue);
    }
}