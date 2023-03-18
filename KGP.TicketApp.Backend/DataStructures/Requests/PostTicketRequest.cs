using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KGP.TicketApp.Backend.DataStructures.Requests
{
    public class PostTicketRequest
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
    }
}
