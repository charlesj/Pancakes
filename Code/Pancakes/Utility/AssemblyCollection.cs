using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pancakes.Utility
{
    public class AssemblyCollection : List<Assembly>
    {
        public virtual IEnumerable<Type> GetTypesImplementing(Type type)
        {
            return this.SelectMany(ass => ass.ExportedTypes)
                .Where(t => type != t)
                .Where(type.IsAssignableFrom);
        }
    }
}
