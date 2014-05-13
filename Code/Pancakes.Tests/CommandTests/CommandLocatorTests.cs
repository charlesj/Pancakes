namespace Pancakes.Tests.CommandTests
{
    using System.Diagnostics.CodeAnalysis;

    using Pancakes.Commands;

    using Xunit;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class CommandLocatorTests
    {
        [Fact]
        public void CanInstantiate()
        {
            var locator = new CommandLocator();
            Assert.NotNull(locator);
        }

        [Fact]
        public void ThrowsCommandNotFoundExceptionIfCannotFind()
        {
            var locator = new CommandLocator();
            Assert.Throws<CommandNotFoundException>(() => locator.LocateCommand<string, string>());
        }
    }
}
