//  <copyright file="MultiTenantLocalizationDictionaryCacheCleaner.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Dependency;
using CodeZero.Events.Bus.Entities;
using CodeZero.Events.Bus.Handlers;
using CodeZero.Runtime.Caching;

namespace CodeZero.Localization
{
    /// <summary>
    /// Clears related localization cache when a <see cref="ApplicationLanguageText"/> changes.
    /// </summary>
    public class MultiTenantLocalizationDictionaryCacheCleaner : 
        ITransientDependency,
        IEventHandler<EntityChangedEventData<ApplicationLanguageText>>
    {
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenantLocalizationDictionaryCacheCleaner"/> class.
        /// </summary>
        public MultiTenantLocalizationDictionaryCacheCleaner(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(EntityChangedEventData<ApplicationLanguageText> eventData)
        {
            _cacheManager
                .GetMultiTenantLocalizationDictionaryCache()
                .Remove(MultiTenantLocalizationDictionaryCacheHelper.CalculateCacheKey(
                    eventData.Entity.TenantId,
                    eventData.Entity.Source,
                    eventData.Entity.LanguageName)
                );
        }
    }
}