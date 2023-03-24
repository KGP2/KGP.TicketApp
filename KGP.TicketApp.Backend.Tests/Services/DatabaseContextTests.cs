using KGP.TicketApp.Model.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace KGP.TicketApp.Backend.Tests.Services
{
    public class DatabaseContextTests
    {
        private DatabaseContext? databaseContext;
        private IServiceScope? scope;

        [SetUp]
        public void Setup()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            scope = webAppFactory.Services.CreateScope();
            databaseContext = scope?.ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        [Test]
        public void DatabaseContext_ShouldNotBeNull()
        {
            databaseContext.Should().NotBeNull();
        }

        [Test]
        public void DatabaseContext_ShouldConnect()
        {
            databaseContext!.Database.CanConnect().Should().BeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            scope?.Dispose();
        }
    }
}
