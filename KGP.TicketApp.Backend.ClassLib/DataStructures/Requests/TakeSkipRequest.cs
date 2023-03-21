namespace KGP.TicketApp.Backend.Libraries.DataStructures.Requests
{
    public record TakeSkipRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
