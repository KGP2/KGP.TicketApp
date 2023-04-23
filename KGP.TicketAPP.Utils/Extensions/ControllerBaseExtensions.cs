using KGP.TicketApp.Backend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketAPP.Utils.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetTokenFromCookie(this ControllerBase controller)
        {
            return controller.Request.Cookies["Token"] ?? throw new InvalidOperationException("Cookie value \"Token\" not set.");
        }

        public static Guid GetCallingUserIdFromCookie(this ControllerBase controller)
        {
            var token = controller.GetTokenFromCookie();
            return Guid.Parse(JwtTokenHelper.GetIdFromToken(token));
        }
    }
}
