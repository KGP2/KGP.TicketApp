namespace KGP.TicketApp.Model.Requests
{
    public record PostTicketRequest
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
    }
}
