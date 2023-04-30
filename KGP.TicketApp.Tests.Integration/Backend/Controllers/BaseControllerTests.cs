using KGP.TicketApp.Backend;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.Requests;
using KGP.TicketApp.Model.Requests.Events;
using KGP.TicketApp.Tests.Integration.TestUtilites.Http;
using KGP.TicketApp.Tests.Integration.TestUtilites.WebApplication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;

namespace KGP.TicketApp.Tests.Integration.Backend.Controllers
{
    public abstract class BaseControllerTests
    {
        protected WebApplicationFactory<Program> Application;
        protected DatabaseContext DatabaseContext;
        protected IServiceScope? Scope;
        protected HttpClient HttpClient;

        [SetUp]
        public void Setup()
        {
            Application = ApplicationFactory.GetApplicationWithMockDatabase<Program, DatabaseContext>();
            Scope = Application.Services.CreateScope();
            HttpClient = Application.CreateClient();
            DatabaseContext = Scope?.ServiceProvider.GetRequiredService<DatabaseContext>()!;

            DatabaseContext.Database.EnsureDeleted();
            DatabaseContext.Database.EnsureCreated();

            AddInitialData();
        }

        protected abstract void AddInitialData();

        protected async Task SignInAsOrganizer(string email, string password)
        {
            await SignIn(email, password, "users/organizers/login");
        }

        protected async Task SignInAsClient(string email, string password)
        {
            await SignIn(email, password, "users/clients/login");
        }

        private async Task SignIn(string email, string password, string path)
        {
            var request = RequestFactory.RequestMessageWithBody(path, HttpMethod.Post, new LoginCredentialsRequest
            {
                Email = email,
                Password = password
            });

            var result = await HttpClient.SendAsync(request);
            result.Headers.TryGetValues("Set-Cookie", out var values);

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", values!.First().Replace("Token=", "").Replace(@"; path=/", ""));
        }

        [TearDown]
        public void TearDown()
        {
            Scope?.Dispose();
        }
    }
}
