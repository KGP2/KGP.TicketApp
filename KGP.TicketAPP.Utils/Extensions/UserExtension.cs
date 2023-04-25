using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Extensions
{
    public static class UserExtension
    {
        public static void UpdateUser(this User user, EditRegisterUserRequest request)
        {
            user.Surname = request.Surname;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Name = request.Name;
        }
    }
}
