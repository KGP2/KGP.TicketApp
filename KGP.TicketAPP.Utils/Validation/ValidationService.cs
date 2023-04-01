using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.TicketAPP.Utils.Validation
{
    public class ValidationService : IValidationService
    {
        #region Fields

        private PasswordValidator passwordValidator;
        private EmailValidator emailValidator;

        #endregion

        #region Constructors

        public ValidationService()
        {
            passwordValidator = new PasswordValidator();
            emailValidator = new EmailValidator();
        }

        #endregion



        #region Properties

        public PasswordValidator PasswordValidator => passwordValidator;

        public EmailValidator EmailValidator => emailValidator;

        #endregion
    }
}
