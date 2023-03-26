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
    public class OrganizerRepository : RepositoryBase<Organizer>, IOrganizerRepository
    {
        #region Constructors

        public OrganizerRepository(DatabaseContext context) : base(context) { }

        #endregion

        #region Interface methods

        public Organizer? FindUserByEmail(string email)
        {
            return DatabaseContext.Set<Organizer>().FirstOrDefault(user => user.Email == email);
        }

        #endregion
    }
}
