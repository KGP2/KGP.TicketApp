using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Model.Database.Tables;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Ticket> GetTicketsByOwner(Guid ownerId)
        {
            return DatabaseContext.Tickets.Include(t => t.Event).Where(it => it.Owner.Id == ownerId);
        }
        public override Ticket? GetById(Guid id)
        {
            return DatabaseContext.Tickets.Include(it => it.Event).Where(it => it.Id == id).First();
        }
    }
}
