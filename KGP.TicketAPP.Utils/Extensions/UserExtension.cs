using KGP.TicketApp.Model.Database.Tables;
using KGP.TicketApp.Model.Requests;
using Microsoft.IdentityModel.Tokens;
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
            if (!request.Surname.IsNullOrEmpty())
                user.Surname = request.Surname;
            if (!request.Email.IsNullOrEmpty())
                user.Email = request.Email;
            if (!request.Password.IsNullOrEmpty())
                user.Password = request.Password;
            if (!request.Name.IsNullOrEmpty())
                user.Name = request.Name;
        }
    }
}
