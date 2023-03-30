using KGP.TicketApp.Model.Database;
using KGP.TicketApp.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Repositories
{
    public abstract class UserRepository<T> : RepositoryBase<T> where T : User
    {
        #region Constructors

        public UserRepository(DatabaseContext context) : base(context) { }

        #endregion

        #region Interface methods

        public T? FindUserByEmail(string email)
        {
            return DatabaseContext.Set<T>().FirstOrDefault(user => user.Email == email);
        }

        #endregion
    }
}
