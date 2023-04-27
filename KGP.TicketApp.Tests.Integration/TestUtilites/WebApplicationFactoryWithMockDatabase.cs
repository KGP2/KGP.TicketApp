using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KGP.TicketApp.Tests.Integration.TestUtilites
{
    public class WebApplicationFactoryWithMockDatabase<TProgram, TDbContext> : WebApplicationFactory<TProgram>
        where TDbContext : DbContext
        where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<TDbContext>(options =>
                {
                    options.UseSqlite("DataSource=file::memory:?cache=shared");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
                appContext.Database.EnsureCreated();
            });
        }
    }
}
