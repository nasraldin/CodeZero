//  <copyright file="PermissionGrantInfo.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Authorization
{
    /// <summary>
    /// Represents a permission <see cref="Name"/> with <see cref="IsGranted"/> information.
    /// </summary>
    public class PermissionGrantInfo
    {
        /// <summary>
        /// Name of the permission.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Is this permission granted Prohibited?
        /// </summary>
        public bool IsGranted { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="PermissionGrantInfo"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isGranted"></param>
        public PermissionGrantInfo(string name, bool isGranted)
        {
            Name = name;
            IsGranted = isGranted;
        }
    }
}