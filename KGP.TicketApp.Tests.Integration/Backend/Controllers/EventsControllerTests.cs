using KGP.TicketApp.Backend;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Tests.Integration.TestUtilites;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using KGP.TicketApp.Model.Requests.Events;

namespace KGP.TicketApp.Tests.Integration.Backend.Controllers
{
    public class EventsControllerTests
    {
        private WebApplicationFactory<Program> application;
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            application = ApplicationFactory.GetApplicationWithMockDatabase<Program, DatabaseContext>();
            client = application.CreateClient();
        }

        [Test]
        public async Task DatabaseContext_ShouldNotBeNull()
        {
            var body = new GetEventsRequest
            {
                DateFrom = null,
                DateTo = null,
                IsFull = null,
                Place = null
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("events", UriKind.Relative),
                Method = HttpMethod.Get,
                Content = new StringContent(
                    JsonConvert.SerializeObject(body),
                    Encoding.UTF8,
                    "application/json")
            };

            var response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            int x = 0;
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }
    }
}
