using KGP.TicketApp.Backend;
using KGP.TicketApp.Backend.Options;
using KGP.TicketApp.Tests.Integration.TestUtilites;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KGP.TicketApp.Tests.Integration.Backend.Services
{
    public class AzureAppConfigurationTests
    {
        private WebApplicationFactory<Program> application;
        private IOptions<ApplicationOptions>? service;

        [SetUp]
        public void Setup()
        {
            application = ApplicationFactory.GetFullApplication<Program>();
            service = application
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
