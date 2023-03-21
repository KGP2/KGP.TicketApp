namespace KGP.TicketApp.Backend.Libraries.DataStructures.Requests
{
    public record PostTicketRequest
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
    }
}
