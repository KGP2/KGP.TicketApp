using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Validation
{
    public class EmailValidator : IValidator<string>
    {
        public bool Validate(string obj, out string error)
        {
            error = "email is invalid";

            return true;
        }
    }
}
