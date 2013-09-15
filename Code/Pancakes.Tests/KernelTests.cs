namespace Pancakes.Tests
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;

	using Xunit;

	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
	public class KernelTests
	{
		[Fact]
		public void CanBootWithDefaultConfiguration()
		{
			Assert.DoesNotThrow(() => new Kernel(BootConfiguration.DefaultConfiguration));
		}

		[Fact]
		public void BootingWithVerbosityWritesToStandardOut()
		{
			var memoryStreamWriter = new StringWriter();
			Console.SetOut(memoryStreamWriter);
			new Kernel(BootConfiguration.DefaultConfiguration.BeVerbose());
			var text = memoryStreamWriter.ToString();
			Assert.True(text.Contains("Booting..."));
		}

		[Fact]
		public void BootingWithVerbosityOffDoesNotWriteToStandardOut()
		{
			var memoryStreamWriter = new StringWriter();
			Console.SetOut(memoryStreamWriter);
			new Kernel(BootConfiguration.DefaultConfiguration);
			var text = memoryStreamWriter.ToString();
			Assert.Empty(text);
		}
	}
}
