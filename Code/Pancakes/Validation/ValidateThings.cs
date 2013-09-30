namespace Pancakes.Validation
{
	using System;
	using System.Linq;

	using Pancakes.Mapping;

	/// <summary>
	/// The validate things.
	/// </summary>
	public class ValidateThings : IValidateThings
	{
		/// <summary>
		/// The mapping service.
		/// </summary>
		private readonly IMappingService mappingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidateThings"/> class.
		/// </summary>
		/// <param name="mappingService">
		/// The mapping service.
		/// </param>
		public ValidateThings(IMappingService mappingService)
		{
			this.mappingService = mappingService;
		}

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
		public void CheckValidationAndThrow<T>(T obj)
		{
			var result = this.CheckValidation(obj);
			if (!result.IsValid)
			{
				throw new ValidationException(result);
			}
		}

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
		public Result CheckValidation<T>(T obj)
		{
			var validator = this.LocateValidator<T>();
			return validator.Check(obj);
		}

		/// <summary>
		/// The locate validator.
		/// </summary>
		/// <typeparam name="T">
		/// The type of the validator to locate
		/// </typeparam>
		/// <returns>
		/// The validator for the object type.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// When there is some sort of problem.
		/// </exception>
		private IValidate<T> LocateValidator<T>()
		{            
			var argumentType = typeof(T);
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembly in assemblies)
			{
				if (assembly.GetTypes().Any(t => t.GetInterfaces().Contains(typeof(IValidate<T>))))
				{
					var type = assembly.GetTypes().First(t => t.GetInterfaces().Contains(typeof(IValidate<T>)));
					try
					{
						// TODO: Use service locator for this.
						var validator = Activator.CreateInstance(type, new object[] { this.mappingService }) as IValidate<T>;
						//var validator = Kernel.BootedKernel.ServiceLocater.GetService<IValidate<T>>();
						return validator;
					}
					catch (Exception e)
					{
						var constructorMessage =
							string.Format(
								"There was a problem constructing the validator for type {0}.  This is most likely "
								+ "caused by a validator that does not have the expected constructor signature. Check "
								+ "inner exception for more information.",
								argumentType.FullName);
						throw new InvalidOperationException(constructorMessage, e);
					}
				}
			}
			
			var message = string.Format("Could not locate a validator for type {0}", argumentType.FullName);
			throw new InvalidOperationException(message);
		}
	}
}