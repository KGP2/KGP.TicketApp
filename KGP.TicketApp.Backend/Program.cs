using System.Reflection;
using KGP.TicketApp.Backend.Options;
using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Repositories;
using KGP.TicketAPP.Utils.Helpers.HashAlgorithms.Factory;
using KGP.TicketAPP.Utils.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KGP.TicketApp.Backend.Helpers;

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
            }).AddJwtBearer(JwtTokenHelper.Client, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration.GetSection("Backend").Get<ApplicationOptions>().JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Backend").Get<ApplicationOptions>().JwtKey)),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    AudienceValidator = JwtTokenHelper.ClientTypeValidator
                };
            }).AddJwtBearer(JwtTokenHelper.Organizer, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration.GetSection("Backend").Get<ApplicationOptions>().JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Backend").Get<ApplicationOptions>().JwtKey)),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    AudienceValidator = JwtTokenHelper.OrganizerTypeValidator
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
           builder.Services.AddCors(o => o.AddPolicy("CorsEnabled", builder =>
           {
               builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
           }));
            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IHashAlgorithmFactory, HashAlgorithmFactory>();
            builder.Services.AddScoped<IValidationService, ValidationService>();
            builder.Services.AddSwaggerGen(options =>
            {
                var basePath = AppContext.BaseDirectory;
                var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
                var commentsXmlPath = Path.Combine(basePath, fileName);
                options.IncludeXmlComments(commentsXmlPath, true);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsEnabled");

            app.MapControllers();

            app.Run();
        }
    }
}
