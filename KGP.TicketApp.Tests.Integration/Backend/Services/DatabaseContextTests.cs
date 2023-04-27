using FluentAssertions;
using KGP.TicketApp.Backend;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Tests.Integration.TestUtilites;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KGP.TicketApp.Tests.Integration.Backend.Services
{
    public class DatabaseContextTests
    {
        private WebApplicationFactory<Program> application;
        private DatabaseContext? databaseContext;
        private IServiceScope? scope;

        [SetUp]
        public void Setup()
        {
            application = ApplicationFactory.GetFullApplication<Program>();
            scope = application.Services.CreateScope();
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

        [TestCaseSource(nameof(tableNames))]
        public void DatabaseContext_ShouldFindTable(string tableName)
        {
            var tables = databaseContext!.Database
                .SqlQuery<string?>($"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'")
                .ToList();

            tables.Should().Contain(tableName);
        }

        private static object[] tableNames = { "Users", "Events", "Tickets", "Locations", "ClientEvent_Likings", "ClientEvent_Participatings" };

        [TearDown]
        public void TearDown()
        {
            scope?.Dispose();
        }
    }
}
