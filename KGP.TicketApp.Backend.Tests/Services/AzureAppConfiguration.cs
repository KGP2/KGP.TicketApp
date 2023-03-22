using FluentAssertions;
using KGP.TicketApp.Backend.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;

namespace KGP.TicketApp.Backend.Tests.Services
{
    public class AzureAppConfiguration
    {
        private WebApplicationFactory<Program> webAppFactory;

        [SetUp]
        public void Setup()
        {
            webAppFactory = new WebApplicationFactory<Program>();
        }

        [Test]
        public void AzureConfiguration_ShouldConnect()
        {
            var service = webAppFactory
                .Services
                .GetService(typeof(IOptions<ApplicationOptions>));

            Assert.Pass();
        }

        [Test]
        public void AzureConfiguration_ShouldNotBeNull()
        {
            var service = webAppFactory
                .Services
                .GetService(typeof(IOptions<ApplicationOptions>));
            
            service.Should().NotBeNull();
        }

        [Test]
        public void AzureConfiguration_ShouldHaveEveryField()
        {
            var service = webAppFactory
                .Services
                .GetService(typeof(IOptions<ApplicationOptions>)) as IOptions<ApplicationOptions>;

            var options = service!.Value!;

            var properties = options.GetType()
                .GetProperties()
                .Select(prop => prop.GetValue(options))
                .ToList();
            
            properties.Should().AllSatisfy(prop => prop.Should().NotBeNull());
        }
    }
}
