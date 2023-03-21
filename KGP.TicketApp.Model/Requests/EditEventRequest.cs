namespace KGP.TicketApp.Model.Requests
{
    public record EditEventRequest
    {
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public int ParticipiantsLimit { get; set; }
        public double Price { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndTime { get; set; }
    }
}
