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
    }
}
