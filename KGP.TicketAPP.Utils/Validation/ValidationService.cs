using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Validation
{
    public static class ValidationService
    {
        #region Fields

        private static PasswordValidator passwordValidator = new PasswordValidator();
        private static EmailValidator emailValidator = new EmailValidator();

        #endregion

        #region Properties

        public static PasswordValidator PasswordValidator => passwordValidator;

        public static EmailValidator EmailValidator => emailValidator;

        #endregion
    }
}
