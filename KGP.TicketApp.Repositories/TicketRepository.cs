using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Repositories
{
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        #region Constructors
        public TicketRepository(DatabaseContext context) : base(context)
        {
        }
        #endregion
    }
}
