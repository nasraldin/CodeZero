//  <copyright file="CodeZeroRedisCacheOptions.cs" project="CodeZero.RedisCache" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Configuration.Startup;
using CodeZero.Extensions;
using System.Configuration;

namespace CodeZero.Runtime.Caching.Redis
{
    public class CodeZeroRedisCacheOptions
    {
        public ICodeZeroStartupConfiguration CodeZeroStartupConfiguration { get; }

        private const string ConnectionStringKey = "CodeZero.Redis.Cache";

        private const string DatabaseIdSettingKey = "CodeZero.Redis.Cache.DatabaseId";

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        public CodeZeroRedisCacheOptions(ICodeZeroStartupConfiguration codeZeroStartupConfiguration)
        {
            CodeZeroStartupConfiguration = codeZeroStartupConfiguration;

            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
        }

        private static int GetDefaultDatabaseId()
        {
            var appSetting = ConfigurationManager.AppSettings[DatabaseIdSettingKey];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }

            if (!int.TryParse(appSetting, out var databaseId))
            {
                return -1;
            }

            return databaseId;
        }

        private static string GetDefaultConnectionString()
        {
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringKey];
            if (connStr == null || connStr.ConnectionString.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr.ConnectionString;
        }
    }
}