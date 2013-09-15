namespace Pancakes.Validation
{
    using FluentValidation;
    using FluentValidation.Results;

    using Pancakes.Mapping;

    /// <summary>
    /// The base validator.
    /// </summary>
    /// <typeparam name="T">
    /// The type to validate
    /// </typeparam>
    public abstract class BaseValidator<T> : AbstractValidator<T>, IValidate<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseValidator{T}"/> class.
        /// </summary>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        /// <remarks>
        /// The constructor on the inherited object should have the validation rules.  Since we're using fluent validation,
        /// the rules should follow that library.
        /// </remarks>
        /// <code>
        /// RuleFor(object => object.Property).NotEmpty().WithMessage("Property must be included");
        /// </code>
        protected BaseValidator(IMappingService mapper)
        {
            this.Mapper = mapper;
        }

        /// <summary>
        /// Gets or sets the mapper.
        /// </summary>
        protected IMappingService Mapper { get; set; }

        /// <summary>
        /// Validates the passed object.
        /// </summary>
        /// <param name="validationTarget">
        /// The validation target.
        /// </param>
        /// <returns>
        /// The result of the validation attempt
        /// </returns>
        public Result Check(T validationTarget)
        {
            var result = this.Validate(validationTarget);
            return this.Mapper.Map<ValidationResult, Result>(result);
        }
    }
}