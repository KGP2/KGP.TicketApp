using KGP.TicketApp.Model.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KGP.TicketApp.Tests.Integration.TestUtilites
{
    public static class ApplicationFactory
    {
        public static WebApplicationFactory<TProgram> GetFullApplication<TProgram>() where TProgram : class
        {
            return new WebApplicationFactory<TProgram>();
        }

        public static WebApplicationFactory<TProgram> GetApplicationWithMockDatabase<TProgram, TDbContext>()
            where TProgram : class
            where TDbContext : DbContext
        {
            return new WebApplicationFactoryWithMockDatabase<TProgram, TDbContext>();
        }
    }
}
