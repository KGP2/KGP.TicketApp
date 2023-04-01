using KGP.TicketApp.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Contracts
{
    public interface IOrganizerRepository : IRepositoryBase<Organizer>, IUserRepository<Organizer>
    {
    }
}
