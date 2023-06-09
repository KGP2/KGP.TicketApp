﻿using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGP.TicketApp.Model.Database.Tables.User;

namespace KGP.TicketApp.Repositories
{
    public class ClientRepository : UserRepository<Client>, IClientRepository
    {
        #region Constructors

        public ClientRepository(DatabaseContext context) : base(context) {}

        #endregion
    }
}
