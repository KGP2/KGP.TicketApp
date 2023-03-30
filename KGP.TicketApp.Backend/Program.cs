using KGP.TicketApp.Backend.Options;
using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Repositories;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms.Factory;
using KGP.TicketAPP.Utils.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            //Add authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration.GetSection("Backend").Get<ApplicationOptions>().JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Backend").Get<ApplicationOptions>().JwtKey)),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetSection("Backend").Get<ApplicationOptions>().DatabaseConnectionString,
                subbuilder => subbuilder.MigrationsAssembly("KGP.TicketApp.Backend"));
            });

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IHashAlgorithmFactory, HashAlgorithmFactory>();
            builder.Services.AddScoped<IValidationService, ValidationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
