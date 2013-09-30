namespace Pancakes.StaticTests
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	using Pancakes.Exceptions;
	using Pancakes.ServiceLocater;

	using Xunit;

	/// <summary>
	/// The purpose of this program is to test the static singleton of the core boot process.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The entry point
		/// </summary>
		public static void Main()
		{
			// TryingToAccessBootedKernelBeforeBootThrowsException
			Assert.Throws<PancakeException>(() => Bootstrapper.BootedKernel);
			
			Console.WriteLine("Exception was properly thrown");

			// CanAccessKernelAfterBooting 
			Bootstrapper.Boot(BootConfiguration.DefaultConfiguration);
			IKernel booted = new TestingKernel();
			Assert.DoesNotThrow(() => booted = Bootstrapper.BootedKernel);
			Console.WriteLine("Can boot using default configuration and access booted kernel.");

			// BootIsIdempotent
			Bootstrapper.Boot(BootConfiguration.DefaultConfiguration);
			var secondKernel = Bootstrapper.BootedKernel;

			Assert.Equal(booted, secondKernel);

			Console.WriteLine("The boot method is idempotent");
			Console.ReadLine();
		}

		[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
		internal class TestingKernel : IKernel
		{
			public IServiceLocater ServiceLocater { get; private set; }

			public void CheckSanity()
			{
				throw new System.NotImplementedException();
			}

			public void WriteIfVerbose(string format, params object[] args)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}
