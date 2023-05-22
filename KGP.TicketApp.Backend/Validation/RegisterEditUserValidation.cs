using KGP.TicketApp.Contracts;
using KGP.TicketApp.Model.Requests;
using KGP.TicketAPP.Utils.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Tls;
using System.Security.Principal;
using System.Text;
using static KGP.TicketApp.Model.Database.Tables.User;

namespace KGP.TicketApp.Backend.Validation
{
    public class RegisterEditUserValidation : IActionFilter
    {
        #region Fields
        private IValidationService validationService;
        private IRepositoryWrapper repositoryWrapper;
        private Types userType;
        private bool checkType;
        #endregion


        #region Constructors
        public RegisterEditUserValidation(Types userType, bool checkType, IValidationService validationService, IRepositoryWrapper repositoryWrapper)
        {
            this.validationService = validationService;
            this.repositoryWrapper = repositoryWrapper;
            this.userType = userType;
            this.checkType = checkType;
        }
        #endregion


        #region Interface methods
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EditRegisterUserRequest);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Request is null");
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            var req = (EditRegisterUserRequest)param.Value;

            if (!req.Email.IsNullOrEmpty() || checkType)
                if (!validationService.EmailValidator.Validate(req.Email, out var error1))
                    stringBuilder.AppendLine(error1);
            if (!req.Password.IsNullOrEmpty() || checkType)
                if (!validationService.PasswordValidator.Validate(req.Password, out var error2))          
                    stringBuilder.AppendLine(error2);

            if (checkType)
            {
                if (userType == Types.Organizer && repositoryWrapper.OrganizerRepository.FindUserByEmail(req.Email) != null)
                    stringBuilder.AppendLine("Organizer with this email exists");
                else if (userType == Types.Client && repositoryWrapper.ClientRepository.FindUserByEmail(req.Email) != null)
                    stringBuilder.AppendLine("Client with this email exists");
            }

            if (stringBuilder.Length > 0)
                context.Result = new BadRequestObjectResult($"{stringBuilder}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        #endregion
    }
}
