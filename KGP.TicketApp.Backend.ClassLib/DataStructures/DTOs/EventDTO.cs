namespace KGP.TicketApp.Backend.Libraries.DataStructures.DTOs
{
    public record EventDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string OrganizerId { get; set; }
        public int ParticipantsLimit { get; set; }
        public double Price { get; set; }
        public DateTime SaleStartDate { get; set; }
        public DateTime SaleEndDate { get; set; }
        public string Photo { get; set; }
    }
}
