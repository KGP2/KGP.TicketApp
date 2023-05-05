using Microsoft.Extensions.DependencyInjection;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

namespace KGP.TicketApp.Services.CloudStorage
{
    public static class ServiceExtensions
    {
        public static void AddAzureBlobCloudStorage(this IServiceCollection services, string azureStorageName, string azureStorageKey)
        {
            services.AddScoped<ICloudStorage, AzureBlobCloudStorage>(s =>
            {
                return new AzureBlobCloudStorage(azureStorageName, azureStorageKey);
            });
        }
    }
}
