//  <copyright file="EmbeddedResourceSet.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Collections.Generic;
using System.Reflection;
using CodeZero.Extensions;
using CodeZero.IO.Extensions;

namespace CodeZero.Resources.Embedded
{
    public class EmbeddedResourceSet
    {
        public string RootPath { get; }

        public Assembly Assembly { get; }

        public string ResourceNamespace { get; }

        public EmbeddedResourceSet(string rootPath, Assembly assembly, string resourceNamespace)
        {
            RootPath = rootPath.EnsureEndsWith('/');
            Assembly = assembly;
            ResourceNamespace = resourceNamespace;
        }

        internal void AddResources(Dictionary<string, EmbeddedResourceItem> resources)
        {
            foreach (var resourceName in Assembly.GetManifestResourceNames())
            {
                if (!resourceName.StartsWith(ResourceNamespace))
                {
                    continue;
                }

                using (var stream = Assembly.GetManifestResourceStream(resourceName))
                {
                    var relativePath = ConvertToRelativePath(resourceName);
                    var filePath = EmbeddedResourcePathHelper.NormalizePath(RootPath) + relativePath;

                    resources[filePath] = new EmbeddedResourceItem(
                        filePath,
                        stream.GetAllBytes(),
                        Assembly
                    );
                }
            }
        }

        private string ConvertToRelativePath(string resourceName)
        {
            return resourceName.Substring(ResourceNamespace.Length + 1);
        }
    }
}