using KGP.TicketApp.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGP.TicketApp.Model.Database.Tables.User;

namespace KGP.TicketApp.Contracts
{
    public interface IUserRepository<T>
    {
        T? FindUserByEmail(string email);
    }
}
