//  <copyright file="ICodeZeroDbContextConfigurer.cs" project="CodeZero.EntityFrameworkCore" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Microsoft.EntityFrameworkCore;

namespace CodeZero.EntityFrameworkCore.Configuration
{
    public interface ICodeZeroDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        void Configure(CodeZeroDbContextConfiguration<TDbContext> configuration);
    }
}