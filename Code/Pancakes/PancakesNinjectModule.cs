// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PancakesNinjectModule.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   The pancakes ninject module.  Contains the bindings for the internal interfaces.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes
{
	using Ninject.Modules;

	using Pancakes.Settings;

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
		}
	}
}