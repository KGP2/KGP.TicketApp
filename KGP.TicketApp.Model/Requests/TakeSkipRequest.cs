namespace KGP.TicketApp.Model.Requests
{
    public record TakeSkipRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
