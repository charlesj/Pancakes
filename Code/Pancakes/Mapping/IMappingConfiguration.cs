namespace Pancakes.Mapping
{
    /// <summary>
    /// The MappingConfiguration interface.
    /// </summary>
    public interface IMappingConfiguration
    {
        /// <summary>
        /// Configures the mapping.  
        /// </summary>
        /// <remarks>
        /// Configuration happens by looking through all the loaded assemblies and looking
        /// for classes that implement IMappingConfiguration.  It will then load those classes up and called Configure()
        /// on this.  Configure should do everything that needs to happen inorder for the map() function to work on the
        /// MappingService
        /// </remarks>
        void Configure();
    }
}