namespace Pancakes.Tests.CommandTests
{
    using System.Diagnostics.CodeAnalysis;

    using Pancakes.Commands;
    using Pancakes.ServiceLocater;

    using Xunit;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class CommandLocatorTests
    {
        [Fact]
        public void CanInstantiate()
        {
            var locator = new CommandLocator(new NinjectServiceLocater());
            Assert.NotNull(locator);
        }

        [Fact]
        public void ThrowsCommandNotFoundExceptionIfCannotFind()
        {
            var locator = new CommandLocator(new NinjectServiceLocater());
            Assert.Throws<CommandNotFoundException>(() => locator.LocateCommand<string, string>());
        }
    }
}
