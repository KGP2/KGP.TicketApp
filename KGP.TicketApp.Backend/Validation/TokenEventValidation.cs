using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using KGP.TicketApp.Contracts;
using KGP.TicketAPP.Utils.Extensions;

namespace KGP.TicketApp.Backend.Validation
{
    public class TokenEventValidation : IActionFilter
    {
        #region Fields

        IRepositoryWrapper repositoryWrapper;

        #endregion

        #region Constructor
        public TokenEventValidation (IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }
        #endregion
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
                var Event = repositoryWrapper.TicketRepository.GetById(id);
                if (Event == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }
                if (idFromReq != Event.Owner.Id)
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}
