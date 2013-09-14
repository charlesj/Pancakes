namespace Pancakes.Mapping
{
    using AutoMapper;

    /// <summary>
    /// The auto mapper mapping service.  This provides a generic and abstract way of mapping one object to another. 
    /// </summary>
    public class AutoMapperMappingService : IMappingService
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
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : class
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}