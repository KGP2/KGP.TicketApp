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
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        #region Constructors

        public EventRepository(DatabaseContext context) : base(context) { }

        public List<Event> GetByIdList(IEnumerable<Guid> ids)
        {
            return DatabaseContext.Set<Event>()
                .Where(e => ids.Contains(e.Id))
                .ToList();
        }

        public List<Event> GetByOrganizerId(Guid id)
        {
            return DatabaseContext.Set<Event>()
                .Where(e => e.Organizer.Id == id)
                .ToList();
        }

        #endregion

        #region Interface methods

        #endregion
    }
}
