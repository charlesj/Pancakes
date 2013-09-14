namespace Pancakes.Mapping
{
    /// <summary>
    /// IMappingService encapsulates object to object conversions.
    /// </summary>
    public interface IMappingService
    {
        /// <summary>
        /// Performs a conversion from one type to another type in a strongly typed manner.
        /// </summary>
        /// <param name="source">
        /// The source object.
        /// </param>
        /// <typeparam name="TSource">
        /// The source object type.
        /// </typeparam>
        /// <typeparam name="TDestination">
        /// The destination object type.
        /// </typeparam>
        /// <returns>
        /// The specified destination type.
        /// </returns>
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : class;
    }
}