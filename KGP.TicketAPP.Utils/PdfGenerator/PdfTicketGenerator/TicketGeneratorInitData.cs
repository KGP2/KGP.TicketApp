﻿using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Utils.PdfGenerator.PdfTicketGenerator
{
    public class TicketGeneratorInitData : IPdfGeneratorInitData<TicketGeneratorInitData>
    {
        #region Fields
        public Ticket ticket;
        #endregion

        #region Interface methods

        public void Init(IPdfGenerator<TicketGeneratorInitData> generator)
        {
            var ticketGenerator = generator as TicketGenerator;

            ticketGenerator.Ticket = ticket;
        }
        #endregion
    }
}
