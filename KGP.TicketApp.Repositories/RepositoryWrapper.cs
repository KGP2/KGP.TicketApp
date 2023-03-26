using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        #region Fields

        private DatabaseContext databaseContext;
        private IUserRepository userRepository;

        #endregion

        #region Constructors

        public RepositoryWrapper(DatabaseContext databaseContext) => this.databaseContext = databaseContext;

        #endregion

        #region Properties

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UsersRepository(databaseContext);

                return userRepository;
            }
        }

        #endregion

        #region Interface methods
        public void Save()
        {
            databaseContext.SaveChanges();
        }

        #endregion
    }
}
