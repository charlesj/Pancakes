namespace Pancakes.Tests
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;

	using Pancakes.Exceptions;

	using Xunit;

	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
	public class StaticBootTests
	{
		[Fact]
		public void TryingToAccessBootedKernelBeforeBootThrowsException()
		{
			var setup = this.SetupTestingDomainWithAssembly("Pancakes.dll");
			var pancakesAssembly = setup.Item2;
			var kernelType = pancakesAssembly.GetType("Pancakes.Kernel");
			var bootedkernelProperty = kernelType.GetProperty("BootedKernel");
			try
			{
				bootedkernelProperty.GetValue(null);
			}
			catch (Exception e)
			{
				Assert.IsType(typeof(PancakeException), e.InnerException);
			}

			AppDomain.Unload(setup.Item1);
		}

		[Fact]
		public void CanAccessKernelAfterBooting()
		{
			var setup = this.SetupTestingDomainWithAssembly("Pancakes.dll");
			var kernelType = setup.Item2.GetType("Pancakes.Kernel");
			var bootMethod = kernelType.GetMethod("Boot");
			bootMethod.Invoke(null, new object[] { BootConfiguration.DefaultConfiguration });
			var bootedkernelProperty = kernelType.GetProperty("BootedKernel");

			Assert.DoesNotThrow(() => bootedkernelProperty.GetValue(null));

			AppDomain.Unload(setup.Item1);
		}

		[Fact]
		public void BootIsIdempotent()
		{
			var setup = this.SetupTestingDomainWithAssembly("Pancakes.dll");
			var kernelType = setup.Item2.GetType("Pancakes.Kernel");
			var bootMethod = kernelType.GetMethod("Boot");
			bootMethod.Invoke(null, new object[] { BootConfiguration.DefaultConfiguration });
			var bootedkernelProperty = kernelType.GetProperty("BootedKernel");

			var bootedKernel = (Kernel)bootedkernelProperty.GetValue(null);

			bootMethod.Invoke(null, new object[] { BootConfiguration.DefaultConfiguration });

			var secondCall = (Kernel)bootedkernelProperty.GetValue(null);

			Assert.Equal(bootedKernel, secondCall);

			AppDomain.Unload(setup.Item1);
		}

		private Tuple<AppDomain, Assembly> SetupTestingDomainWithAssembly(string assemblyPath)
		{
			// we guarantee that each domain will have a unique name.
			AppDomain testingDomain = AppDomain.CreateDomain(DateTime.Now.Ticks.ToString());
			var pancakesAssemblyName = new AssemblyName();
			pancakesAssemblyName.CodeBase = assemblyPath;
			var assembly = testingDomain.Load(pancakesAssemblyName);

			return new Tuple<AppDomain, Assembly>(testingDomain, assembly);
		}
	}
}