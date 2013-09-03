﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceLocater.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   Defines a basic service locater.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.ServiceLocater
{
	using System;

	/// <summary>
	/// Defines a basic service locater.
	/// </summary>
	public interface IServiceLocater 
	{
		/// <summary>
		/// The get service.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		object GetService(Type type);

		/// <summary>
		/// The get service.
		/// </summary>
		/// <typeparam name="TServiceType">
		/// The type of the service to locate.
		/// </typeparam>
		/// <returns>
		/// The <see cref="TServiceType"/>.
		/// </returns>
		TServiceType GetService<TServiceType>();
	}
}