﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KernelTests.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   Tests for the bootstrapper itself.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Tests
{
	using System.Diagnostics.CodeAnalysis;

	using Xunit;

	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
	public class KernelTests
	{
		[Fact]
		public void CanBootWithDefaultConfiguration()
		{
			Assert.DoesNotThrow(() => Kernel.Boot(BootConfiguration.DefaultConfiguration));
		}

	}
}