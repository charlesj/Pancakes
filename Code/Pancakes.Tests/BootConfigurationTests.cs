// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootConfigurationTests.cs" company="Josh Charles">
//   Copyright (c) 2013 Josh Charles.  Released under the MIT license.
// </copyright>
// <summary>
//   Contains tests for BootConfigurations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Pancakes.Tests
{
	using System.Diagnostics.CodeAnalysis;

	using Xunit;

	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
	public class BootConfigurationTests
	{
		private readonly BootConfiguration systemUnderTest;

		public BootConfigurationTests()
		{
			this.systemUnderTest = new BootConfiguration();
		}

		[Fact]
		public void DefaultsSetCorrectly()
		{
			Assert.True(this.systemUnderTest.CheckSanity);
		}

		[Fact]
		public void SkipSanityCheckSetsPropertyToFalse()
		{
			var configuration = this.systemUnderTest.SkipSanityCheck();
			Assert.False(configuration.CheckSanity);
		}

		[Fact]
		public void CanAddAssemblySearchPatterns()
		{
			this.systemUnderTest.AddAssemblySearchPattern("Pan*.dll");
		}

		[Fact]
		public void AddedSearchPatternAppearsInSearchPatternList()
		{
			var pattern = "pan*.dll";
			this.systemUnderTest.AddAssemblySearchPattern(pattern);
			Assert.True(this.systemUnderTest.AssemblySearchPatterns.Contains(pattern));
		}

		[Fact]
		public void CanSetVerbosity()
		{
			Assert.False(this.systemUnderTest.Verbose);
			this.systemUnderTest.BeVerbose();
			Assert.True(this.systemUnderTest.Verbose);
		}
	}
}
