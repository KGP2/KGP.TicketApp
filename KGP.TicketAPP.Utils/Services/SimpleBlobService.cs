using Azure.Storage.Blobs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utilities.Services
{
    public class SimpleBlobService
    {
        #region Fields

        private BlobServiceClient blobServiceClient;
        private BlobContainerClient ticketCointainer;

        #endregion

        #region Constructors

        public SimpleBlobService(string connectionString, string ticketCointainerName)
        {
            if (connectionString.IsNullOrEmpty())
                throw new Exception("Blob connection string is empty. Check Azure configuration.");
            if (ticketCointainerName.IsNullOrEmpty())
                throw new Exception("Blob cointainer name string is empty. Check Azure configuration.");

            blobServiceClient = new BlobServiceClient(connectionString);
            ticketCointainer = blobServiceClient.GetBlobContainerClient(ticketCointainerName);
        }

        #endregion

        #region Public methods


        public string SaveTicket(byte[] ticket, string ticketName)
        {
            BlobClient blobClient = ticketCointainer.GetBlobClient(ticketName);
            

            using (MemoryStream memoryStream = new MemoryStream(ticket))
            {
                blobClient.Upload(memoryStream, overwrite: true);
            }

            return blobClient.Uri.ToString();
        }

        #endregion
    }
}
