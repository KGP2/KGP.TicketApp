using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms;
using System.Net;
using KGP.TicketApp.Model.Database;
using Microsoft.Extensions.DependencyInjection;

namespace KGP.TicketApp.Tests.Integration.Backend.Controllers.EventController
{
    [TestFixture]
    public class DeleteEventsTests : BaseControllerTests
    {
        private Guid eventId;

        private Guid organizerId;
        private string organizerEmail = null!;
        private string organizerPassword = null!;

        private Guid clientId;
        private string clientEmail = null!;
        private string clientPassword = null!;

        protected override void AddInitialData(DatabaseContext databaseContext)
        {
            organizerId = Guid.NewGuid();
            organizerEmail = "organizer@company.com";
            organizerPassword = "strongP@ssword123";

            var organizer = new Organizer
            {
                Id = organizerId,
                Name = "organizerName",
                Surname = "organizerSurname",
                CompanyName = "organizerCompany",
                Email = organizerEmail,
                Password = new BCryptAlgorithm().Hash(organizerPassword),
            };
            databaseContext.Add(organizer);

            clientId = Guid.NewGuid();
            clientEmail = "client@company.com";
            clientPassword = "strongP@ssword123";
            var client = new Client
            {
                Id = clientId,
                Name = "clientName",
                Surname = "clientSurname",
                Email = clientEmail,
                Password = new BCryptAlgorithm().Hash(clientPassword),
                DateOfBirth = DateTime.Today.AddYears(-20),
            };
            databaseContext.Add(client);

            var location = new Location
            {
                City = "a",
                StreetName = "b",
            };

            eventId = Guid.NewGuid();

            databaseContext.Add(new Event
            {
                Id = eventId,
                Name = "a",
                ParticipantsLimit = 2,
                Date = DateTime.Today,
                TicketSaleEndDate = DateTime.Today.AddDays(1),
                TicketSaleStartDate = DateTime.Today.AddDays(-1),
                Organizer = organizer,
                Place = location,
                Price = "2137",
                Photo = "s"
            });
            databaseContext.SaveChanges();
        }
        [Test]
        public async Task Delete_Unauthenticated_ShouldReturnUnauthorized()
        {   
            var response = await HttpClient.DeleteAsync($"events/{eventId}");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            using var scope = GetNewScope();
            using var context = scope.ServiceProvider.GetService<DatabaseContext>()!;
            context.Set<Event>().Count().Should().Be(1);
        }

        [Test]
        public async Task Delete_Unauthorized_ShouldReturnUnauthorized()
        {
            var response = await HttpClient.DeleteAsync($"events/{eventId}");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            using var scope = GetNewScope();
            using var context = scope.ServiceProvider.GetService<DatabaseContext>()!;
            context.Set<Event>().Count().Should().Be(1);
        }

        //[Test]
        public async Task Delete_Authorized_ShouldSuccessfullyCreateEvent()
        {
            await SignInAsOrganizer(organizerEmail, organizerPassword);

            var response = await HttpClient.DeleteAsync($"events/{eventId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            using var scope = GetNewScope();
            using var context = scope.ServiceProvider.GetService<DatabaseContext>()!;
            context.Set<Event>().Count().Should().Be(0);
        }

        [Test]
        public async Task Delete_NotExisting_ShouldReturnNotFound()
        {
            await SignInAsOrganizer(organizerEmail, organizerPassword);

            var response = await HttpClient.DeleteAsync($"events/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            using var scope = GetNewScope();
            using var context = scope.ServiceProvider.GetService<DatabaseContext>()!;
            context.Set<Event>().Count().Should().Be(1);
        }

    }
}
