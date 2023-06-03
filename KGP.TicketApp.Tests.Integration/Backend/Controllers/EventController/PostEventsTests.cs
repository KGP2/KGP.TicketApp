using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Model.Requests.Events;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Tests.Integration.TestUtilites.Http;
using KGP.TicketApp.Model.Database.Tables;
using System.Net;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms;
using Microsoft.Extensions.DependencyInjection;

namespace KGP.TicketApp.Tests.Integration.Backend.Controllers.EventController
{
    [TestFixture]
    public class PostEventsTests : BaseControllerTests
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

        private CreateEventRequest GetCreateEventRequest()
        {
            return new CreateEventRequest
            {
                Name = "a",
                OrganizerId = organizerId,
                Date = DateTime.UtcNow,
                ParticipiantsLimit = 10,
                Photo = "s", // TODO
                Street = "s", // TODO
                Price = 100.21M,
                SaleStartDate = DateTime.Today.AddDays(-1),
                SaleEndTime = DateTime.Today.AddDays(1)
            };
        }

        private EditEventRequest GetEditEventRequest()
        {
            return new EditEventRequest
            {
                ParticipiantsLimit = 50
            };
        }

        [Test]
        public async Task Create_Unauthenticated_ShouldReturnUnauthorized()
        {
            var request = RequestFactory.RequestMessageWithBody("events", HttpMethod.Post, GetCreateEventRequest());

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Create_Unauthorized_ShouldReturnUnauthorized()
        {
            await SignInAsClient(clientEmail, clientPassword);

            var request = RequestFactory.RequestMessageWithBody("events", HttpMethod.Post, GetCreateEventRequest());

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Create_Authorized_ShouldSuccessfullyCreateEvent()
        {
            await SignInAsOrganizer(organizerEmail, organizerPassword);

            var request = RequestFactory.RequestMessageWithBody("events", HttpMethod.Post, GetCreateEventRequest());

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            using var scope = GetNewScope();
            using var context = scope.ServiceProvider.GetService<DatabaseContext>()!;
            context.Set<Event>().Count().Should().Be(2);
        }

        //[Test]
        public async Task Edit_Authorized_ShouldSuccessfullyEditEvent()
        {
            await SignInAsOrganizer(organizerEmail, organizerPassword);

            var request = RequestFactory.RequestMessageWithBody($"events/{eventId}", HttpMethod.Post, GetEditEventRequest());

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            using var scope = GetNewScope();
            using var context = scope.ServiceProvider.GetService<DatabaseContext>()!;
            context.Set<Event>().Count().Should().Be(1);

            var @event = context.Set<Event>().Find(eventId)!;
            @event.ParticipantsLimit.Should().Be(50);
            @event.Name.Should().Be("a");
        }
    }
}
