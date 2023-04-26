using KGP.TicketApp.Model.Requests;
using KGP.TicketAPP.Utils.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Principal;
using System.Text;

namespace KGP.TicketApp.Backend.Validation
{
    public class RegisterEditUserValidation : IActionFilter
    {
        #region Fields
        private IValidationService validationService;
        #endregion


        #region Constructors
        public RegisterEditUserValidation(IValidationService validationService) => this.validationService = validationService;
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

            if (!validationService.EmailValidator.Validate(req.Email, out var error1))
                stringBuilder.AppendLine(error1);
            if (!validationService.PasswordValidator.Validate(req.Password, out var error2))          
                stringBuilder.AppendLine(error2);

            if (stringBuilder.Length > 0)
                context.Result = new BadRequestObjectResult($"{stringBuilder}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        #endregion
    }
}
