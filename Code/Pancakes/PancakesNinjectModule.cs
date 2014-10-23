namespace Pancakes
{
	using Ninject.Modules;

	using Pancakes.Commands;
	using Pancakes.Logging;
	using Pancakes.Mapping;
	using Pancakes.Settings;
	using Pancakes.Validation;

	/// <summary>
	/// The pancakes ninject module.  Contains the bindings for the internal interfaces.
	/// </summary>
	public class PancakesNinjectModule : NinjectModule
	{
		/// <summary>
		/// Binds the internal pancakes types to their implementations.
		/// </summary>
		public override void Load()
		{
			this.Bind<ITypeConverter>().To<TypeConverter>();
			this.Bind<ISettings>().To<ReflectiveSettings>();
			this.Bind<ICommandLocator>().To<CommandLocator>();
			this.Bind<IMappingService>().To<AutoMapperMappingService>();
			this.Bind<IValidateThings>().To<ValidateThings>();
			this.Bind<ILogger>().To<NLogLogger>();
		}
	}
}