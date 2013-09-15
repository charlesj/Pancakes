namespace Pancakes.Tests.MappingTests
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Pancakes.Mapping;

    using Xunit;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Testing Class. No Documentation Required")]
    public class MappingServiceTests
    {
        [Fact]
        public void CanMapFromFootballToSoccer()
        {
            var config = new SportMappingConfiguration();
            config.Configure();

            var mapper = new AutoMapperMappingService();

            var soccer = new SoccerPlayer { Name = "Fozzy" };

            var mapped = mapper.Map<SoccerPlayer, FootballPlayer>(soccer);

            Assert.Equal("Fozzy", mapped.Name);
        }

        private class FootballPlayer
        {
            public string Name { get; set; }
        }

        private class SoccerPlayer
        {
            public string Name { get; set; }
        }

        private class SportMappingConfiguration : IMappingConfiguration
        {
            public void Configure()
            {
                Mapper.CreateMap<FootballPlayer, SoccerPlayer>();
                Mapper.CreateMap<SoccerPlayer, FootballPlayer>();
            }
        }
    }
}
