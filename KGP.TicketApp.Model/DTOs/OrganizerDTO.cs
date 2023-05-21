using KGP.TicketApp.Model.Database.Tables;

namespace KGP.TicketApp.Model.DTOs
{
    public record OrganizerDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? CompanyName { get; set; }

        public static OrganizerDTO FromDatabaseUser(Organizer user)
        {
            return new OrganizerDTO()
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                CompanyName = user.CompanyName
            };
        }
    }
}
