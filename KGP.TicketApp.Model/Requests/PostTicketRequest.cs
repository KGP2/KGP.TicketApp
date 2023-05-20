namespace KGP.TicketApp.Model.Requests
{
    public record PostTicketRequest
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
    }
}
