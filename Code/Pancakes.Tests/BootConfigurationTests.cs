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
	}
}
