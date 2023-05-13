using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketApp.Contracts
{
    public interface IRepositoryWrapper
    {
        IClientRepository ClientRepository { get; }
        IOrganizerRepository OrganizerRepository { get; }
        IEventRepository EventRepository { get; }
        ITicketRepository TicketRepository { get; }

        void Save();
    }
}
