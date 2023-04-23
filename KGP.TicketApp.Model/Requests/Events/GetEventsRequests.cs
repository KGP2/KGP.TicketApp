namespace KGP.TicketApp.Model.Requests.Events
{
    public record GetEventsRequest
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Place { get; set; }
        public bool? IsFull { get; set; }
    }
}
