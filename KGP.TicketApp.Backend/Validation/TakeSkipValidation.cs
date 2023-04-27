using KGP.TicketApp.Model.Requests;
using KGP.TicketAPP.Utils.Validation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace KGP.TicketApp.Backend.Validation
{
    public class TakeSkipValidation : IActionFilter
    {
        #region Interface methods
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is TakeSkipRequest);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Request is null");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("Skip and Take cannot be negative values");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        #endregion
    }
}
