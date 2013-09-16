namespace Pancakes.Validation
{
    /// <summary>
    /// The ValidateThings interface.
    /// </summary>
    public interface IValidateThings
    {
        /// <summary>
        /// Checks if the object is valid, and if not, throws the validation exception.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// The type of the obejct to check.
        /// </typeparam>
        /// <exception cref="ValidationException">
        /// Thrown if the object does not pass muster.
        /// </exception>
        void CheckValidationAndThrow<T>(T obj);

        /// <summary>
        /// Checks if the object is valid and returns the result.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// The type of the object to check validation.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Result"/>.
        /// </returns>
        Result CheckValidation<T>(T obj);
    }
}