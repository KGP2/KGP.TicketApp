using KGP.TicketApp.Backend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketAPP.Utils.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetToken(this ControllerBase controller)
        {
            return controller.Request.Headers.Authorization.First().Split(' ')[1];
        }

        public static Guid GetCallingUserId(this ControllerBase controller)
        {
            var token = controller.GetToken();
            return Guid.Parse(JwtTokenHelper.GetIdFromToken(token));
        }
    }
}
