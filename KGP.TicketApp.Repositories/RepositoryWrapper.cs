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
        private IClientRepository clientRepository;
        private IOrganizerRepository organizerRepository;

        #endregion

        #region Constructors

        public RepositoryWrapper(DatabaseContext databaseContext) => this.databaseContext = databaseContext;

        #endregion

        #region Properties

        public IClientRepository ClientRepository
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(databaseContext);

                return clientRepository;
            }
        }
        public IOrganizerRepository OrganizerRepository
        {
            get
            {
                if (organizerRepository == null)
                    organizerRepository = new OrganizerRepository(databaseContext);

                return organizerRepository;
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
