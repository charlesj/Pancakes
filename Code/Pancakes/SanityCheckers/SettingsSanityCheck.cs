// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsSanityCheck.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   Checks the sanity of the settings objects.  Takes all available implementations of ISettings and
//   makes sure their properties has values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.SanityCheckers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Pancakes.Settings;

	/// <summary>
	/// Checks the sanity of the settings objects.  Takes all available implementations of ISettings and
	/// makes sure their properties has values.
	/// </summary>
	public class SettingsSanityCheck : ICheckKernelSanity
	{
		/// <summary>
		/// Runs the check against settings objects.
		/// </summary>
		/// <param name="kernel">
		/// The kernel.
		/// </param>
		public void Check(IKernel kernel)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var settingsInterfaces = new List<Type>();
			foreach (var assembly in assemblies)
			{
				settingsInterfaces.AddRange(assembly.GetTypes().Where(type => type.IsInterface && type.IsAssignableFrom(typeof(ISettings))));
			}

			foreach (var settingsInterface in settingsInterfaces)
			{
				var settings = (ISettings)kernel.ServiceLocater.GetService(settingsInterface);
				settings.CheckAllSettingForValues();
			}

			kernel.WriteIfVerbose("Settings implementations seem to be sane.");
		}
	}
}