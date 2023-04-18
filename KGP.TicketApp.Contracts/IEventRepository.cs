using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KGP.TicketApp.Model.Database.Tables;

namespace KGP.TicketApp.Contracts
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        List<Event> GetByOrganizerId(Guid id);
        List<Event> GetByIdList(IEnumerable<Guid> ids);
    }
}