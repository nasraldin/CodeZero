//  <copyright file="CodeZeroIdentityCoreServerModule.cs" project="CodeZero.IdentityCore.IdentityServer4" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.AutoMapper;
using CodeZero.Identity;
using CodeZero.Modules;
using CodeZero.Reflection.Extensions;
using IdentityServer4.Models;

namespace CodeZero.IdentityServer4
{
    [DependsOn(typeof(CodeZeroIdentityCoreModule), typeof(CodeZeroAutoMapperModule))]
    public class CodeZeroIdentityCoreServerModule : CodeZeroModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.CodeZeroAutoMapper().Configurators.Add(config =>
            {
                //PersistedGrant -> PersistedGrantEntity
                config.CreateMap<PersistedGrant, PersistedGrantEntity>()
                    .ForMember(d => d.Id, c => c.MapFrom(s => s.Key));

                //PersistedGrantEntity -> PersistedGrant
                config.CreateMap<PersistedGrantEntity, PersistedGrant>()
                    .ForMember(d => d.Key, c => c.MapFrom(s => s.Id));
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CodeZeroIdentityCoreServerModule).GetAssembly());
        }
    }
}
