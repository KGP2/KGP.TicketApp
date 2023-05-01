using KGP.TicketApp.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator.TicketGenerator
{
    public class TicketGeneratorInitData : IPdfGeneratorInitData
    {
        #region Fields
        public ClientDTO client;
        #endregion

        #region Interface methods

        public void Init(IPdfGenerator<IPdfGeneratorInitData> generator)
        {
            if (generator.GetType() != typeof(TicketGenerator))
                throw new ArgumentException("Generator should be type of TicketGenerator.");

            var ticketGenerator = generator as TicketGenerator;

            ticketGenerator.Client = client;
        }
        #endregion
    }
}
