namespace Archimedes.Common.Tests.CommandTests
{
    using System;
    using System.Linq;

    using FluentValidation;

    using Ninject.Modules;

    using Pancakes;
    using Pancakes.Commands;
    using Pancakes.Mapping;
    using Pancakes.Validation;

    using Xunit;

    public class AddCommandTests
    {
        public AddCommandTests()
        {
            Bootstrapper.Boot(BootConfiguration.DefaultConfiguration);
        }

        [Fact]
        public void CanExecuteCommand()
        {
            var command = Bootstrapper.BootedKernel.ServiceLocator.GetService<AddCommand>();
            var request = new AddRequest { FirstNumber = 1, SecondNumber = 1 };
            var response = command.Execute(request);
            Assert.True(response.Result == 2);
        }

        [Fact]
        public void CanUseLocator()
        {
            var locator = new CommandLocator();
            var command = locator.FindCommand<AddRequest, int>();
            Assert.NotNull(command);
        }

        [Fact]
        public void CanUseHeadquarters()
        {
            var headquarters = Bootstrapper.BootedKernel.ServiceLocator.GetService<Headquarters>();
            var response = headquarters.Execute<AddRequest, int>(new AddRequest { FirstNumber = 2, SecondNumber = 2 });
            Assert.True(response.Result == 4);
        }

        [Fact]
        public void CanReadElapsed()
        {
            var headquarters = Bootstrapper.BootedKernel.ServiceLocator.GetService<Headquarters>();
            var response = headquarters.Execute<AddRequest, int>(new AddRequest { FirstNumber = 2, SecondNumber = 2 });
            Console.Write("Execution Time: {0}ms", response.ExecutionTime);
        }

        [Fact]
        public void InvalidRequestReturnsInvalidRequestTrue()
        {
            var command = Bootstrapper.BootedKernel.ServiceLocator.GetService<AddCommand>();
            var request = new AddRequest { FirstNumber = -1, SecondNumber = 1 }; // invalid because of the -1
            var response = command.Execute(request);
            Assert.Equal(response.ResultType, ResponseTypes.InvalidRequest);
        }

        [Fact]
        public void InvalidRequestReturnsValidationErrors()
        {
            var command = Bootstrapper.BootedKernel.ServiceLocator.GetService<AddCommand>();
            var request = new AddRequest { FirstNumber = -1, SecondNumber = 1 }; // invalid because of the -1
            var response = command.Execute(request);
            Assert.True(response.ValidationErrors.Any());
        }

        [Fact]
        public void UnauthorizedRequestReturnsUnauthorizedResponse()
        {
            var command = Bootstrapper.BootedKernel.ServiceLocator.GetService<AddCommand>();
            var request = new AddRequest { FirstNumber = 1, SecondNumber = 1 };
            command.AuthorizeOveride = false;
            var response = command.Execute(request);
            Assert.Equal(response.ResultType, ResponseTypes.Unauthorized);
        }
    }

    public class AddCommandNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<BaseCommand<AddRequest, int>>().To<AddCommand>();
        }
    }

    public class AddCommand : BaseCommand<AddRequest, int>
    {
        public bool AuthorizeOveride { get; set; }
        public AddCommand(IValidateThings valdiator)
            : base(valdiator)
        {
            AuthorizeOveride = true;
        }

        protected override int Work()
        {
            return this.Request.FirstNumber + this.Request.SecondNumber;
        }

        protected override bool Authorize()
        {
            return this.AuthorizeOveride;
        }
    }

    public class AddRequest : Request
    {
        public int FirstNumber { get; set; }

        public int SecondNumber { get; set; }
    }

    public class AddRequestValidator : BaseValidator<AddRequest>
    {
        public AddRequestValidator(IMappingService mapper)
            : base(mapper)
        {
            RuleFor(obj => obj.FirstNumber).GreaterThanOrEqualTo(0);
            RuleFor(obj => obj.SecondNumber).LessThanOrEqualTo(100);
        }
    }
}