using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Backend.Tests.Utils.PdfGenerator
{
    public class GeneratorTests
    {
        #region Consts
        protected const string ticketName = "generatedTicket";
        #endregion
        public void SaveLocally(byte[] data)
        {
            using var stream = File.Create(ticketName);
            stream.Write(data, 0, data.Length);
        }
    }
}
