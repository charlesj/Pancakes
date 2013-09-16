namespace Pancakes.Mapping
{
    using System;
    using System.Linq;

    /// <summary>
    /// The auto mapper configuration loader.
    /// </summary>
    public static class MappingConfigurationLoader
    {
        /// <summary>
        /// Uses reflection to find all of the implementations of IMappingConfiguration and runs configure on them.
        /// Should be ran *after* initializing ninject, because otherwise you cannot guarantee that all the required
        /// assemblies are loaded into the app domain.
        /// </summary>
        /// <remarks>
        /// A major simplifying assumption of this method is that all assemblies are allready loaded into the app domain.
        /// It will not load any others.
        /// </remarks>
        public static void LoadConfigurations()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IMappingConfiguration)));
                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type) as IMappingConfiguration;
                    if (instance != null)
                    {
                        instance.Configure();
                    }
                }
            }
        }
    }
}