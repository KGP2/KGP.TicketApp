#pragma warning disable CS8618

namespace KGP.TicketApp.Model.Requests.Events
{
    public record CreateEventRequest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public Guid OrganizerId { get; set; }
        public int ParticipiantsLimit { get; set; }
        public decimal Price { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndTime { get; set; }
        public string Photo { get; set; }
    }
}
