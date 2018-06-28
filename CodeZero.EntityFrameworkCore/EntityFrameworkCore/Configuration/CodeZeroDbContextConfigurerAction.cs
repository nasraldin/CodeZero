//  <copyright file="CodeZeroDbContextConfigurerAction.cs" project="CodeZero.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using Microsoft.EntityFrameworkCore;

namespace CodeZero.EntityFrameworkCore.Configuration
{
    public class CodeZeroDbContextConfigurerAction<TDbContext> : ICodeZeroDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        public Action<CodeZeroDbContextConfiguration<TDbContext>> Action { get; set; }

        public CodeZeroDbContextConfigurerAction(Action<CodeZeroDbContextConfiguration<TDbContext>> action)
        {
            Action = action;
        }

        public void Configure(CodeZeroDbContextConfiguration<TDbContext> configuration)
        {
            Action(configuration);
        }
    }
}