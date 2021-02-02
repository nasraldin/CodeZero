using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CodeZero.Shared.Reflection
{
    public class TypeFinder : ITypeFinder
    {
        private readonly IAssemblyFinder _assemblyFinder;

        private readonly Lazy<IReadOnlyList<Type>> _types;

        public TypeFinder(IAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;

            _types = new Lazy<IReadOnlyList<Type>>(FindAll, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IReadOnlyList<Type> Types => _types.Value;

        private IReadOnlyList<Type> FindAll()
        {
            var allTypes = new List<Type>();

            foreach (var assembly in _assemblyFinder.Assemblies)
            {
                try
                {
                    var typesInThisAssembly = AssemblyHelper.GetAllTypes(assembly);

                    if (!typesInThisAssembly.Any())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    //TODO: Trigger a global event?
                    throw new Exception("Error, all types assembly finder. Reason: {0}", ex);
                }
            }

            return allTypes;
        }
    }

}
