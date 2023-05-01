using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Backend.Tests.Utils.PdfGenerator
{
    public class GeneratorTests
    {
        public void SaveLocally(byte[] data)
        {
            using var stream = File.Create("generatedPdf");
            stream.Write(data, 0, data.Length);
        }
    }
}
