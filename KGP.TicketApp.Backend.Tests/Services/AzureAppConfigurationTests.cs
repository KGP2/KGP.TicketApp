using FluentAssertions;
using KGP.TicketApp.Backend.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KGP.TicketApp.Backend.Tests.Services
{
    public class AzureAppConfigurationTests
    {
        private WebApplicationFactory<Program> webAppFactory;
        private IOptions<ApplicationOptions>? service;

        [SetUp]
        public void Setup()
        {
            webAppFactory = new WebApplicationFactory<Program>();
            service = webAppFactory
                .Services
                .GetService<IOptions<ApplicationOptions>>();
        }

        [Test]
        public void AzureConfiguration_ShouldNotBeNull()
        {
            service.Should().NotBeNull();
        }

        [Test]
        public void AzureConfiguration_ShouldHaveEveryField()
        {
            var options = service!.Value;

            var properties = options.GetType()
                .GetProperties()
                .Select(prop => prop.GetValue(options))
                .ToList();
            
            properties.Should().AllSatisfy(prop => prop.Should().NotBeNull());
        }
    }
}
