using KGP.TicketAPP.Utils.Validation;
using System.Text;

namespace KGP.TicketApp.Backend.Validation
{
    public static class RegisterValidation
    {
        public static bool ValidateRegister(string email, string password, IValidationService validationService, out string error)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool ret = true;

            if (!validationService.EmailValidator.Validate(email, out var error1))
            {
                stringBuilder.AppendLine(error1);
                ret = false;
            }
            if (!validationService.PasswordValidator.Validate(password, out var error2))
            {
                stringBuilder.AppendLine(error2);
                ret = false;
            }

            error = stringBuilder.ToString();
            return ret;
        }
    }
}
