//  <copyright file="CodeZeroEfCoreServiceCollectionExtensions.cs" project="CodeZero.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using CodeZero.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeZero.EntityFrameworkCore
{
    public static class CodeZeroEfCoreServiceCollectionExtensions
    {
        public static void AddCodeZeroDbContext<TDbContext>(
            this IServiceCollection services,
            Action<CodeZeroDbContextConfiguration<TDbContext>> action)
            where TDbContext : DbContext
        {
            services.AddSingleton(
                typeof(ICodeZeroDbContextConfigurer<TDbContext>),
                new CodeZeroDbContextConfigurerAction<TDbContext>(action)
            );
        }
    }
}
