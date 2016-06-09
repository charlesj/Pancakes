using System;
using System.Threading.Tasks;
using Pancakes.Commands;
using Pancakes.Tests.TestUtilities;
using Xunit;

namespace Pancakes.Tests.CommandTests
{
    public class CommandSerializerTests : BaseUnitTest<CommandSerializer>
    {
        const string serialization = "{\"Name\":\"Fozzy\",\"DateCreated\":\"2015-10-31T00:00:00\"}";

        [Fact]
        public void CanSerializeIntoJson()
        {
            var command = new SerializeCommand {Name = "Fozzy", DateCreated = new DateTime(2015, 10, 31)};
            var result = this.SystemUnderTest.Serialize(command);    
            Assert.Equal(serialization, result);
        }

        [Fact]
        public void CanDeserialize()
        {
            var command = new SerializeCommand();
            this.SystemUnderTest.DeserializeInto(serialization, command);
            Assert.Equal("Fozzy", command.Name);
            Assert.Equal(new DateTime(2015, 10, 31), command.DateCreated);
        }

        public class SerializeCommand : ICommand
        {
            public string Name { get; set; }
            public DateTime DateCreated { get; set; }

            public Task<bool> AuthorizeAsync()
            {
                return Task.FromResult(true);
            }

            public Task<bool> ValidateAsync()
            {
                return Task.FromResult(false);
            }

            public Task ExecuteAsync()
            {
                return Task.FromResult(0);
            }
        }
    }
}
