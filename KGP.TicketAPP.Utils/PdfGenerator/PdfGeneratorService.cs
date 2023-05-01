using KGP.TicketApp.Utils.PdfGenerator.PdfTicketGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator
{
    public class PdfGeneratorService
    {
        #region Fields
        private TicketGenerator ticketGenerator = new TicketGenerator();
        #endregion

        #region Properties
        public TicketGenerator TicketGenerator => ticketGenerator;
        #endregion
    }
}
