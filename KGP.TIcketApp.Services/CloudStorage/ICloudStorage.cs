using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Services.CloudStorage
{
    public interface ICloudStorage
    {
        byte[] Download(string path);
        string Upload(string fileName, byte[] data);
    }
}
