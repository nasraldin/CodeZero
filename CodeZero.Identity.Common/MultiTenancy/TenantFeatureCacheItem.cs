//  <copyright file="TenantFeatureCacheItem.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Collections.Generic;

namespace CodeZero.MultiTenancy
{
    /// <summary>
    /// Used to store features of a Tenant in the cache.
    /// </summary>
    [Serializable]
    public class TenantFeatureCacheItem
    {
        /// <summary>
        /// The cache store name.
        /// </summary>
        public const string CacheStoreName = "CodeZeroTenantFeatures";

        /// <summary>
        /// Edition of the tenant.
        /// </summary>
        public int? EditionId { get; set; }

        /// <summary>
        /// Feature values.
        /// </summary>
        public IDictionary<string, string> FeatureValues { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantFeatureCacheItem"/> class.
        /// </summary>
        public TenantFeatureCacheItem()
        {
            FeatureValues = new Dictionary<string, string>();
        }
    }
}