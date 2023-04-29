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

        protected override void AddInitialData()
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
            DatabaseContext.Add(organizer);

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
            DatabaseContext.Add(client);

            var location = new Location
            {
                City = "a",
                StreetName = "b",
                BuildingName = "c",
                PostalCode = "d",
                StreetNumber = "e"
            };

            eventId = Guid.NewGuid();

            DatabaseContext.Add(new Event
            {
                Id = eventId,
                Name = "a",
                ParticipantsLimit = 2,
                Date = DateTime.Today,
                TicketSaleEndDate = DateTime.Today.AddDays(1),
                TicketSaleStartDate = DateTime.Today.AddDays(-1),
                Organizer = organizer,
                Place = location,
                Price = "2137"
            });
            DatabaseContext.SaveChanges();
        }

        [Test]
        public async Task WithEmptyRequest_ShouldReturnAllEvents()
        {
            var request = RequestFactory.RequestMessageWithBody("events", HttpMethod.Get, new GetEventsRequest
            {
                DateFrom = null,
                DateTo = null,
                IsFull = null,
                Place = null
            });

            var response = await HttpClient.SendAsync(request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO[]>();
            result.Should().HaveCount(1);
        }

        [Test]
        public async Task GetEvent_Existing_ShouldReturnEvent()
        {
            var response = await HttpClient.GetAsync($"events/{eventId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.GetContent<EventDTO>();
            result?.Id.Should().Be(eventId.ToString());
        }

        [Test]
        public async Task GetEvent_NotExisting_ShouldReturnNotFound()
        {
            // Guid.NewGuid() is almost surely not present in database yet
            var response = await HttpClient.GetAsync($"events/{Guid.NewGuid()}");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
