namespace Pancakes.Validation
{
    /// <summary>
    /// The Validator interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type that this is a validator for
    /// </typeparam>
    public interface IValidate<in T>
    {
        /// <summary>
        /// Validates the passed object.
        /// </summary>
        /// <param name="validationTarget">
        /// The validation target.
        /// </param>
        /// <returns>
        /// The result of the validation attempt
        /// </returns>
        Result Check(T validationTarget);
    }
}