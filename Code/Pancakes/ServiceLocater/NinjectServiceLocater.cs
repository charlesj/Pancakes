namespace Pancakes.ServiceLocater
{
	using System;
	using System.Linq;

	using Ninject;

	/// <summary>
	/// Implementation of the service lcoater using Ninject, with some configuration options.
	/// </summary>
	public class NinjectServiceLocater : IServiceLocater
	{
		/// <summary>
		/// The kernel.
		/// </summary>
		private readonly IKernel kernel;

		/// <summary>
		/// Initializes a new instance of the <see cref="NinjectServiceLocater"/> class.
		/// </summary>
		/// <param name="assemblySearchPatterns">
		/// The assembly search patterns are used to search for ninject modules.  If no patterns are passed, all .dlls' are searched.
		/// </param>
		public NinjectServiceLocater(params string[] assemblySearchPatterns)
		{
			this.kernel = new StandardKernel();
			if (assemblySearchPatterns.Any())
			{
				foreach (var pattern in assemblySearchPatterns)
				{
					this.kernel.Load(pattern);
				}
			}
			else
			{
				this.kernel.Load("*.dll");
			}
		}

		/// <summary>
		/// The get service.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public object GetService(Type type)
		{
			return this.kernel.Get(type);
		}

		/// <summary>
		/// The get service.
		/// </summary>
		/// <typeparam name="TServiceType">
		/// The type of the service to locate.
		/// </typeparam>
		/// <returns>
		/// The <see cref="TServiceType"/>.
		/// </returns>
		public TServiceType GetService<TServiceType>()
		{
			return this.kernel.Get<TServiceType>();
		}
	}
}