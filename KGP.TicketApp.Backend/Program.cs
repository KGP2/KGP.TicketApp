using KGP.TicketApp.Backend.Options;

namespace KGP.TicketApp.Backend
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure logging
            builder.Logging
                .ClearProviders()
                .AddConsole()
                .AddAzureWebAppDiagnostics();

            // Add configuration
            builder.Configuration.AddAzureAppConfiguration(builder.Configuration["AzureConfigurationConnectionString"]);
            builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection("Backend"));

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


