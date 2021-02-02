﻿using CodeZero.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CodeZero.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot BuildConfiguration(
            ConfigurationBuilderOptions options = null,
            Action<IConfigurationBuilder> builderAction = null)
        {
            options = options ?? new ConfigurationBuilderOptions();

            if (options.BasePath.IsNullOrEmpty())
            {
                options.BasePath = Directory.GetCurrentDirectory();
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(options.BasePath)
                .AddJsonFile(options.FileName + ".json", optional: true, reloadOnChange: true);

            if (!options.EnvironmentName.IsNullOrEmpty())
            {
                builder = builder.AddJsonFile($"{options.FileName}.{options.EnvironmentName}.json", optional: true, reloadOnChange: true);
            }

            if (options.EnvironmentName == "Development")
            {
                if (options.UserSecretsId != null)
                {
                    builder.AddUserSecrets(options.UserSecretsId);
                }
                else if (options.UserSecretsAssembly != null)
                {
                    builder.AddUserSecrets(options.UserSecretsAssembly, true);
                }
            }

            builder = builder.AddEnvironmentVariables(options.EnvironmentVariablesPrefix);

            if (options.CommandLineArgs != null)
            {
                builder = builder.AddCommandLine(options.CommandLineArgs);
            }

            builderAction?.Invoke(builder);

            return builder.Build();
        }
    }
}