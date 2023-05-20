using KGP.TicketAPP.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KGP.TicketApp.Backend.Validation
{
    public class TokenValidation : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = (ControllerBase)context.Controller;
            var idFromReq = controller.GetCallingUserId();
            if (context.ActionArguments.ContainsKey("id"))
            {
                var id = (Guid)context.ActionArguments["id"];
                if (id != idFromReq)
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}
