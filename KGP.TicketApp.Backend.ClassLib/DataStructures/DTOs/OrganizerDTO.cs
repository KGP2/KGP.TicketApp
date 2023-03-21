namespace KGP.TicketApp.Backend.Libraries.DataStructures.DTOs
{
    public record OrganizerDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }
        public string CompanyName { get; set; }
    }
}
