using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Validation
{
    public class PasswordValidator : IValidator<string>
    {
        public bool Validate(string obj, out string error)
        {
            error = "password is invalid";

            return true;
        }
    }
}
