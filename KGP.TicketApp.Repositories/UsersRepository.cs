using KGP.TicketApp.Contracts;
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
    public class UsersRepository : RepositoryBase<User>, IUserRepository
    {
        #region Constructors

        public UsersRepository(DatabaseContext context) : base(context) {}

        #endregion

        #region Interface methods

        public User? FindUserByEmail(string email, Types type)
        {
            return DatabaseContext.Set<User>().FirstOrDefault(user => user.Email == email && user.UserType == type);
        }

        #endregion
    }
}
