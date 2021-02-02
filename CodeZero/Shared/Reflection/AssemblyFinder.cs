using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace CodeZero.Shared.Reflection
{
    public class AssemblyFinder : IAssemblyFinder
    {
        private readonly Lazy<IReadOnlyList<Assembly>> _assemblies;

        public AssemblyFinder()
        {
            _assemblies = new Lazy<IReadOnlyList<Assembly>>(FindAll, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IReadOnlyList<Assembly> Assemblies => _assemblies.Value;

        public IReadOnlyList<Assembly> FindAll()
        {
            var assemblies = new List<Assembly>();
            return assemblies.Distinct().ToImmutableList();
        }
    }
}
