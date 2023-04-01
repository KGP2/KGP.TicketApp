using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Repositories
{
    public class OrganizerRepository : UserRepository<Organizer>, IOrganizerRepository
    {
        #region Constructors

        public OrganizerRepository(DatabaseContext context) : base(context) { }

        #endregion
    }
}
