using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Services.CloudStorage
{
    public class AzureBlobCloudStorage : ICloudStorage
    {
        private BlobContainerClient client;

        public AzureBlobCloudStorage(string azureStorageName, string azureStorageKey)
        {
            client = new BlobContainerClient("", "");
        }

        public byte[] Download(string path)
        {
            throw new NotImplementedException();
        }

        public string Upload(string fileName, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
