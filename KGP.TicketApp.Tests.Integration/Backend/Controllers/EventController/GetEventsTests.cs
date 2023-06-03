using KGP.TicketApp.Backend.Helpers;
using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.DTOs;
using KGP.TicketApp.Model.Requests.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using KGP.TicketAPP.Utils.Extensions;
using NUnit.Framework;
using KGP.TicketApp.Tests.Integration.TestUtilites.Http;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms;
using System.Net;
using KGP.TicketApp.Model.Database;

namespace KGP.TicketApp.Tests.Integration.Backend.Controllers.EventController
{
    [TestFixture]
    public class GetEventsTests : BaseControllerTests
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
                BuildingName = "c",
                PostalCode = "d",
                StreetNumber = "e"
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
                Photo = "a"
            });
            databaseContext.SaveChanges();
        }

        [Test]
        public async Task GetAll_WithEmptyRequest_ShouldReturnAllEvents()
        {
            var response = await HttpClient.GetAsync("events");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(1);
        }

        [Test]
        public async Task GetAll_WithQueryString_ShouldFilter()
        {
            // IsFull=False
            var response = await HttpClient.GetAsync("events?IsFull=False");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(1);

            // IsFull=True
            response = await HttpClient.GetAsync("events?IsFull=True");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(0);

            // DateFrom=2023-01-01&IsFull=False
            response = await HttpClient.GetAsync("events?DateFrom=2023-01-01&IsFull=False");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(1);

            // DateTo=2023-01-01&IsFull=False
            response = await HttpClient.GetAsync("events?DateTo=2023-01-01&IsFull=False");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(0);
        }

        [Test]
        public async Task GetOne_Existing_ShouldSuccessfullyReturnMatchingEvent()
        {
            var response = await HttpClient.GetAsync($"events/{eventId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO>();
            result?.Id.Should().Be(eventId.ToString());
        }

        [Test]
        public async Task GetOne_NotExisting_ShouldReturnNotFound()
        {
            // Guid.NewGuid() is almost surely not present in database yet
            var response = await HttpClient.GetAsync($"events/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GetByOrganier_NotExistingOrganizer_ShouldReturnEmptyList()
        {
            // Guid.NewGuid() is almost surely not present in database yet
            var response = await HttpClient.GetAsync($"eventsByOrganizer/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(0);
        }

        [Test]
        public async Task GetByOrganier_ExistingOrganizer_ShouldReturnEvents()
        {
            // Guid.NewGuid() is almost surely not present in database yet
            var response = await HttpClient.GetAsync($"eventsByOrganizer/{organizerId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(1);
        }

        [Test]
        public async Task GetList_AllNotExisting_ShouldReturnEmptyList()
        {
            // Guid.NewGuid() is almost surely not present in database yet
            var request = RequestFactory.RequestMessageWithBody("eventList", HttpMethod.Get, new Guid[]
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            });

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(0);
        }

        [Test]
        public async Task GetList_SomeExisting_ShouldReturnListForThoseExisting()
        {
            // Guid.NewGuid() is almost surely not present in database yet
            var request = RequestFactory.RequestMessageWithBody("eventList", HttpMethod.Get, new Guid[]
            {
                Guid.NewGuid(),
                eventId,
                Guid.NewGuid(),
            });

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(1);
        }
    }
}
